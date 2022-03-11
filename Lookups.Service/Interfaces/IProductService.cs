using Common.StandardInfrastructure;
using Orders.Service.Dto;
using Orders.Service.FilterDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<PagedListDto<ProductDto>> GetAllCountriesPaged(ProductFilterDto filteringDto, PagingSortingDto pagingSortingDto);
        Task<ProductDto> Get(Guid id);
        Task<string> Add(ProductDto productDto);
        Task<string> Update(ProductDto productDto);
        Task<string> Delete(Guid id);
    }
}
