using ExpenseTracker.Services.Exceptions;
using ExpenseTracker.Services.Interfaces;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseTracker.Api.Identity
{
    public class ProfileService : IProfileService
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(IUsersService usersService, ILogger<ProfileService> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No subject claim present");

            try
            {
                var user = await _usersService.FindUserByGuidAsync(Guid.Parse(sub));

                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.GivenName, user.Name),
                    new Claim(JwtClaimTypes.FamilyName, user.Surname),
                    new Claim(JwtClaimTypes.Email, user.Email)
                };
                context.AddRequestedClaims(claims);
            }
            catch (NotFoundException)
            {
                _logger.LogWarning("No user found matching subject Id: {sub}", sub);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No subject claim present");

            try
            {
                var user = await _usersService.FindUserByGuidAsync(Guid.Parse(sub));
                context.IsActive = user.IsActive;
            }
            catch (NotFoundException)
            {
                _logger.LogWarning("No user found matching subject Id: {sub}", sub);
                context.IsActive = false;
            }
        }
    }
}
