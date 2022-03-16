using Orders.Service.Interfaces;
using Orders.Service.Services;
using System;
using Xunit;
using Moq;
using AutoMapper;
using Orders.DataAccess;
using Microsoft.Extensions.Localization;
using Orders.Data.Entities;
using System.Collections.Generic;
using Orders.Service.Dto;
using Newtonsoft.Json;
using Orders.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Orders.Test
{
    public class OrdersServiceTest
    {
        private readonly OrderService _orderService;
        private readonly OrdersController _orderController;

        private Mock<IOrderService> iorderService;
        private Mock<IMapper> mapper;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IStringLocalizer<Service.Resources.Orders>> ordersLocalize;
        private Mock<IStringLocalizer<Common.StandardInfrastructure.Resources.Common>> commonLocalize;

        public OrdersServiceTest()
        {
            mapper = new Mock<IMapper>();
            unitOfWork = new Mock<IUnitOfWork>();
            ordersLocalize = new Mock<IStringLocalizer<Service.Resources.Orders>>();
            commonLocalize = new Mock<IStringLocalizer<Common.StandardInfrastructure.Resources.Common>>();
            iorderService = new Mock<IOrderService>();
            _orderService = new OrderService(mapper.Object, unitOfWork.Object, ordersLocalize.Object, commonLocalize.Object);
            _orderController = new OrdersController(iorderService.Object);
        }
        [Fact]
        public void CheckOutOfStockMessage_StringErrorOrNull_ReturnTrue()
        {
            var result = _orderService.CheckOutOfStockMessage(GetInputOrderDetails(), GetOldProductDetails());
            var expectedResult = new StringBuilder();
            expectedResult = expectedResult.AppendLine($"product code SL-BR have only 28 items in stock.");
            Assert.Equal(expectedResult.ToString(), result);
        }
        private List<ProductDetailDto> GetOldProductDetails()
        {
            List<ProductDetailDto> output = new List<ProductDetailDto>
            {
                        new ProductDetailDto()
                    {
                        ProductId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                        ProductCode = "SL-BR",
                        RemainAmount = 28
                    }
            
            };
            return output;
        }
        private List<OrderDetailDto> GetInputOrderDetails()
        {
            List<OrderDetailDto> output = new List<OrderDetailDto>
            {
              new OrderDetailDto {
                  ProductId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                  Amount = 40
              },
               
              new OrderDetailDto  {
                  ProductId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                  Amount = 5
              }
            };
            return output;
        }
    }

}