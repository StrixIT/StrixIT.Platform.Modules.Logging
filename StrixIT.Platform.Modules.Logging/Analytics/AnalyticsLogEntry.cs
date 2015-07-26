#region Apache License

//-----------------------------------------------------------------------
// <copyright file="AnalyticsLogEntry.cs" company="StrixIT">
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
using System;
using System.ComponentModel.DataAnnotations;

namespace StrixIT.Platform.Modules.Logging
{
    public class AnalyticsLogEntry
    {
        #region Public Properties

        [StrixNotDefault]
        public Guid ApplicationId { get; set; }

        [StrixNotDefault]
        public Guid GroupId { get; set; }

        public long Id { get; set; }

        [StringLength(4000)]
        [StrixRequired]
        public string LogData { get; set; }

        [StrixNotDefault]
        public DateTime LogDateTime { get; set; }

        [StringLength(50)]
        [StrixRequired]
        public string LogType { get; set; }

        [StrixNotDefault]
        public Guid? UserId { get; set; }

        [StringLength(250)]
        public string UserName { get; set; }

        #endregion Public Properties
    }
}