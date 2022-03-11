using System;
using Audit.Core;

namespace Common.StandardInfrastructure
{
    public class AuditCustomEvent : AuditEvent
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int ActionID { get; set; }
        public string EmployeeIds { get; set; }
    }
}
