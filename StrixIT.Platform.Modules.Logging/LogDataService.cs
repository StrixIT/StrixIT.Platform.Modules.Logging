#region Apache License

//-----------------------------------------------------------------------
// <copyright file="LogDataService.cs" company="StrixIT">
// Copyright 2015 StrixIT. Author R.G. Schurgers MA MSc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use file except in compliance with the License.
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
using log4net.Appender;
using StrixIT.Platform.Core;
using StrixIT.Platform.Core.Environment;
using StrixIT.Platform.Web;
using System.Collections.Generic;
using System.Linq;

namespace StrixIT.Platform.Modules.Logging
{
    public class LogDataService : ILogDataService
    {
        #region Private Fields

        private ILoggingDataSource _dataSource;

        private IMembershipSettings _membershipSettings;
        private IUserContext _user;

        #endregion Private Fields

        #region Public Constructors

        public LogDataService(ILoggingDataSource dataSource, IUserContext user, IMembershipSettings membershipSettings)
        {
            _dataSource = dataSource;
            _user = user;
            _membershipSettings = membershipSettings;
        }

        #endregion Public Constructors

        #region Public Methods

        public IList<AuditLogListModel> AuditLogEntries(FilterOptions filter)
        {
            FlushAppender("AuditLog");
            SetDefaultSort(filter);
            return _dataSource.AuditLogQuery().Where(a => a.ApplicationId == _membershipSettings.ApplicationId && a.GroupId == _user.GroupId).Filter(filter).Map<AuditLogListModel>().ToList();
        }

        public IList<ErrorLogListModel> ErrorLogEntries(FilterOptions filter)
        {
            FlushAppender("ErrorLog");
            SetDefaultSort(filter);
            var results = _dataSource.ErrorLogQuery().Where(e => e.ApplicationId == _membershipSettings.ApplicationId).Filter(filter).Map<ErrorLogListModel>().ToList();
            results.ForEach(r =>
                {
                    r.Message = GetMessageText(HtmlHelpers.HtmlDecode(r.Message));
                });
            return results;
        }

        public AuditLogEntry GetAuditLogEntry(long id)
        {
            return _dataSource.AuditLogQuery().Where(a => a.ApplicationId == _membershipSettings.ApplicationId && a.GroupId == _user.GroupId).FirstOrDefault(a => a.Id == id);
        }

        public ErrorLogEntry GetErrorLogEntry(long id)
        {
            return _dataSource.ErrorLogQuery().Where(e => e.ApplicationId == _membershipSettings.ApplicationId).FirstOrDefault(e => e.Id == id);
        }

        #endregion Public Methods

        #region Private Methods

        private static void FlushAppender(string appenderName)
        {
            var rep = LogManager.GetRepository();
            var appender = rep.GetAppenders().FirstOrDefault(a => a.GetType().Equals(typeof(AdoNetAppender)) && a.Name == appenderName) as AdoNetAppender;

            if (appender != null)
            {
                appender.Flush();
            }
        }

        private static string GetMessageText(string message)
        {
            return new JavaScriptException(message).GetMessage();
        }

        private static void SetDefaultSort(FilterOptions options)
        {
            if (options.Sort.IsEmpty())
            {
                options.Sort.Add(new SortField { Field = "LogDateTime", Dir = "desc" });
            }
        }

        #endregion Private Methods
    }
}