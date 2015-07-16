//-----------------------------------------------------------------------
// <copyright file="ErrorLogController.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using StrixIT.Platform.Core;
using StrixIT.Platform.Web;

namespace StrixIT.Platform.Modules.Logging
{
    [StrixAuthorization(Permissions = LoggingPermissions.ViewErrorLog)]
    public class ErrorLogController : BaseController
    {
        private ILogDataService _dataService;

        public ErrorLogController(ILogDataService dataService)
        {
            this._dataService = dataService;
        }

        public ActionResult Index()
        {
            var config = new ListConfiguration(typeof(ErrorLogListModel), new string[] { "ExceptionType", "UserEmail" });
            config.Fields.Insert(0, new ListFieldConfiguration("LogDateTime", "kendoDateTime") { ShowFilter = false });
            config.Fields.Add(new ListFieldConfiguration("Message") { DisplayHtml = true });
            config.HideNameColumn = true;
            config.InterfaceResourceType = typeof(Resources.Interface);
            return this.View(config);
        }

        [HttpPost]
        public JsonResult List(FilterOptions options)
        {
            var entries = this._dataService.ErrorLogEntries(options);
            return this.Json(entries.DataRecords(options));
        }

        public ActionResult Details()
        {
            return this.View();
        }

        [HttpPost]
        public JsonResult Get(long id)
        {
            return this.Json(this._dataService.GetErrorLogEntry(id));
        }

        [HttpPost]
        [ValidateInput(false)]
        [AllowAnonymous]
        public void LogJavaScriptError(ErrorLogEntry entry)
        {
            var message = Web.Helpers.HtmlEncode(entry.Message);
            var exception = new JavaScriptException(message);
            Logger.Log(exception.Message, exception, LogLevel.Error);
        }
    }
}