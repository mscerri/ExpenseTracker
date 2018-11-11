using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace ExpenseTracker.Api.Tests.Helpers
{
    public abstract class InMemoryIntegrationTests<TStartup> : IDisposable where TStartup : class, IStartup
    {
        protected HttpClient Client { get; set; }
        protected TestServer Server { get; set; }

        protected InMemoryIntegrationTests()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseStartup<TStartup>()
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(TStartup).Assembly.FullName));

            Client = new HttpClient(Server.CreateHandler())
            {
                BaseAddress = Server.BaseAddress
            };
        }

        public void Dispose()
        {
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}
