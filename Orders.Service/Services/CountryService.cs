using AutoMapper;
using Common.StandardInfrastructure;
using Orders.Data.Entities;
using Orders.DataAccess;
using Orders.Service.Dto;
using Orders.Service.FilterDto;
using Orders.Service.Interfaces;
using Orders.Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Orders.Service.Services
{
    public class CountryService : BaseServices, ICountryService
    {
        public CountryService(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<Service.Resources.Orders> ordersLocalize
            , IStringLocalizer<Common.StandardInfrastructure.Resources.Common> commonLocalize)
             : base(mapper, unitOfWork, ordersLocalize, commonLocalize)
        {
        }

        public async Task<IEnumerable<CountryDto>> GetAll()
        {
            var list = await UnitOfWork.GetRepository<Country>().GetAllAsync();
            return Mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(list);
        }

        public async Task<PagedListDto<CountryDto>> GetAllCountriesPaged(CountryFilterDto filteringDto, PagingSortingDto pagingSortingDto)
        {
            var predicate = Helper.GetPredicate<Country, CountryFilterDto>(filteringDto);
            var (list, count) = await UnitOfWork.GetRepository<Country>().GetPagedListAsync(predicate, pagingSortingDto);
            return new PagedListDto<CountryDto>() { List = Mapper.Map<List<CountryDto>>(list), Count = count };
        }

        public async Task<CountryDto> Get(Guid id)
        {
            var country = await UnitOfWork.GetRepository<Country>().GetAsync(id);
            return Mapper.Map<Country, CountryDto>(country);
        }

        public async Task<string> Add(CountryDto countryDto)
        {
            if (await UnitOfWork.GetRepository<Country>().IsExistAsync(countryDto))
            {
                return OrdersLocalize["CountryService_IsExists"];
            }
            else
            {
                var country = Mapper.Map<CountryDto, Country>(countryDto);
                await UnitOfWork.GetRepository<Country>().AddAsync(country);
                await UnitOfWork.SaveChangesAsync();
                return null;
            }
        }

        public async Task<string> Update(CountryDto countryDto)
        {
            if (await UnitOfWork.GetRepository<Country>().IsExistAsync(countryDto))
            {
                return OrdersLocalize["CountryService_IsExists"];
            }
            else
            {
                var countryOld =await UnitOfWork.GetRepository<Country>().GetAsync(countryDto.Id);
                var countryNew = Mapper.Map<Country>(countryDto);
                await UnitOfWork.GetRepository<Country>().UpdateAsync(countryNew, countryOld);
                await UnitOfWork.SaveChangesAsync();
                return null;
            }
        }

        public async Task<string> Delete(Guid id)
        {
            var country = await UnitOfWork.GetRepository<Country>().GetAsync(id);
            if (country == null) return null;
            await UnitOfWork.GetRepository<Country>().RemoveAsync(country);
            await UnitOfWork.SaveChangesAsync();
            return null;
        }

    }
}
