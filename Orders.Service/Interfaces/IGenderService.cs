using Common.StandardInfrastructure;
using Orders.Service.Dto;
using Orders.Service.FilterDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Service.Interfaces
{
    public interface IGenderService
    {
        Task<GenderDto> Get(Guid id);
        Task<IEnumerable<GenderDto>> GetAll();
        Task<IEnumerable<DropdownDto>> GetDropdownList();
    }
}