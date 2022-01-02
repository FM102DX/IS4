using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace FerryData.IS4.Infrastructure
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<Client> GetClients()
        {
            yield return new Client
            {
                ClientId = "client_blazor",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,

                AllowedScopes =
                {
                    "Blazor",
                    "ServerAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile
                },
               

                /*
                 * 
                 * RedirectUris = { "https://localhost:44326/authentication/login-callback" },
                 PostLogoutRedirectUris = { "https://localhost:44326/authentication/logout-callback" },
               
                */

                RedirectUris = {
                    "https://t104admin.ricompany.info/authentication/login-callback",
                    "https://t104shop.ricompany.info/authentication/login-callback",
                    "https://localhost:44326/authentication/login-callback"
                },
                PostLogoutRedirectUris = {
                    "https://t104admin.ricompany.info/authentication/logout-callback",
                    "https://t104shop.ricompany.info/authentication/logout-callback",
                    "https://localhost:44326/authentication/logout-callback"
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource("ServerAPI", "Server API");
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Address();
            yield return new IdentityResources.Email();
            yield return new IdentityResources.Profile();
        }

        public static IEnumerable<ApiScope> GetScopes()
        {
            yield return new ApiScope("ServerAPI", "Server API");
            yield return new ApiScope("Blazor", "Blazor WebAssembly");

        }
    }
}
