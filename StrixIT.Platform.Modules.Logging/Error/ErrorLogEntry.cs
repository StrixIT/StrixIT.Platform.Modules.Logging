#region Apache License
//-----------------------------------------------------------------------
// <copyright file="ErrorLogEntry.cs" company="StrixIT">
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