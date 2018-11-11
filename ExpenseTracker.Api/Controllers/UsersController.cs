using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ExpenseTracker.Api.Validation;
using ExpenseTracker.DTO;
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
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userRegistrationDto)
        {
            var user = await _usersService.RegisterUserAsync(userRegistrationDto);
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpGet("{id}", Name = "GetUser")]
        [Authorize(Constants.Policy.EndUser)]
        [ValidateModel]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUser(string id)
        {
            if (!Guid.TryParse(id, out var userGuid))
            {
                ModelState.AddModelError("id", "ID provided is not in a valid format");
                return BadRequest(ModelState);
            }

            var response = await _usersService.FindUserByUserGuidAsync(userGuid);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Constants.Policy.EndUser)]
        [ValidateModel]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationError[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userDto)
        {
            if (!Guid.TryParse(id, out var userGuid))
            {
                ModelState.AddModelError("id", "ID provided is not in a valid format");
                return BadRequest(ModelState);
            }

            var response = await _usersService.UpdateUserAsync(userGuid, userDto);
            return Ok(response);
        }

        [HttpGet("{id}/transactions")]
        [Authorize]
        public IActionResult GetUserTransactions(string id)
        {
            return Ok();
        }

        [HttpPost("{id}/transactions")]
        [Authorize]
        public IActionResult CreateUserTransaction(string id)
        {
            return CreatedAtRoute("GetTransaction", new {id = 1}, new {transactionid = 1});
        }
    }
}