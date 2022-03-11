using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orders.Data.Entities.Base;

namespace Orders.Data.Entities
{
    public class Country : BaseModel
    {
        [StringLength(10)]
        public string Code { get; set; }
        [StringLength(200)]
        public string NameFl { get; set; }
        [StringLength(200)]
        public string NameSl { get; set; }




    }
}
