using Orders.Service.FilterDto;
using Orders.Service.Interfaces;
using Orders.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders.Service.Dto;

namespace Orders.WebAPI.Controllers
{
    /// <inheritdoc />
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        /// <inheritdoc />
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// Get all data 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _orderService.GetAll();
            return Ok(list);
        }
        /// <summary>
        /// Get data by Id
        /// </summary>
        /// <param name="id">PK Column Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _orderService.Get(id);
            return Ok(order);
        }

        /// <summary>
        /// Add checkout for order
        /// </summary>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCheckout(OrderDto orderDto)
        {
            var message = await _orderService.AddCheckout(orderDto);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }

    }
}