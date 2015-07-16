//-----------------------------------------------------------------------
// <copyright file="ILoggingDataSource.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;

namespace StrixIT.Platform.Modules.Logging
{
    /// <summary>
    /// The interface for the logging datasource used to read log entries.
    /// </summary>
    public interface ILoggingDataSource
    {
        IQueryable<ErrorLogEntry> ErrorLogQuery();

        IQueryable<AuditLogEntry> AuditLogQuery();

        IQueryable<AnalyticsLogEntry> AnalyticsLogQuery();

        ErrorLogEntry GetErrorLogEntry(long id);

        AuditLogEntry GetAuditLogEntry(long id);
    }
}
