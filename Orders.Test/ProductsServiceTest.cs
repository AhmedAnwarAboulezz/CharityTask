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


namespace Orders.Test
{
    public class ProductsServiceTest
    {
        private readonly ProductService _productService;
        private Mock<IMapper> mapper;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IStringLocalizer<Service.Resources.Orders>> ordersLocalize;
        private Mock<IStringLocalizer<Common.StandardInfrastructure.Resources.Common>> commonLocalize;

        public ProductsServiceTest()
        {
            mapper = new Mock<IMapper>();
            unitOfWork = new Mock<IUnitOfWork>();
            ordersLocalize = new Mock<IStringLocalizer<Service.Resources.Orders>>();
            commonLocalize = new Mock<IStringLocalizer<Common.StandardInfrastructure.Resources.Common>>();
            _productService = new ProductService(mapper.Object, unitOfWork.Object, ordersLocalize.Object, commonLocalize.Object);
        }



        [Fact]
        public void SetRemainInStock_GivenProduct_ReturnTrue()
        {
            var input = new Product() { Id = Guid.NewGuid(), Code = "SL-MF", Price = 50, ProductTypeId = 1, AmountInStock = 15, OrderDetails = new List<OrderDetail>(){ new OrderDetail{Amount=5}, new OrderDetail{Amount=3}}};
            var result = _productService.SetRemainInStock(input);
            var expectedResult = new ProductDto { Id = input.Id, Code = input.Code, Price = input.Price, ProductTypeId = input.ProductTypeId, AmountInStock = input.AmountInStock, CreatedDate = input.CreatedDate, RemainInStock = 7 };
            var resultStr = JsonConvert.SerializeObject(result);
            var expectedResultStr = JsonConvert.SerializeObject(expectedResult);
            Assert.Equal(expectedResultStr, resultStr);
        }
    }
}