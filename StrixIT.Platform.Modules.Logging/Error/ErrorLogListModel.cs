//-----------------------------------------------------------------------
// <copyright file="ErrorLogListModel.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
