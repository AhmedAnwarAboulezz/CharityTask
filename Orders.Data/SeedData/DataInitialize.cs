using Common.StandardInfrastructure;
using Orders.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Data.SeedData
{
    public class DataInitialize : IDataInitialize
    {

        public IEnumerable<Country> AddCountries()
        {
            return new List<Country>()
            {
                new Country{ Id = new Guid("5C60F693-BEF5-E011-A485-80EE7300C695"),NameSl="مصر", NameFl = "Egypt"},
                new Country{ Id = new Guid("5C60F693-BEF5-E011-A485-80EE7300C696"), NameSl = "الكويت", NameFl = "Kuwait"}
            };
        }
       
        public IEnumerable<Gender> AddGenders()
        {
            var enums = Enum.GetValues(typeof(GenderEnum));
            return (from object enumItem in enums
                    select new Gender
                    {
                        Id = ((GenderEnum)enumItem).GetEnumGuid(),
                        GenderNameFl = ((GenderEnum)enumItem).GetName(true)[0],
                        GenderNameSl = ((GenderEnum)enumItem).GetName(true)[1],
                        IsShown = ((GenderEnum)enumItem).GetEnumGuid() == GenderEnum.Both.GetEnumGuid() ? false : true
                    }).ToList();


        }

       

    }
}
