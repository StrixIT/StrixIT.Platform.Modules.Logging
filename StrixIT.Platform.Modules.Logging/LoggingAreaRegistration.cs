//-----------------------------------------------------------------------
// <copyright file="LoggingAreaRegistration.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web.Mvc;
using StrixIT.Platform.Core;
using StrixIT.Platform.Web;

namespace StrixIT.Platform.Modules.Logging
{
    public class LoggingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return LoggingConstants.LOGGING;
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var culture = StrixPlatform.DefaultCultureCode.ToLower();

            context.MapLocalizedRoute(
                LoggingConstants.LOGGING,
                "{language}/Admin/Logging/{controller}/{action}/{id}",
                new { language = culture, controller = LoggingConstants.LOGGING, action = MvcConstants.INDEX, id = UrlParameter.Optional });
        }
    }
}