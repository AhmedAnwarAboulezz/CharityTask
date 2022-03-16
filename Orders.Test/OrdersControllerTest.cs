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
    public class OrdersControllerTest
    {
        //private readonly OrdersController _orderController;

        private Mock<IOrderService> iorderService;


        public OrdersControllerTest()
        {
            iorderService = new Mock<IOrderService>();
            //_orderController = new OrdersController(iorderService.Object);
        }
        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public void AddCheckout_OrderDto_ErrorExceedInStock()
        {
            var errorMsg = ExceedMessageFailed();
            //arrange
            iorderService.Setup(x => x.AddCheckout(null).Result).Returns(ExceedMessageFailed);
            var controller = new OrdersController(iorderService.Object);
            //act
            var actionResult = controller.AddCheckout(null).Result;
            var result = actionResult as ObjectResult;
            var actual = result.Value as string;
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errorMsg, actual);
        }
        private string ExceedMessageFailed()
        {
            var errorMessage = new StringBuilder();
            errorMessage.AppendLine("product code SL-BR have only 28 items in stock.");
            return errorMessage.ToString();
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public void AddCheckout_OrderDto_Successed()
        {
            //arrange
            iorderService.Setup(x => x.AddCheckout(null).Result).Returns("");
            var controller = new OrdersController(iorderService.Object);
            //act
            var actionResult = controller.AddCheckout(null).Result;
            var result = actionResult as OkResult;
            //var actual = result.Value as string;
            //assert
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, result.StatusCode);
        }

        private List<ProductDto> GetSampleProducts()
        {
            List<ProductDto> output = new List<ProductDto>
            {
              new ProductDto  {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                Code = "SL-BR",
                NameFl= "Brownie",
                NameSl= "Brownie",
                Price= (float)0.65,
                ProductTypeId= 1,
                AmountInStock= 48
              },
              new ProductDto  {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                Code = "SL-MF",
                NameFl= "Muffin",
                NameSl= "Muffin",
                Price= (float)1.00,
                ProductTypeId= 1,
                AmountInStock= 36
              }
            };
            return output;
        }
    }

}