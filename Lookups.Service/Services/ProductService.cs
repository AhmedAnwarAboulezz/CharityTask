﻿using AutoMapper;
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
    public class ProductService : BaseServices, IProductService
    {
        public ProductService(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<Service.Resources.Orders> ordersLocalize
            , IStringLocalizer<Common.StandardInfrastructure.Resources.Common> commonLocalize)
             : base(mapper, unitOfWork, ordersLocalize, commonLocalize)
        {
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var list = await UnitOfWork.GetRepository<Product>().GetAllAsync();
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(list);
        }

        public async Task<PagedListDto<ProductDto>> GetAllProductsPaged(ProductFilterDto filteringDto, PagingSortingDto pagingSortingDto)
        {
            var predicate = Helper.GetPredicate<Product, ProductFilterDto>(filteringDto);
            var (list, count) = await UnitOfWork.GetRepository<Product>().GetPagedListAsync(predicate, pagingSortingDto);
            return new PagedListDto<ProductDto>() { List = Mapper.Map<List<ProductDto>>(list), Count = count };
        }

        public async Task<ProductDto> Get(Guid id)
        {
            var product = await UnitOfWork.GetRepository<Product>().GetAsync(id);
            return Mapper.Map<Product, ProductDto>(product);
        }

        public async Task<string> Add(ProductDto productDto)
        {
            if (await UnitOfWork.GetRepository<Product>().IsExistAsync(productDto))
            {
                return OrdersLocalize["ProductService_IsExists"];
            }
            else
            {
                var product = Mapper.Map<ProductDto, Product>(productDto);
                await UnitOfWork.GetRepository<Product>().AddAsync(product);
                await UnitOfWork.SaveChangesAsync();
                return null;
            }
        }

        public async Task<string> Update(ProductDto productDto)
        {
            if (await UnitOfWork.GetRepository<Product>().IsExistAsync(productDto))
            {
                return OrdersLocalize["ProductService_IsExists"];
            }
            else
            {
                var productOld =await UnitOfWork.GetRepository<Product>().GetAsync(productDto.Id);
                var productNew = Mapper.Map<Product>(productDto);
                await UnitOfWork.GetRepository<Product>().UpdateAsync(productNew, productOld);
                await UnitOfWork.SaveChangesAsync();
                return null;
            }
        }

        public async Task<string> Delete(Guid id)
        {
            var product = await UnitOfWork.GetRepository<Product>().GetAsync(id);
            if (product == null) return null;
            await UnitOfWork.GetRepository<Product>().RemoveAsync(product);
            await UnitOfWork.SaveChangesAsync();
            return null;
        }

    }
}