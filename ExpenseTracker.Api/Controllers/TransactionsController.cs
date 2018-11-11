using ExpenseTracker.Api.Extensions;
using ExpenseTracker.Api.Validation;
using ExpenseTracker.DTO;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ExpenseTracker.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ITransactionsService transactionsService, ILogger<TransactionsController> logger)
        {
            _transactionsService = transactionsService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetTransaction")]
        [Authorize(Constants.Policy.Any)]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var userGuid = User.GetUserGuid();
            if (userGuid != null && !await _transactionsService.ValidateTransactionUserOwnershipAsync(id, userGuid.Value))
            {
                _logger.LogWarning("User {userId} does not have permission to view transaction {transactionId}", userGuid, id);
                return Forbid();
            }

            var response = await _transactionsService.FindTransactionByGuidAsync(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Constants.Policy.Any)]
        [ValidateModel]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTransactionDto updateTransactionDto)
        {
            var userGuid = User.GetUserGuid();
            if (userGuid != null && !await _transactionsService.ValidateTransactionUserOwnershipAsync(id, userGuid.Value))
            {
                _logger.LogWarning("User {userId} does not have permission to update transaction {transactionId}", userGuid, id);
                return Forbid();
            }

            var response = await _transactionsService.UpdateTransactionAsync(id, updateTransactionDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Constants.Policy.Any)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var userGuid = User.GetUserGuid();
            if (userGuid != null && !await _transactionsService.ValidateTransactionUserOwnershipAsync(id, userGuid.Value))
            {
                _logger.LogWarning("User {userId} does not have permission to delete transaction {transactionId}", userGuid, id);
                return Forbid();
            }

            await _transactionsService.DeleteTransactionAsync(id);
            return NoContent();
        }
    }
}