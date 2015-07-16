//-----------------------------------------------------------------------
// <copyright file="JavaScriptException.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Modules.Logging
{
    [Serializable]
    public class JavaScriptException : Exception
    {
        public JavaScriptException(string message) : base(message) { }

        public string GetMessage()
        {
            var message = this.Message;
            var messageEnd = message.Contains("At url: ") ? message.IndexOf("At url: ") : message.Length;
            return message.Substring(0, messageEnd);
        }
    }
}
