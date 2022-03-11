using Orders.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Data.Entities
{
    public class Order : BaseModel
    {
        public string PhoneNumber { get; set; }
        public float TotalPrice { get; set; }
        public float TotalPayment { get; set; }
        public float Change { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
