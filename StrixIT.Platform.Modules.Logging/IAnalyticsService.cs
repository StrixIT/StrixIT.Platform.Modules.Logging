﻿#region Apache License

//-----------------------------------------------------------------------
// <copyright file="IAnalyticsService.cs" company="StrixIT">
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
    /// This interface describes the StrixPlatform analytics service.
    /// </summary>
    public interface IAnalyticsService
    {
        #region Public Methods

        /// <summary>
        /// Gets a query for the platform analytics log.
        /// </summary>
        /// <returns>The query for the analytics log</returns>
        IQueryable<AnalyticsLogEntry> Analytics();

        #endregion Public Methods
    }
}