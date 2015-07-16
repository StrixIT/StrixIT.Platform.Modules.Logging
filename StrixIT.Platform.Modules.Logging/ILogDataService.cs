//-----------------------------------------------------------------------
// <copyright file="ILogDataService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System.Collections.Generic;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Modules.Logging
{
    public interface ILogDataService
    {
        IList<ErrorLogListModel> ErrorLogEntries(FilterOptions filter);

        IList<AuditLogListModel> AuditLogEntries(FilterOptions filter);

        ErrorLogEntry GetErrorLogEntry(long id);

        AuditLogEntry GetAuditLogEntry(long id);
    }
}
