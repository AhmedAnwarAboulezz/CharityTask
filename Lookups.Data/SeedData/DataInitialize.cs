using Common.StandardInfrastructure;
using Common.StandardInfrastructure.Utility;
using Orders.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Data.SeedData
{
    public class DataInitialize : IDataInitialize
    {

       
        public IEnumerable<ProductType> AddProductTypes()
        {
            var enums = Enum.GetValues(typeof(ProductTypeEnum));
            return (from object enumItem in enums
                    select new ProductType
                    {
                        Id = (int)((ProductTypeEnum)enumItem),
                        NameFl = ((ProductTypeEnum)enumItem).GetName(true)[0],
                        NameSl = ((ProductTypeEnum)enumItem).GetName(true)[1]                        
                    }).ToList();
        }

        public IEnumerable<Product> AddProducts()
        {
            var dataText = System.IO.File.ReadAllText(@"seed/Products.json");
            var data = Seeder<IEnumerable<Product>>.Seedit(dataText).Select(SetRemainAmountInStock);
            return data;
        }
        private Product SetRemainAmountInStock(Product product)
        {
            product.RemainAmountInStock = 0;
            return product;
        }




    }
}
