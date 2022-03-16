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

namespace Orders.Test
{
    public class ProductsControllerTest
    {
        private Mock<IProductService> iproductService;

        public ProductsControllerTest()
        {
            iproductService = new Mock<IProductService>();
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public void GetAll_ListOfProductDto_ProductExistsInRepo()
        {
            //arrange
            iproductService.Setup(x => x.GetAll().Result).Returns(GetSampleProducts);
            var controller = new ProductsController(iproductService.Object);
            //act
            var actionResult = controller.GetAll().Result;
            var result = actionResult as OkObjectResult;
            var actual = result.Value as List<ProductDto>;
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleProducts().Count(), actual.Count());
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