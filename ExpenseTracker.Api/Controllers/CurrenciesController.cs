using System.Net;
using System.Threading.Tasks;
using ExpenseTracker.Api.Dtos;
using ExpenseTracker.Api.Validation;
using ExpenseTracker.Services;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/currencies")]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrenciesService _currenciesService;

        public CurrenciesController(ICurrenciesService currenciesService)
        {
            _currenciesService = currenciesService;
        }

        [HttpGet]
        [Authorize(Constants.Policy.EndUser)]
        [Authorize(Constants.Policy.Management)]
        [ProducesResponseType(typeof(CurrencyDto[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurrencies()
        {
            var response = await _currenciesService.ListAllCurrenciesAsync();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetCurrency")]
        [Authorize(Constants.Policy.EndUser)]
        [Authorize(Constants.Policy.Management)]
        [ProducesResponseType(typeof(CurrencyDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrency(int id)
        {
            var response = await _currenciesService.FindCurrencyByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Constants.Policy.Management)]
        [ValidateModel]
        [ProducesResponseType(typeof(CurrencyDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCurrency([FromBody] CreateCurrencyDto createCurrencyDto)
        {
            var response = await _currenciesService.CreateCurrencyAsync(createCurrencyDto);
            return CreatedAtRoute("GetCurrency", new {id = response.Id}, response);
        }

        [HttpPut("{id}")]
        [Authorize(Constants.Policy.Management)]
        [ValidateModel]
        [ProducesResponseType(typeof(CurrencyDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateCurrency(int id, [FromBody] CurrencyDto currencyDto)
        {
            var response = await _currenciesService.UpdateCurrencyAsync(id, currencyDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Constants.Policy.Management)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            await _currenciesService.DeleteCurrencyAsync(id);
            return NoContent();
        }
    }
}