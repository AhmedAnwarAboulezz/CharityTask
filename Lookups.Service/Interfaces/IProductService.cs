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
        Task<PagedListDto<ProductDto>> GetAllProductsPaged(ProductFilterDto filteringDto, PagingSortingDto pagingSortingDto);
        Task<IEnumerable<ProductDto>> GetAllByProductType(int productType);
        Task<ProductDto> Get(Guid id);
        Task<string> Add(ProductDto productDto);
        Task<string> Update(ProductDto productDto);
        Task<string> Delete(Guid id);
        Task<string> DecreaseRemainInStock(Guid productId);
    }
}
