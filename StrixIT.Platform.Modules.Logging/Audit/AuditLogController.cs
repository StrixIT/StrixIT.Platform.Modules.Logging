//-----------------------------------------------------------------------
// <copyright file="AuditLogController.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using StrixIT.Platform.Web;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Modules.Logging
{
    [StrixAuthorization(Permissions = LoggingPermissions.ViewAuditLog)]
    public class AuditLogController : BaseController
    {
        private ILogDataService _dataService;

        public AuditLogController(ILogDataService dataService)
        {
            this._dataService = dataService;
        }

        public ActionResult Index()
        {
            var config = new ListConfiguration(typeof(AuditLogListModel), new string[] { "UserName", "LogType", "Message" });
            config.Fields.Insert(2, new ListFieldConfiguration("LogDateTime", "kendoDateTime") { ShowFilter = false });
            config.HideNameColumn = true;
            config.InterfaceResourceType = typeof(Resources.Interface);
            return this.View(config);
        }

        [HttpPost]
        public JsonResult List(FilterOptions options)
        {
            var entries = this._dataService.AuditLogEntries(options);
            return this.Json(entries.DataRecords(options));
        }

        public ActionResult Details()
        {
            return this.View();
        }

        [HttpPost]
        public JsonResult Get(long id)
        {
            return this.Json(this._dataService.GetAuditLogEntry(id));
        }
    }
}