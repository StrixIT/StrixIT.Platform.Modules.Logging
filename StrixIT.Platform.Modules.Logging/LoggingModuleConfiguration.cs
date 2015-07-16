//-----------------------------------------------------------------------
// <copyright file="LoggingModuleConfiguration.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using StrixIT.Platform.Core;
using StrixIT.Platform.Web;

namespace StrixIT.Platform.Modules.Logging
{
    public class LoggingModuleConfiguration : IModuleConfiguration
    {
        private static bool _membershipPresent = DependencyInjector.TryGet<IMembershipService>() != null;

        public string Name
        {
            get
            {
                return LoggingConstants.LOGGING;
            }
        }

        public IList<ModuleLink> ModuleLinks
        {
            get
            {
                var list = new List<ModuleLink>();
                list.Add(new ModuleLink(Resources.Interface.ErrorLogTitle, LoggingPermissions.ViewErrorLog, "ErrorLog"));

                if (_membershipPresent)
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
    }
}
