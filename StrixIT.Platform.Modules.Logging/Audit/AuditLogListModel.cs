//-----------------------------------------------------------------------
// <copyright file="AuditLogListModel.cs" company="StrixIT">
//     Author: R.G. Schurgers MA MSc. Copyright (c) StrixIT. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace StrixIT.Platform.Modules.Logging
{
    public class AuditLogListModel
    {
        public long Id { get; set; }

        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public string LogType { get; set; }

        public DateTime LogDateTime { get; set; }

        public string Message { get; set; }
    }
}
