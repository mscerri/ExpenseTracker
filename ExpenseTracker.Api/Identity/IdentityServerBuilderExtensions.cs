using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Api.Identity
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddRequiredServices(this IIdentityServerBuilder builder)
        {
            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            builder.AddProfileService<ProfileService>();
            return builder;
        }
    }
}
