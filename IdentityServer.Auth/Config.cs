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
                new ApiResource("api-products", "Product Catalog"),
                new ApiResource("api-inventory", "Current Stock"),
                new ApiResource("api-search", "Stock Management")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "vue js client",
                    ClientId = "vuejsclient",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "http://localhost:8081",
                        "http://localhost:8081/callback.html",
                        "http://localhost:8081/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:8081"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:8081"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "api-products",
                        "api-search"
                    },
                    // If set to true, adds claims to id_token
                    // not needed as the client get those claims from user info end point
                    AlwaysIncludeUserClaimsInIdToken = false,
                    AlwaysSendClientClaims = false
                },
                new Client
                {
                    ClientId = "search-api",
                    ClientSecrets =
                    {
                        new Secret("zbieram-pieczarki".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "api-products",
                        "api-inventory"
                    }
                }
            };
        }
    }
}
