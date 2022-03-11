using System;

namespace Common.StandardInfrastructure.CommonDto
{
    public class BaseReportDto
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeNumber { get; set; } = "";
        public string EmployeeNameFl { get; set; } = "";
        public string EmployeeNameSl { get; set; } = "";
        public string CivilId { get; set; } = "";
        public string DepartmentId { get; set; } = "";
        public string DepartmentFl { get; set; } = "";
        public string DepartmentSl { get; set; } = "";
        public Guid AdminLevelId { get; set; }
        public string AdminLevelFl { get; set; } = "";
        public string AdminLevelSl { get; set; } = "";
        public Guid LocationId { get; set; }
        public string LocationFl { get; set; } = "";
        public string LocationSl { get; set; } = "";
        public Guid JobId { get; set; }
        public string JobNameFl { get; set; } = "";
        public string JobNameSl { get; set; } = "";
        public Guid ContractTypeId { get; set; }
        public string ContractTypeFl { get; set; } = "";
        public string ContractTypeSl { get; set; } = "";
        public Guid QualificationId { get; set; }
        public string QualificationTypeFl { get; set; } = "";
        public string QualificationTypeSl { get; set; } = "";
    }
}
