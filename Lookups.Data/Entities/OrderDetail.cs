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
        public Guid OrderId { get; set; }
        public  virtual Order Order { get; set; }
        public int Amount { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
