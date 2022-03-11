using AutoMapper;
using Common.StandardInfrastructure;
using Orders.Data.Entities;
using Orders.DataAccess;
using Orders.Service.Dto;
using Orders.Service.FilterDto;
using Orders.Service.Interfaces;
using Orders.Service.Services.Base;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Service.Services
{
    public class GenderService : BaseServices, IGenderService
    {
        public GenderService(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<Service.Resources.Orders> ordersLocalize
             , IStringLocalizer<Common.StandardInfrastructure.Resources.Common> commonLocalize)
            : base(mapper, unitOfWork, ordersLocalize, commonLocalize) { }

        public async Task<IEnumerable<GenderDto>> GetAll()
        {
            var list = await UnitOfWork.GetRepository<Gender>().FindAsync(r => r.IsShown == true);
            return Mapper.Map<IEnumerable<Gender>, IEnumerable<GenderDto>>(list);
        }
        public async Task<IEnumerable<DropdownDto>> GetDropdownList()
        {
            var list = await UnitOfWork.GetRepository<Gender>().GetAllAsync();
            return Mapper.Map<IEnumerable<DropdownDto>>(list.Where(r => r.IsShown == true));
        }
        public async Task<GenderDto> Get(Guid id)
        {
            var gender = await UnitOfWork.GetRepository<Gender>().GetAsync(id);
            return Mapper.Map<Gender, GenderDto>(gender);
        }

    }
}
