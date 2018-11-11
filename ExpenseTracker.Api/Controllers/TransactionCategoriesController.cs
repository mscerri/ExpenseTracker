using ExpenseTracker.Api.Dtos;
using ExpenseTracker.Api.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ExpenseTracker.Services;
using ExpenseTracker.Services.Interfaces;

namespace ExpenseTracker.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class TransactionCategoriesController : ControllerBase
    {
        private readonly ITransactionCategoriesService _transactionCategoriesService;

        public TransactionCategoriesController(ITransactionCategoriesService transactionCategoriesService)
        {
            _transactionCategoriesService = transactionCategoriesService;
        }

        [HttpGet]
        [Authorize(Constants.Policy.EndUser)]
        [Authorize(Constants.Policy.Management)]
        [ProducesResponseType(typeof(TransactionCategoryDto[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTransactionCategories()
        {
            var response = await _transactionCategoriesService.ListAllTransactionCategoriesAsync();
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetTransactionCategory")]
        [Authorize(Constants.Policy.EndUser)]
        [Authorize(Constants.Policy.Management)]
        [ProducesResponseType(typeof(TransactionCategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTransactionCategory(int id)
        {
            var response = await _transactionCategoriesService.FindTransactionCategoryByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Constants.Policy.Management)]
        [ValidateModel]
        [ProducesResponseType(typeof(TransactionCategoryDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTransactionCategory([FromBody] CreateTransactionCategoryDto createTransactionCategoryDto)
        {
            var response = await _transactionCategoriesService.CreateTransactionCategoryAsync(createTransactionCategoryDto);
            return CreatedAtRoute("GetTransactionCategory", new {id = response.Id}, response);
        }

        [HttpPut("{id}")]
        [Authorize(Constants.Policy.Management)]
        [ValidateModel]
        [ProducesResponseType(typeof(TransactionCategoryDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateTransactionCategory(int id, [FromBody] TransactionCategoryDto transactionCategoryDto)
        {
            var response = await _transactionCategoriesService.UpdateTransactionCategoryAsync(id, transactionCategoryDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Constants.Policy.Management)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTransactionCategory(int id)
        {
            await _transactionCategoriesService.DeleteTransactionCategoryAsync(id);
            return NoContent();
        }
    }
}