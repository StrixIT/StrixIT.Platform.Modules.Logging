#region Apache License

//-----------------------------------------------------------------------
// <copyright file="LoggingModuleConfiguration.cs" company="StrixIT">
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
using StrixIT.Platform.Core.Environment;
using System;
using System.Collections.Generic;

namespace StrixIT.Platform.Modules.Logging
{
    public class LoggingModuleConfiguration : IModuleConfiguration
    {
        #region Private Fields

        private static bool? _membershipPresent;

        #endregion Private Fields

        #region Public Constructors

        public LoggingModuleConfiguration(IMembershipSettings membershipSettings)
        {
            if (!_membershipPresent.HasValue)
            {
                _membershipPresent = membershipSettings.ApplicationId != Guid.Empty;
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public IList<ModuleLink> ModuleLinks
        {
            get
            {
                var list = new List<ModuleLink>();
                list.Add(new ModuleLink(Resources.Interface.ErrorLogTitle, LoggingPermissions.ViewErrorLog, "ErrorLog"));

                if (_membershipPresent.Value)
                {
                    list.Add(new ModuleLink(Resources.Interface.AuditLogTitle, LoggingPermissions.ViewAuditLog, "AuditLog"));
                }

                return list;
            }
        }

        public IDictionary<string, IList<string>> ModulePermissions
        {
            get
            {
                var dictionary = new Dictionary<string, IList<string>>();

                var adminPermissions = new List<string>
                {
                    LoggingPermissions.ViewErrorLog,
                    LoggingPermissions.ViewAuditLog,
                    LoggingPermissions.ClearErrorLog
                };

                dictionary.Add(PlatformConstants.ADMINROLE, adminPermissions);
                dictionary.Add(PlatformConstants.GROUPADMINROLE, new List<string> { LoggingPermissions.ViewAuditLog });
                return dictionary;
            }
        }

        public string Name
        {
            get
            {
                return LoggingConstants.LOGGING;
            }
        }

        #endregion Public Properties
    }
}