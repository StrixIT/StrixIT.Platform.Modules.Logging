#region Apache License

//-----------------------------------------------------------------------
// <copyright file="LoggingDataSource.cs" company="StrixIT">
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
using StrixIT.Platform.Framework;
using System;
using System.Data.Entity;
using System.Linq;

namespace StrixIT.Platform.Modules.Logging
{
    public class LoggingDataSource : EntityFrameworkDataSource, ILoggingDataSource
    {
        #region Public Constructors

        public LoggingDataSource(IConfiguration config) : base(config, LoggingConstants.LOGGING)
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<AnalyticsLogEntry> AnalyticsLog { get; set; }
        public DbSet<AuditLogEntry> AuditLog { get; set; }
        public DbSet<ErrorLogEntry> ErrorLog { get; set; }

        #endregion Public Properties

        #region Public Methods

        public IQueryable<AnalyticsLogEntry> AnalyticsLogQuery()
        {
            return this.AnalyticsLog;
        }

        public IQueryable<AuditLogEntry> AuditLogQuery()
        {
            return this.AuditLog;
        }

        public IQueryable<ErrorLogEntry> ErrorLogQuery()
        {
            return this.ErrorLog;
        }

        public AuditLogEntry GetAuditLogEntry(long id)
        {
            return this.AuditLog.FirstOrDefault(l => l.Id == id);
        }

        public ErrorLogEntry GetErrorLogEntry(long id)
        {
            return this.ErrorLog.FirstOrDefault(l => l.Id == id);
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            modelBuilder.Entity<ErrorLogEntry>().ToTable("ErrorLog");
            modelBuilder.Entity<AuditLogEntry>().ToTable("AuditLog");
            modelBuilder.Entity<AnalyticsLogEntry>().ToTable("AnalyticsLog");
        }

        #endregion Protected Methods
    }
}