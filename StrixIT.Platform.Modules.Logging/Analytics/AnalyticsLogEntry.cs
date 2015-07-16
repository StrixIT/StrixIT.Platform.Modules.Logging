//-----------------------------------------------------------------------
// <copyright file="AnalyticsLogEntry.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Modules.Logging
{
    public class AnalyticsLogEntry
    {
        public long Id { get; set; }

        [StrixNotDefault]
        public Guid ApplicationId { get; set; }

        [StrixNotDefault]
        public Guid GroupId { get; set; }

        [StrixNotDefault]
        public Guid? UserId { get; set; }

        [StringLength(250)]
        public string UserName { get; set; }

        [StringLength(50)]
        [StrixRequired]
        public string LogType { get; set; }

        [StrixNotDefault]
        public DateTime LogDateTime { get; set; }

        [StringLength(4000)]
        [StrixRequired]
        public string LogData { get; set; }
    }
}