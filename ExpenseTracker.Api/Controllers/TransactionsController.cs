using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/transactions")]
    public class TransactionsController : ControllerBase
    {
        [HttpGet("{id}", Name = "GetTransaction")]
        public IActionResult GetTransaction(long id)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(long id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(long id)
        {
            return NoContent();
        }
    }
}