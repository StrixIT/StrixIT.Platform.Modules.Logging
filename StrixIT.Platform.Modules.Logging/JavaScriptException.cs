#region Apache License

//-----------------------------------------------------------------------
// <copyright file="JavaScriptException.cs" company="StrixIT">
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

using System;

namespace StrixIT.Platform.Modules.Logging
{
    [Serializable]
    public class JavaScriptException : Exception
    {
        #region Public Constructors

        public JavaScriptException(string message) : base(message)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public string GetMessage()
        {
            var message = this.Message;
            var messageEnd = message.Contains("At url: ") ? message.IndexOf("At url: ") : message.Length;
            return message.Substring(0, messageEnd);
        }

        #endregion Public Methods
    }
}