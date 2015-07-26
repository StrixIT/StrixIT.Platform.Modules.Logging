#region Apache License

//-----------------------------------------------------------------------
// <copyright file="ILoggingDataSource.cs" company="StrixIT">
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

using System.Linq;

namespace StrixIT.Platform.Modules.Logging
{
    /// <summary>
    /// The interface for the logging datasource used to read log entries.
    /// </summary>
    public interface ILoggingDataSource
    {
        #region Public Methods

        IQueryable<AnalyticsLogEntry> AnalyticsLogQuery();

        IQueryable<AuditLogEntry> AuditLogQuery();

        IQueryable<ErrorLogEntry> ErrorLogQuery();

        AuditLogEntry GetAuditLogEntry(long id);

        ErrorLogEntry GetErrorLogEntry(long id);

        #endregion Public Methods
    }
}