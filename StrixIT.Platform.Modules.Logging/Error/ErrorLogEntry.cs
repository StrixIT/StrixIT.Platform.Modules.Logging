//-----------------------------------------------------------------------
// <copyright file="ErrorLogEntry.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StrixIT.Platform.Core;

namespace StrixIT.Platform.Modules.Logging
{
    public class ErrorLogEntry
    {
        public long Id { get; set; }

        [StringLength(100)]
        public string ExceptionType { get; set; }

        [StrixNotDefault]
        public Guid ApplicationId { get; set; }

        [StringLength(250)]
        public string UserEmail { get; set; }

        [StrixNotDefault]
        public DateTime LogDateTime { get; set; }

        [StringLength(10)]
        [StrixRequired(AllowEmptyStrings = false)]
        public string Level { get; set; }

        [AllowHtml]
        [StringLength(500)]
        [StrixRequired(AllowEmptyStrings = false)]
        public string Message { get; set; }

        [StringLength(4000)]
        public string Exception { get; set; }

        [StringLength(40)]
        public string IPAddress { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(100)]
        public string UserAgent { get; set; }

        [StringLength(10)]
        public string Method { get; set; }

        [StringLength(50)]
        public string ContentType { get; set; }

        [StringLength(1000)]
        public string Headers { get; set; }

        [StringLength(1000)]
        public string Cookies { get; set; }
    }
}