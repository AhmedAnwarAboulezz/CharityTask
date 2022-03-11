using Orders.Data.Entities;
using System.Collections.Generic;

namespace Orders.Data.SeedData
{
    public interface IDataInitialize
    {
        IEnumerable<Country> AddCountries();
      
        IEnumerable<Gender> AddGenders();
       
    }
}
