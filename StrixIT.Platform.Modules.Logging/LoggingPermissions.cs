﻿#region Apache License
//-----------------------------------------------------------------------
// <copyright file="LoggingPermissions.cs" company="StrixIT">
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

namespace StrixIT.Platform.Modules.Logging
{
    public static class LoggingPermissions
    {
        public const string ViewErrorLog = "StrixIT.Logging.ViewErrorLog";
        public const string ViewAuditLog = "StrixIT.Logging.ViewAuditLog";
        public const string ClearErrorLog = "StrixIT.Logging.ClearErrorLog";
    }
}
