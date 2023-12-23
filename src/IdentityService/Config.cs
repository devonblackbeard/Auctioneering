using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("auctionApp", "Auction app full access"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new() {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = {"openid", "profile","auctionApp"},
                RedirectUris = {"http://www.getpostman.com/oauth2/callback"},
                ClientSecrets= new [] { new Secret("NotASecret".Sha256()) },
                AllowedGrantTypes = { GrantType.ResourceOwnerPassword }
            },
            new()
            {
                ClientId = "nextApp",
                ClientName = "nextApp",
                ClientSecrets= new [] { new Secret("NotASecret".Sha256()) },
                AllowedGrantTypes = { GrantType.ClientCredentials },
                RequirePkce = false,
                RedirectUris = {"http://localhost:3000/api/auth/callback/id-server"},
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "auctionapp" },
                AccessTokenLifetime = 3600 * 24 * 30
            }
        };
}
