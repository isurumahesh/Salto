﻿using Duende.IdentityServer.Models;
using IdentityModel;

namespace CloudWorks.Idp;
public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scwapi:manage"),
            
            new ApiScope("scwapi:use")
            {
                UserClaims={JwtClaimTypes.Email, JwtClaimTypes.Role}
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // server to server client credentials flow client
            new Client
            {
                ClientId = "b6a868bb-c552-473c-b440-f90b0aa05c2e",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scwapi:manage" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0",

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                RequireClientSecret = false,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scwapi:use" }
            },
        };
}
