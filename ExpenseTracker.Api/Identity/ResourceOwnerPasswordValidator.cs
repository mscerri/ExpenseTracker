using ExpenseTracker.Api.Services;
using IdentityModel;
using IdentityServer4.Validation;
using System;
using System.Threading.Tasks;

namespace ExpenseTracker.Api.Identity
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUsersService _usersService;

        public ResourceOwnerPasswordValidator(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(Guid.NewGuid().ToString(), OidcConstants.AuthenticationMethods.Password);
            return Task.CompletedTask;
        }
    }
}
