using Common.StandardInfrastructure;
using Orders.Service.Dto;
using Orders.Service.FilterDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> Get(Guid id);
        Task<IEnumerable<OrderDto>> GetAll();
        Task<string> AddCheckout(OrderDto orderDto);
    }
}