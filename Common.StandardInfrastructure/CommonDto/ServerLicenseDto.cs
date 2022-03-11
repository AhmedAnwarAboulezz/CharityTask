using System.Collections.Generic;

namespace Common.StandardInfrastructure.CommonDto
{
    public class ServerLicenseDto
    {
        public List<string> MacAddress { get; set; }
        public List<string> Processor { get; set; }
        public List<string> MotherBoard { get; set; }
    }

    public class PasswordHashDto
    {
        public string PasswordHash { get; set; }

    }
    public class PasswordConfirmationDto
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
