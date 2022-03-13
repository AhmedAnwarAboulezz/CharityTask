using System;
using Orders.Service.Dto.Base;

namespace Orders.Service.Dto
{
    public class ProductDto: BaseDto
    {
        public string Code { get; set; }
        public string NameFl { get; set; }
        public string NameSl { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Price { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int? AmountInStock { get; set; }
        public int? RemainInStock { get; set; }
    }

    public class ProductResetDto : BaseDto
    {
        public int? ProductTypeId { get; set; }
        public int AddedAmount { get; set; }
    }

}
