using Orders.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Data.Entities
{
    public class OrderDetail : BaseModel
    {
        public int OrderId { get; set; }
        public  virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
