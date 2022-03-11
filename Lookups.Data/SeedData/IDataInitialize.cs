using Orders.Data.Entities;
using System.Collections.Generic;

namespace Orders.Data.SeedData
{
    public interface IDataInitialize
    {

        IEnumerable<Product> AddProducts();
        IEnumerable<ProductType> AddProductTypes();


    }
}
