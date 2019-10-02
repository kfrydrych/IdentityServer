using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer.Auth
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "admin",
                    Password = "password",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Krzysztof"),
                        new Claim("family_name", "Frydrych")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {

            return new IdentityResource[] // available scopes
            {
                new IdentityResources.OpenId(), // Return subjectId claim
                new IdentityResources.Profile() // Profile claims (given_name, family_name)
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}
