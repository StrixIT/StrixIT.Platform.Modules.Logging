//-----------------------------------------------------------------------
// <copyright file="LoggingDataSource.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Data.Entity;
using System.Linq;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Modules.Logging
{
    public class LoggingDataSource : EntityFrameworkDataSource, ILoggingDataSource
    {
        public LoggingDataSource() : base(LoggingConstants.LOGGING)
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ErrorLogEntry> ErrorLog { get; set; }

        public DbSet<AuditLogEntry> AuditLog { get; set; }

        public DbSet<AnalyticsLogEntry> AnalyticsLog { get; set; }

        public IQueryable<ErrorLogEntry> ErrorLogQuery()
        {
            return this.ErrorLog;
        }

        public IQueryable<AuditLogEntry> AuditLogQuery()
        {
            return this.AuditLog;
        }

        public IQueryable<AnalyticsLogEntry> AnalyticsLogQuery()
        {
            return this.AnalyticsLog;
        }

        public ErrorLogEntry GetErrorLogEntry(long id)
        {
            return this.ErrorLog.FirstOrDefault(l => l.Id == id);
        }

        public AuditLogEntry GetAuditLogEntry(long id)
        {
            return this.AuditLog.FirstOrDefault(l => l.Id == id);
        }

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
    }
}