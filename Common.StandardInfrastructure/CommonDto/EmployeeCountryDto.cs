using System;

namespace Common.StandardInfrastructure.CommonDto
{
    public class EmployeeCountryDto
    {
        public Guid EmployeeId { get; set; }
        public Guid? CountryId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
