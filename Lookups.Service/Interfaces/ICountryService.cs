using Common.StandardInfrastructure;
using Orders.Service.Dto;
using Orders.Service.FilterDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Service.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetAll();
        Task<PagedListDto<CountryDto>> GetAllCountriesPaged(CountryFilterDto filteringDto, PagingSortingDto pagingSortingDto);

        Task<CountryDto> Get(Guid id);
        Task<string> Add(CountryDto countryDto);
        Task<string> Update(CountryDto countryDto);
        Task<string> Delete(Guid id);
    }
}
