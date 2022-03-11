using System;
using Orders.Service.Dto.Base;
using System.ComponentModel.DataAnnotations;

namespace Orders.Service.Dto
{
    public class CountryDto: BaseDto
    {
        [StringLength(maximumLength: 10, ErrorMessageResourceName = "CountryDto_Code_Length", ErrorMessageResourceType = typeof(Resources.Orders))]
        public string Code { get; set; }

        [StringLength(maximumLength: 200, ErrorMessageResourceName = "CountryDto_NameFl_Length", ErrorMessageResourceType = typeof(Resources.Orders))]
        public string NameFl { get; set; }

        [StringLength(maximumLength: 200, ErrorMessageResourceName = "CountryDto_NameSl_Length", ErrorMessageResourceType = typeof(Resources.Orders))]
        public string NameSl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
