//-----------------------------------------------------------------------
// <copyright file="IAnalyticsService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System.Linq;

namespace StrixIT.Platform.Modules.Logging
{
    /// <summary>
    /// This interface describes the StrixPlatform analytics service.
    /// </summary>
    public interface IAnalyticsService
    {
        /// <summary>
        /// Gets a query for the platform analytics log.
        /// </summary>
        /// <returns>The query for the analytics log</returns>
        IQueryable<AnalyticsLogEntry> Analytics();
    }
}