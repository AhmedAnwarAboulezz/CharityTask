using Orders.Service.FilterDto;
using Orders.Service.Interfaces;
using Orders.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.WebAPI.Controllers
{
    /// <inheritdoc />
    public class GendersController : BaseController
    {
        private readonly IGenderService _genderService;
        /// <inheritdoc />
        public GendersController(IGenderService genderService)
        {
            _genderService = genderService;
        }
        /// <summary>
        /// Get all data 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _genderService.GetAll();
            return Ok(list);
        }
        /// <summary>
        /// Get all data 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetDropdownList()
        {
            var list = await _genderService.GetDropdownList();
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
            var gender = await _genderService.Get(id);
            return Ok(gender);
        }
        
    }
}