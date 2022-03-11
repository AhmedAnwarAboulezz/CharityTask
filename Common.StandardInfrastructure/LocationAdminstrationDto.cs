using System;
using System.Collections.Generic;

namespace Common.StandardInfrastructure
{
   public class LocationAdminstrationDto
    {
       public IEnumerable<Guid> AdminstrationId { get; set; }
       public IEnumerable<Guid> LocationId { get; set; }
       public bool? IsSuperAdmin { get; set; } = false;
    }
}
