using Common.StandardInfrastructure;
using Orders.Service.Dto;
using Orders.Service.FilterDto;
using Orders.Service.Interfaces;
using Orders.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Orders.WebAPI.Controllers
{
    /// <inheritdoc />
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        /// <inheritdoc />
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// Get data pagged 
        /// </summary>
        /// <param name="filteringDto"> Search filter</param>
        /// <param name="pagingSortingDto">Sort Parameters</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAllPaged([FromBody] ProductFilterDto filteringDto, [FromQuery] PagingSortingDto pagingSortingDto)
        {
            var result = await _productService.GetAllProductsPaged(filteringDto, pagingSortingDto);
            return Ok(result);
        }
        /// <summary>
        /// Get all data 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _productService.GetAll();
            return Ok(list);
        }
        /// <summary>
        /// Get All Products By Product Type
        /// </summary>
        /// <param name="productType">Product Type</param>
        /// <returns></returns>
        [HttpGet("{productType}")]
        public async Task<IActionResult> GetAllByProductType(int productType)
        {
            var product = await _productService.GetAllByProductType(productType);

            return Ok(product);
        }

        /// <summary>
        /// Get data by Id
        /// </summary>
        /// <param name="id">PK Column Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _productService.Get(id);

            return Ok(product);
        }
        /// <summary>
        /// Insert new
        /// </summary>
        /// <param name="productDto">Inserted object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            var message = await _productService.Add(productDto);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }
        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="productDto">Updated Object</param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var message = await _productService.Update(productDto);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();

        }
        /// <summary>
        /// Delete data by Id
        /// </summary>
        /// <param name="id">PK Column Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var message = await _productService.Delete(id);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }


        /// <summary>
        /// Decrease Remain In Stock
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DecreaseRemainInStock(Guid productId)
        {
            var message = await _productService.DecreaseRemainInStock(productId);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }

    }

}
