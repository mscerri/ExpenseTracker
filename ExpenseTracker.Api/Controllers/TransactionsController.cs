using ExpenseTracker.DTO;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using ExpenseTracker.Api.Validation;

namespace ExpenseTracker.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpGet("{id}", Name = "GetTransaction")]
        [Authorize(Constants.Policy.Any)]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var response = await _transactionsService.FindTransactionByGuidAsync(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Constants.Policy.Any)]
        [ValidateModel]
        public IActionResult UpdateTransaction(Guid id, [FromBody] UpdateTransactionDto updateTransactionDto)
        {
            _transactionsService.UpdateTransactionAsync(id, updateTransactionDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Constants.Policy.Any)]
        public IActionResult DeleteTransaction(Guid id)
        {
            return NoContent();
        }
    }
}