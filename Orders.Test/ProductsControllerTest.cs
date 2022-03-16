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

        [Fact]
        public void GetAllByProductType_ListOfProductDto_ProductExistsByTypeInRepo()
        {
            //arrange
            iproductService.Setup(x => x.GetAllByProductType(1).Result).Returns(GetSampleProducts);
            iproductService.Setup(x => x.GetAllByProductType(2).Result).Returns(GetSampleProducts);
            var controller = new ProductsController(iproductService.Object);
            //act
            var actionResult = controller.GetAllByProductType(1).Result;
            var result = actionResult as OkObjectResult;
            var actual = result.Value as List<ProductDto>;

            var actionResult2 = controller.GetAllByProductType(2).Result;
            var result2 = actionResult as OkObjectResult;
            var actual2 = result.Value as List<ProductDto>;
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleProducts().Count(), actual.Count());
            Assert.IsType<OkObjectResult>(result2);
            Assert.Equal(GetSampleProducts().Count(), actual2.Count());

        }

        [Fact]
        public void GetAllByProductType_ListOfProductDto_ProductDoesnetExistsByTypeInRepo()
        {
            //arrange
            iproductService.Setup(x => x.GetAllByProductType(3).Result).Returns(new List<ProductDto>());
            iproductService.Setup(x => x.GetAllByProductType(0).Result).Returns(new List<ProductDto>());
            var controller = new ProductsController(iproductService.Object);
            //act
            var actionResult = controller.GetAllByProductType(3).Result;
            var result = actionResult as OkObjectResult;
            var actual = result.Value as List<ProductDto>;

            var actionResult2 = controller.GetAllByProductType(0).Result;
            var result2 = actionResult as OkObjectResult;
            var actual2 = result.Value as List<ProductDto>;
            //assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(0, actual.Count());
            Assert.IsType<OkObjectResult>(result2);
            Assert.Equal(0, actual2.Count());

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