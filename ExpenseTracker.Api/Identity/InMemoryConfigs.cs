using IdentityServer4.Models;
using System.Collections.Generic;

namespace ExpenseTracker.Api
{
    public class InMemoryConfigs
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "expense-tracker",
                    DisplayName = "Expense Tracker",
                    Scopes =
                    {
                        new Scope(Constants.Scopes.TrackExpenses),
                        new Scope(Constants.Policy.Management)
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "96a32b4f-53d6-4001-91ca-97fbdf218b23",
                    ClientSecrets =
                    {
                        new Secret("a77af739-2f7a-45de-980b-c73c2a404e0c".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        Constants.Scopes.TrackExpenses
                    }
                }
            };
        }
    }
}