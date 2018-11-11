using System;
using System.Net;
using System.Runtime.InteropServices;
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
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userRegistrationDto)
        {
            var user = await _usersService.RegisterUserAsync(userRegistrationDto);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize]
        public IActionResult GetUser(long id)
        {
            return Ok();
        }

        [HttpGet("{id}/transactions")]
        [Authorize]
        public IActionResult GetUserTransactions(long id)
        {
            return Ok();
        }

        [HttpPost("{id}/transactions")]
        [Authorize]
        public IActionResult CreateUserTransaction(long id)
        {
            return CreatedAtRoute("GetTransaction", new {id = 1}, new {transactionid = 1});
        }
    }
}