using System;

namespace Common.StandardInfrastructure.CommonDto
{
    public class DeviceCommonDto
    {
        public Guid Id { get; set; }
        public string Treminal_IP { get; set; }
        public string SerialNumber { get; set; }
        public string DescriptionFl { get; set; }
        public string DescriptionSl { get; set; }
        public Guid? OrganizationId { get; set; }
    }
}
