using Common.StandardInfrastructure.CommonDto;
using System;


namespace Orders.Service.FilterDto
{
    public class ProductFilterDto
    {
        public DynamicFilterDto<string> Code { get; set; }
        public DynamicFilterDto<string> NameFl { get; set; }
        public DynamicFilterDto<string> NameSl { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
