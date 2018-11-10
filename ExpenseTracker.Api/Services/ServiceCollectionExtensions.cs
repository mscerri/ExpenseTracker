using ExpenseTracker.Api.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpenseTracker.Api.Services
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddScoped<IUsersService, UsersService>();
            serviceCollection.TryAddScoped<IPasswordHasher<UserDto>, PasswordHasher<UserDto>>();
            serviceCollection.TryAddScoped<ITransactionCategoriesService, TransactionCategoriesService>();
            serviceCollection.TryAddScoped<ICurrenciesService, CurrenciesService>();
        }
    }
}
