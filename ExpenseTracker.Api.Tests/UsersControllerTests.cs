using System;
using System.Threading.Tasks;
using ExpenseTracker.Api.Tests.Helpers;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace ExpenseTracker.Api.Tests
{
    public class UsersControllerTests : InMemoryIntegrationTests<Startup>
    {
        [Fact]
        public async Task Test1()
        {
            var response = await Client.GetAsync("api/v1/users/1");
        }
    }
}
