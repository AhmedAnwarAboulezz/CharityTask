using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Common.StandardInfrastructure.CommonDto
{
    public class EmployeeFullDataDto : BaseReportDto
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeFullDataDto()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

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
       // public Guid? ServiceStatusId { get; set; }
        public string ServiceNameFl { get; set; } = "";
        public string ServiceNameSl { get; set; } = "";
        public Guid CountryId { get; set; }
        public Guid ReligionId { get; set; }
        public string StartDateStr => StartDate.ToString(_httpContextAccessor.HttpContext.Request.Headers["lang"] == "ar-EG" ? "yyyy/MM/dd" : "dd/MM/yyyy");

        public IEnumerable<EmployeeLocationDataDto> EmployeeLocations { get; set; }
    }
    public class EmployeeLocationDataDto
    {
        public Guid LocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
