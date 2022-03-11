using System;

namespace Common.StandardInfrastructure.CommonDto
{
    public class DateDto: DateParameters
    {
        public DateTime EndDate { get; set; }
    }
    public class DateParameters
    {
        public DateTime StartDate { get; set; }
        public Guid? EmployeeId { get; set; }
    }
    public class DateDtoNullable
    {
        public DateTime? LeaveEndDate { get; set; }
        //public DateTime? LeaveStartDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool? IsAbsent { get; set; } = true;
    }
}
