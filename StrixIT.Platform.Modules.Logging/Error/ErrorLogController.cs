#region Apache License
//-----------------------------------------------------------------------
// <copyright file="ErrorLogController.cs" company="StrixIT">
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
#endregion

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