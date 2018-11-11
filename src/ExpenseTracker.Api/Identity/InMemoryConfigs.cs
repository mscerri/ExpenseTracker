using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace ExpenseTracker.Api.Identity
{
    public static class InMemoryConfigs
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
                        new Scope(Constants.Scopes.Manage)
                    },
                    UserClaims =
                    {
                        JwtClaimTypes.GivenName,
                        JwtClaimTypes.FamilyName,
                        JwtClaimTypes.Email
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
                },
                new Client
                {
                    ClientId = "0e4ff7f8-9679-478a-a55c-2444df9c9fc7",
                    ClientSecrets =
                    {
                        new Secret("ccde1e22-85e1-4419-b14a-5d32ca923683".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        Constants.Scopes.Manage
                    }
                }
            };
        }
    }
}