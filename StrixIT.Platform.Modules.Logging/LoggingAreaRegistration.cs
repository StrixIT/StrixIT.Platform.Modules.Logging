#region Apache License

//-----------------------------------------------------------------------
// <copyright file="LoggingAreaRegistration.cs" company="StrixIT">
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
using StrixIT.Platform.Core.Environment;
using StrixIT.Platform.Web;
using System;
using System.Web.Mvc;

namespace StrixIT.Platform.Modules.Logging
{
    public class LoggingAreaRegistration : AreaRegistration
    {
        #region Public Properties

        public override string AreaName
        {
            get
            {
                return LoggingConstants.LOGGING;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var culture = DependencyInjector.Get<ICultureService>().DefaultCultureCode.ToLower();

            context.MapLocalizedRoute(
                LoggingConstants.LOGGING,
                "{language}/Admin/Logging/{controller}/{action}/{id}",
                new { language = culture, controller = LoggingConstants.LOGGING, action = MvcConstants.INDEX, id = UrlParameter.Optional });
        }

        #endregion Public Methods
    }
}