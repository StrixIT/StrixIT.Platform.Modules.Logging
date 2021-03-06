﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="AuditLogController.cs" company="StrixIT">
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

using StrixIT.Platform.Core;
using StrixIT.Platform.Web;
using System.Web.Mvc;

namespace StrixIT.Platform.Modules.Logging
{
    [StrixAuthorization(Permissions = LoggingPermissions.ViewAuditLog)]
    public class AuditLogController : BaseController
    {
        #region Private Fields

        private ILogDataService _dataService;

        #endregion Private Fields

        #region Public Constructors

        public AuditLogController(ILogDataService dataService)
        {
            this._dataService = dataService;
        }

        #endregion Public Constructors

        #region Public Methods

        public ActionResult Details()
        {
            return this.View();
        }

        [HttpPost]
        public JsonResult Get(long id)
        {
            return this.Json(this._dataService.GetAuditLogEntry(id));
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

        #endregion Public Methods
    }
}