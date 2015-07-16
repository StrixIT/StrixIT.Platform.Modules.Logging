//-----------------------------------------------------------------------
// <copyright file="AnalyticsService.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//---------------------------------------------------------------------
using System.Linq;

namespace StrixIT.Platform.Modules.Logging
{
    public class AnalyticsService
    {
        private ILoggingDataSource _source;

        public AnalyticsService(ILoggingDataSource source)
        {
            this._source = source;
        }

        public IQueryable<AnalyticsLogEntry> Analytics()
        {
            return this._source.AnalyticsLogQuery();
        }
    }
}