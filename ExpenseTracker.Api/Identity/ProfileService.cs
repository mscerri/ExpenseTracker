using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Threading.Tasks;
using ExpenseTracker.Api.Services;

namespace ExpenseTracker.Api.Identity
{
    public class ProfileService : IProfileService
    {
        private readonly IUsersService _usersService;

        public ProfileService(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new NotImplementedException();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
