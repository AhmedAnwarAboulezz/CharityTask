using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Orders.Data.Entities.Base;

namespace Orders.Data.Entities
{
    public class Product : BaseModel
    {
        [StringLength(10)]
        public string Code { get; set; }
        [StringLength(200)]
        public string NameFl { get; set; }
        [StringLength(200)]
        public string NameSl { get; set; }
        public float Price { get; set; }
        [StringLength(5)]
        public string Currency { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int AmountInStock { get; set; }
        public int? RemainAmountInStock { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
