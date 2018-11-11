using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ExpenseTracker.Services;
using ExpenseTracker.Services.Exceptions;
using ExpenseTracker.Services.Interfaces;

namespace ExpenseTracker.Api.Identity
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<ResourceOwnerPasswordValidator> _logger;

        public ResourceOwnerPasswordValidator(IUsersService usersService, ILogger<ResourceOwnerPasswordValidator> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = await _usersService.FindUserByEmailAsync(context.UserName);

                if (await _usersService.VerifyUserPassword(user, context.Password))
                {
                    _logger.LogInformation("Credentials validated for username: {username}", context.UserName);
                    context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
                    return;
                }

                _logger.LogInformation("Authentication failed for username: {username}, reason: invalid credentials", context.UserName);
            }
            catch (NotFoundException)
            {
                _logger.LogInformation("No user found matching username: {username}", context.UserName);
            }

            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
        }
    }
}
