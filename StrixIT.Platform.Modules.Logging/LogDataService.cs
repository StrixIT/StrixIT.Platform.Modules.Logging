//-----------------------------------------------------------------------
// <copyright file="LogDataService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using log4net;
using log4net.Appender;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Modules.Logging
{
    public class LogDataService : ILogDataService
    {
        private ILoggingDataSource _dataSource;

        public LogDataService(ILoggingDataSource dataSource)
        {
            this._dataSource = dataSource;
        }

        public IList<ErrorLogListModel> ErrorLogEntries(FilterOptions filter)
        {
            FlushAppender("ErrorLog");
            SetDefaultSort(filter);
            var results = this._dataSource.ErrorLogQuery().Where(e => e.ApplicationId == StrixPlatform.ApplicationId).Filter(filter).Map<ErrorLogListModel>().ToList();
            results.ForEach(r => 
                {
                    r.Message = GetMessageText(Web.Helpers.HtmlDecode(r.Message));
                });
            return results;
        }

        public IList<AuditLogListModel> AuditLogEntries(FilterOptions filter)
        {
            FlushAppender("AuditLog");
            SetDefaultSort(filter);
            return this._dataSource.AuditLogQuery().Where(a => a.ApplicationId == StrixPlatform.ApplicationId && a.GroupId == StrixPlatform.User.GroupId).Filter(filter).Map<AuditLogListModel>().ToList();
        }

        public ErrorLogEntry GetErrorLogEntry(long id)
        {
            return this._dataSource.ErrorLogQuery().Where(e => e.ApplicationId == StrixPlatform.ApplicationId).FirstOrDefault(e => e.Id == id);
        }

        public AuditLogEntry GetAuditLogEntry(long id)
        {
            return this._dataSource.AuditLogQuery().Where(a => a.ApplicationId == StrixPlatform.ApplicationId && a.GroupId == StrixPlatform.User.GroupId).FirstOrDefault(a => a.Id == id);
        }

        private static void SetDefaultSort(FilterOptions options)
        {
            if (options.Sort.IsEmpty())
            {
                options.Sort.Add(new SortField { Field = "LogDateTime", Dir = "desc" });
            }
        }

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
    }
}
