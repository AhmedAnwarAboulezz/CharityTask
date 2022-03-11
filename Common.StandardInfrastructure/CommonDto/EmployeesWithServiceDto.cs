using System;

namespace Common.StandardInfrastructure.CommonDto
{
    public class EmployeesWithServiceDto : BaseReportDto
    {
        public Guid JobDegreeId { get; set; }
        public string JobDegreeFl { get; set; } = "";
        public string JobDegreeSl { get; set; } = "";
        public DateTime DateOfHiring { get; set; }
        public string Email { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } = DateTime.Now;
        public Guid NationalId { get; set; }
        public string NationalityFl { get; set; } = "";
        public string NationalitySl { get; set; } = "";
        public Guid GenderId { get; set; }
        public string GenderNameFl { get; set; } = "";
        public string GenderNameSl { get; set; } = "";
        //public Guid ServiceStatusId { get; set; }
        public string ServiceNameFl { get; set; } = "";
        public string ServiceNameSl { get; set; } = "";
        public Guid CountryId { get; set; }
        public Guid ReligionId { get; set; }
    }
}
