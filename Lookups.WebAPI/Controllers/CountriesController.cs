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
    public class CountriesController : BaseController
    {
        private readonly ICountryService _countryService;
        /// <inheritdoc />
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        /// <summary>
        /// Get data pagged 
        /// </summary>
        /// <param name="filteringDto"> Search filter</param>
        /// <param name="pagingSortingDto">Sort Parameters</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAllPaged([FromBody] CountryFilterDto filteringDto, [FromQuery] PagingSortingDto pagingSortingDto)
        {
            var result = await _countryService.GetAllCountriesPaged(filteringDto, pagingSortingDto);
            return Ok(result);
        }
        /// <summary>
        /// Get all data 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _countryService.GetAll();
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
            var country = await _countryService.Get(id);

            return Ok(country);
        }
        /// <summary>
        /// Insert new
        /// </summary>
        /// <param name="countryDto">Inserted object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CountryDto countryDto)
        {
            var message = await _countryService.Add(countryDto);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }
        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="countryDto">Updated Object</param>
        /// <returns></returns>
        [HttpPut()]
        public async Task<IActionResult> Update(CountryDto countryDto)
        {
            var message = await _countryService.Update(countryDto);
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
            var message = await _countryService.Delete(id);
            if (!string.IsNullOrWhiteSpace(message))
            {
                return BadRequest(message);
            }
            return Ok();
        }

    }

}
