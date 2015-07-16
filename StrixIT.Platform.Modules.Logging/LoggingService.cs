//-----------------------------------------------------------------------
// <copyright file="LoggingService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Security;
using log4net;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Modules.Logging
{
    /// <summary>
    /// The default implementation of the logger interface.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private const string FILE = "File";
        private const string ERROR = "Error";
        private const string AUDIT = "Audit";
        private const string ANALYTICS = "Analytics";
        private static bool _initialized = false;
        private static string[] _frameworkCookieNames;
        private static string[] _headersToIgnore = new string[]
        {
            "cookie",
            "user-agent",
            "content-type"
        };

        public LoggingService(ILoggingDataSource dataSource)
        {
            if (!_initialized)
            {
                dataSource.ErrorLogQuery().Any();
                var configPath = StrixPlatform.Environment.MapPath("Areas/Logging/web.config");
                log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));

                SessionStateSection sessionStateSection = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
                _frameworkCookieNames = new string[] { FormsAuthentication.FormsCookieName.ToLower(), sessionStateSection.CookieName.ToLower(), AntiForgeryConfig.CookieName.ToLower() };

                _initialized = true;
            }
        }

        public string LogScriptErrorUrl
        {
            get
            {
                return "Logging/ErrorLog/LogJavaScriptError";
            }
        }

        public void Log(string message, LogLevel level = LogLevel.Debug)
        {
            Log(message, null, level, FILE);
        }

        public void Log(string message, Exception exception, LogLevel level = LogLevel.Debug)
        {
            Log(message, exception, level, FILE);
        }

        public void LogToAudit(string entryType, string message)
        {
            SetCommonFields(entryType);
            Log(message, null, LogLevel.Info, AUDIT);
        }

        public void LogToAnalytics(string entryType, string data)
        {
            SetCommonFields(entryType);
            Log(data, null, LogLevel.Info, ANALYTICS);
        }

        private static void SetCommonFields(string type)
        {
            log4net.ThreadContext.Properties["groupId"] = StrixPlatform.User.GroupId;
            log4net.ThreadContext.Properties["userName"] = StrixPlatform.User.Name;
            log4net.ThreadContext.Properties["logType"] = type;
        }

        private static void Log(string message, Exception exception, LogLevel level, string target)
        {
            ILog logger = logger = LogManager.GetLogger(target + "Logger");

            if (logger == null)
            {
                throw new ArgumentException(string.Format("No logger {0} found", target));
            }

            if (target != FILE || level == LogLevel.Error || level == LogLevel.Fatal)
            {
                log4net.ThreadContext.Properties["applicationId"] = StrixPlatform.ApplicationId;
                log4net.ThreadContext.Properties["userId"] = StrixPlatform.User.Id;
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

                    log4net.ThreadContext.Properties["exceptionType"] = exceptionType.Name;
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

        private static void FillErrorProperties()
        {
            log4net.ThreadContext.Properties["userEmail"] = StrixPlatform.Environment.CurrentUserEmail;
            var context = HttpContext.Current;

            if (context != null)
            {
                var request = context.Request;
                var url = request.Url.ToString();
                StringBuilder headers = new StringBuilder();

                foreach (var key in request.Headers.AllKeys.Where(k => !_headersToIgnore.Contains(k.ToLower())))
                {
                    if (headers.Length > 0)
                    {
                        headers.Append("\n");
                    }

                    var value = _frameworkCookieNames.Contains(key.ToLower()) ? "Present" : request.Headers[key];
                    headers.Append(string.Format("{0}: {1}", key, value));
                }

                var cookies = new StringBuilder();

                foreach (var key in request.Cookies.AllKeys)
                {
                    if (cookies.Length > 0)
                    {
                        cookies.Append("\n");
                    }

                    var value = _frameworkCookieNames.Contains(key.ToLower()) ? "Present" : request.Cookies[key].Value;
                    cookies.Append(string.Format("{0}: {1}", key, value));
                }

                log4net.ThreadContext.Properties["ipAddress"] = request.UserHostAddress;
                log4net.ThreadContext.Properties["url"] = url.Length < 250 ? url : url.Substring(0, 250);
                log4net.ThreadContext.Properties["userAgent"] = request.UserAgent;
                log4net.ThreadContext.Properties["method"] = request.HttpMethod;
                log4net.ThreadContext.Properties["contentType"] = request.ContentType;
                log4net.ThreadContext.Properties["headers"] = headers.ToString();
                log4net.ThreadContext.Properties["cookies"] = cookies.ToString();
            }
        }
    }
}