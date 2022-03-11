using System;

namespace Common.StandardInfrastructure.CommonDto
{
    public class WorkflowDropdownFilterDto
    {
        public Guid RequestTypId { get; set; }
        public Guid? CurrentAdministrationId { get; set; }
        public DateTime? DayDate { get; set; } = DateTime.Now.Date;


    }
}
