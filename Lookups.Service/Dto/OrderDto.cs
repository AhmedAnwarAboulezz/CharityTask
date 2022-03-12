using Orders.Data.Entities;
using Orders.Service.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Orders.Service.Dto
{
  public  class OrderDto : BaseDto
    {
        public string PhoneNumber { get; set; }
        public float? TotalPrice { get; set; }
        public float? TotalPayment { get; set; }
        public float? Change { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
    public class OrderDetailDto
    {
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public Guid ProductId { get; set; }

    }

    public class ProductDetailDto
    {
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        //public int AmountInStock { get; set; }
        //public int AmountInOrders { get; set; }
        public int RemainAmount { get; set; }


    }
}
