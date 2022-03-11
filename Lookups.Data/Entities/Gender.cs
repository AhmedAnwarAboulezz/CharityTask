using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orders.Data.Entities.Base;

namespace Orders.Data.Entities
{
    public class Gender : BaseModel
    {
        [StringLength(50)]
        public string GenderNameFl { get; set; }
        [StringLength(50)]
        public string GenderNameSl { get; set; }
        public bool IsShown { get; set; }
    }
}
