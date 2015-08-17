#region Apache License

//-----------------------------------------------------------------------
// <copyright file="LoggingService.cs" company="StrixIT">
// Copyright 2015 StrixIT. Author R.G. Schurgers MA MSc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

#endregion Apache License

using log4net;
using StrixIT.Platform.Core;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Security;

namespace StrixIT.Platform.Modules.Logging
{
    /// <summary>
    /// The default implementation of the logger interface.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        #region Private Fields

        private const string ANALYTICS = "Analytics";
        private const string AUDIT = "Audit";
        private const string ERROR = "Error";
        private const string FILE = "File";
        private static string[] _frameworkCookieNames;

        private static string[] _headersToIgnore = new string[]
        {
            "cookie",
            "user-agent",
            "content-type"
        };

        private static bool _initialized = false;

        private IEnvironment _environment;

        private HttpRequestBase _httpRequest;

        #endregion Private Fields

        #region Public Constructors

        public LoggingService(ILoggingDataSource dataSource, IEnvironment environment, HttpRequestBase httpRequest)
        {
            _environment = environment;
            _httpRequest = httpRequest;

            if (!_initialized)
            {
                dataSource.ErrorLogQuery().Any();
                var configPath = environment.MapPath("Areas/Logging/web.config");
                log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));

                SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
                _frameworkCookieNames = new string[] { FormsAuthentication.FormsCookieName.ToLower(), sessionStateSection.CookieName.ToLower(), AntiForgeryConfig.CookieName.ToLower() };

                _initialized = true;
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public string LogScriptErrorUrl
        {
            get
            {
                return "Logging/ErrorLog/LogJavaScriptError";
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void Log(string message, LogLevel level = LogLevel.Debug)
        {
            Log(message, null, level, FILE);
        }

        public void Log(string message, Exception exception, LogLevel level = LogLevel.Debug)
        {
            Log(message, exception, level, FILE);
        }

        public void LogToAnalytics(string entryType, string data)
        {
            SetCommonFields(entryType);
            SetUserIP();
            Log(data, null, LogLevel.Info, ANALYTICS);
        }

        public void LogToAudit(string entryType, string message)
        {
            SetCommonFields(entryType);
            Log(message, null, LogLevel.Info, AUDIT);
        }

        #endregion Public Methods

        #region Private Methods

        private void FillErrorProperties()
        {
            ThreadContext.Properties["userEmail"] = _environment.User.Email;

            if (_httpRequest != null)
            {
                var url = _httpRequest.Url.ToString();
                StringBuilder headers = new StringBuilder();

                foreach (var key in _httpRequest.Headers.AllKeys.Where(k => !_headersToIgnore.Contains(k.ToLower())))
                {
                    if (headers.Length > 0)
                    {
                        headers.Append("\n");
                    }

                    var value = _frameworkCookieNames.Contains(key.ToLower()) ? "Present" : _httpRequest.Headers[key];
                    headers.Append(string.Format("{0}: {1}", key, value));
                }

                var cookies = new StringBuilder();

                foreach (var key in _httpRequest.Cookies.AllKeys)
                {
                    if (cookies.Length > 0)
                    {
                        cookies.Append("\n");
                    }

                    var value = _frameworkCookieNames.Contains(key.ToLower()) ? "Present" : _httpRequest.Cookies[key].Value;
                    cookies.Append(string.Format("{0}: {1}", key, value));
                }

                ThreadContext.Properties["ipAddress"] = _httpRequest.UserHostAddress;
                ThreadContext.Properties["url"] = url.Length < 250 ? url : url.Substring(0, 250);
                ThreadContext.Properties["userAgent"] = _httpRequest.UserAgent;
                ThreadContext.Properties["method"] = _httpRequest.HttpMethod;
                ThreadContext.Properties["contentType"] = _httpRequest.ContentType;
                ThreadContext.Properties["headers"] = headers.ToString();
                ThreadContext.Properties["cookies"] = cookies.ToString();
            }
        }

        private void Log(string message, Exception exception, LogLevel level, string target)
        {
            ILog logger = logger = LogManager.GetLogger(target + "Logger");

            if (logger == null)
            {
                throw new ArgumentException(string.Format("No logger {0} found", target));
            }

            if (target != FILE || level == LogLevel.Error || level == LogLevel.Fatal)
            {
                ThreadContext.Properties["applicationId"] = _environment.Membership.ApplicationId;
                ThreadContext.Properties["userId"] = _environment.User.Id;
            }

            if (level == LogLevel.Error || level == LogLevel.Fatal)
            {
                if (exception != null)
                {
                    var exceptionType = exception.GetType();

                    if (typeof(JavaScriptException).IsAssignableFrom(exceptionType))
                    {
                        message = ((JavaScriptException)exception).GetMessage();
                    }

                    if (exception.InnerException != null)
                    {
                        message = string.Format("{0} Inner exception message: {1}", message, exception.InnerException.Message);
                    }

                    ThreadContext.Properties["exceptionType"] = exceptionType.Name;
                }

                FillErrorProperties();
            }

            switch (level)
            {
                case LogLevel.Debug:
                    {
                        logger.Debug(message, exception);
                    }

                    break;

                case LogLevel.Info:
                    {
                        logger.Info(message, exception);
                    }

                    break;

                case LogLevel.Warning:
                    {
                        logger.Warn(message, exception);
                    }

                    break;

                case LogLevel.Error:
                    {
                        logger.Error(message, exception);
                    }

                    break;

                case LogLevel.Fatal:
                    {
                        logger.Fatal(message, exception);
                    }

                    break;
            }

            if ((level == LogLevel.Error || level == LogLevel.Fatal) && target != ERROR)
            {
                var databaseLogger = LogManager.GetLogger(ERROR + "Logger");

                switch (level)
                {
                    case LogLevel.Error:
                        {
                            databaseLogger.Error(message, exception);
                        }

                        break;

                    case LogLevel.Fatal:
                        {
                            databaseLogger.Fatal(message, exception);
                        }

                        break;
                }
            }
        }

        private void SetCommonFields(string type)
        {
            ThreadContext.Properties["groupId"] = _environment.User.GroupId;
            ThreadContext.Properties["userName"] = _environment.User.Name;
            ThreadContext.Properties["logType"] = type;
        }

        private void SetUserIP()
        {
            if (_httpRequest != null)
            {
                string ipList = _httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];
                string ipAddress;

                if (!string.IsNullOrEmpty(ipList))
                {
                    ipAddress = ipList.Split(',')[0];
                }
                else
                {
                    ipAddress = _httpRequest.ServerVariables["REMOTE_ADDR"];
                }

                ThreadContext.Properties["ipAddress"] = ipAddress;
            }
        }

        #endregion Private Methods
    }
}