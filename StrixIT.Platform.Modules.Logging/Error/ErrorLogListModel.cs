﻿#region Apache License
//-----------------------------------------------------------------------
// <copyright file="ErrorLogListModel.cs" company="StrixIT">
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

namespace StrixIT.Platform.Modules.Logging
{
    public class ErrorLogListModel
    {
        public long Id { get; set; }

        public string ExceptionType { get; set; }

        public string UserEmail { get; set; }

        public DateTime LogDateTime { get; set; }

        public string Message { get; set; }
    }
}
