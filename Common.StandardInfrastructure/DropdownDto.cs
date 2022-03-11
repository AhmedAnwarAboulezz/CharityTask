using System;

namespace Common.StandardInfrastructure
{
    public class DropdownDto
    {
        public Guid Id { get; set; }
        public string NameFl { get; set; }
        public string NameSl { get; set; }
        public string CivilId { get; set; }
        public string EmployeeNumber { get; set; }

        public bool IsSelected { get; set; }
        public bool? IsAllowed { get; set; }
        public string Email { get; set; }
    }
}
