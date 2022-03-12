using AutoMapper;
using Common.StandardInfrastructure;
using Orders.Data.Entities;
using Orders.DataAccess;
using Orders.Service.Interfaces;
using Orders.Service.Services.Base;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Service.Dto;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Orders.Service.Services
{
    public class OrderService : BaseServices, IOrderService
    {
        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IStringLocalizer<Service.Resources.Orders> ordersLocalize
             , IStringLocalizer<Common.StandardInfrastructure.Resources.Common> commonLocalize)
            : base(mapper, unitOfWork, ordersLocalize, commonLocalize) { }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var list = await UnitOfWork.GetRepository<Order>().FindAsync(r => true);
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(list);
        }
        public async Task<OrderDto> Get(Guid id)
        {
            var order = await UnitOfWork.GetRepository<Order>().GetAsync(id);
            return Mapper.Map<Order, OrderDto>(order);
        }

        private async Task<string> ValidateAmountInStockAsync(List<OrderDetailDto> orderDetails)
        {
            var result = new StringBuilder();
            var productIds = orderDetails.Select(a => a.ProductId).ToList();
            var oldProducts = (await UnitOfWork.GetRepository<Product>().FindSelectAsync(a => productIds.Contains(a.Id), 
                select: a=> new ProductDetailDto()
                {
                    ProductId= a.Id,
                    ProductCode= a.Code,
                    RemainAmount = a.AmountInStock - a.OrderDetails.Sum(q => q.Amount)
                }
                , include: source => source.Include(r => r.OrderDetails)));
            orderDetails.ForEach(item =>
            {
                var prodItem = oldProducts.FirstOrDefault(a => a.ProductId == item.ProductId);
                if(prodItem != null && item.Amount > prodItem.RemainAmount)
                {
                    result.AppendLine($"product code {prodItem.ProductCode} have only {prodItem.RemainAmount} items in stock.");
                }
            });
            return result.ToString();
        }
        public async Task<string> AddCheckout(OrderDto orderDto)
        {
            var validateAmount = await ValidateAmountInStockAsync(orderDto.OrderDetails);
            if (!string.IsNullOrWhiteSpace(validateAmount)) return validateAmount;
            var order =  Mapper.Map<Order>(orderDto);
            await UnitOfWork.GetRepository<Order>().AddAsync(order);
            await UnitOfWork.SaveChangesAsync();
            return null;
        }

    }
}
