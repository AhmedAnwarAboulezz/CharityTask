using System;

namespace Common.StandardInfrastructure.CommonDto
{
    public class UserNotificationDto
    {
        public Guid EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
