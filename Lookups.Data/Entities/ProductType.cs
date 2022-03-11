using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Data.Entities
{
    public class ProductType
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string NameFl { get; set; }
        [StringLength(50)]
        public string NameSl { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
