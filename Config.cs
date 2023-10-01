using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId()
        };

   
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
                new Client
                {
                   ClientId = "client",
                   ClientName = "Client for Postman user",
                   AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                   ClientSecrets = {new Secret("secret".Sha256())},
                   AllowedScopes = {"api1"},
                   AlwaysSendClientClaims = true,
                   AlwaysIncludeUserClaimsInIdToken = true,
                   AllowAccessTokensViaBrowser = true,
                },

               new Client
               {
                   ClientId = "swagger",
                   ClientName = "Client for Swagger user",
                   AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                   ClientSecrets = {new Secret("secret".Sha256())},
                   AllowedScopes = {"api1"},
                   AlwaysSendClientClaims = true,
                   AlwaysIncludeUserClaimsInIdToken = true,
                   AllowAccessTokensViaBrowser = true,
                   RedirectUris = { "https://localhost:7191/swagger/oauth2-redirect.html" },
                   AllowedCorsOrigins = { "https://localhost:7191" }
               }
        };
}
