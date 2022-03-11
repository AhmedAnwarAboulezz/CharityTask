using System;

namespace Common.StandardInfrastructure.CommonDto
{
    public class AdministrationFilterDto
    {
        public Guid? EmployeeId { get; set; }
        public Guid?[] AdministrativeLevels { get; set; }
        //public DateTime? DayDate { get; set; } = DateTime.Now;
        public string EmployeeData { get; set; } = "";

        public DateTime? StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; } = DateTime.Now;


    }
}

