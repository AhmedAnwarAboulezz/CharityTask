using System;
using System.Collections.Generic;

namespace Common.StandardInfrastructure.CommonDto
{
    public class ReportFilterDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ServiceStatusId { get; set; }
        public long? StatusId { get; set; }
        public List<Guid?> NationalityIds { get; set; }
        public int? AbsenceTypeId { get; set; }
        public int? AbsenceCount { get; set; }
        public int? TotalLate { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? JobId { get; set; }
        public Guid? DutyTypeId { get; set; }
        public Guid? DutyId { get; set; }
        public Guid? ContractId { get; set; }
        public Guid? QualificationId { get; set; }
        public List<Guid?> EmployeeId { get; set; }
        public string GroupBy { get; set; }
        public string GroupBy1 { get; set; }
        public bool? IsWeekend { get; set; }
        public bool? IsRestday { get; set; }
        public bool? IsHoliday { get; set; }
        public bool? IsPiChart { get; set; }
        public bool? IsPaged { get; set; }
        public List<Guid?> AdminstrativeLevels { get; set; }
        public string ReportName { get; set; }
        public Guid? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationLogo { get; set; }
        public int PrintType { get; set; }
        public List<long?> StatusIdList { get; set; }
        public List<Guid?> LeaveTypeId { get; set; }
        public List<Guid?> PartialPermissionTypeId { get; set; }
        public List<Guid?> FullDayPermissionTypeId { get; set; }
        public List<Guid?> AllowanceTypeId { get; set; }
        public List<Guid?> RequestById { get; set; }
        public bool? IsUnPaidLeave { get; set; }
        public bool? IsExemptionSign { get; set; }
        public int? Year { get; set; }
    }
}
