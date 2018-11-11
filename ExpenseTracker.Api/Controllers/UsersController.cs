using ExpenseTracker.Api.Extensions;
using ExpenseTracker.Api.Validation;
using ExpenseTracker.DTO;
using ExpenseTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ExpenseTracker.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITransactionsService _transactionsService;

        public UsersController(IUsersService usersService, ITransactionsService transactionsService)
        {
            _usersService = usersService;
            _transactionsService = transactionsService;
        }

        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userRegistrationDto)
        {
            var user = await _usersService.RegisterUserAsync(userRegistrationDto);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpGet("me", Name = "GetCurrentUser")]
        [Authorize(Constants.Policy.EndUser)]
        [ValidateModel]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userGuid = User.GetUserGuid();
            if (userGuid == null)
                return Forbid();

            var response = await _usersService.FindUserByGuidAsync(userGuid.Value);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize(Constants.Policy.Management)]
        [ValidateModel]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var response = await _usersService.FindUserByGuidAsync(id);
            return Ok(response);
        }

        [HttpPut("me")]
        [Authorize(Constants.Policy.EndUser)]
        [ValidateModel]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateCurrentUser([FromBody] UserDto userDto)
        {
            var userGuid = User.GetUserGuid();
            if (userGuid == null)
                return Forbid();

            var response = await _usersService.UpdateUserAsync(userGuid.Value, userDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Constants.Policy.EndUser)]
        [ValidateModel]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto userDto)
        {
            var response = await _usersService.UpdateUserAsync(id, userDto);
            return Ok(response);
        }

        [HttpGet("me/transactions")]
        [Authorize(Constants.Policy.EndUser)]
        [ProducesResponseType(typeof(TransactionDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentUserTransactions()
        {
            var userGuid = User.GetUserGuid();
            if (userGuid == null)
                return Forbid();

            var response = await _transactionsService.ListAllTransactionsForUserAsync(userGuid.Value);
            return Ok(response);
        }

        [HttpGet("{id}/transactions")]
        [Authorize(Constants.Policy.Management)]
        [ProducesResponseType(typeof(TransactionDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserTransactions(Guid id)
        {
            var response = await _transactionsService.ListAllTransactionsForUserAsync(id);
            return Ok(response);
        }

        [HttpPost("me/transactions")]
        [Authorize(Constants.Policy.EndUser)]
        [ValidateModel]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateCurrentUserTransaction([FromBody] CreateTransactionDto createTransactionDto)
        {
            var userGuid = User.GetUserGuid();
            if (userGuid == null)
                return Forbid();

            var response = await _transactionsService.CreateTransactionAsync(createTransactionDto, userGuid.Value);
            return CreatedAtRoute("GetTransaction", new { id = response.Id }, response);
        }

        [HttpPost("{id}/transactions")]
        [Authorize(Constants.Policy.Management)]
        [ValidateModel]
        [ProducesResponseType(typeof(TransactionDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateUserTransaction(Guid id, [FromBody] CreateTransactionDto createTransactionDto)
        {
            var response = await _transactionsService.CreateTransactionAsync(createTransactionDto, id);
            return CreatedAtRoute("GetTransaction", new {id = response.Id}, response);
        }
    }
}