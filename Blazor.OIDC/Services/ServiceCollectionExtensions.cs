// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="">
//
// </copyright>
// <summary>
//   The Blazor OIDC service collection extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Blazor.OIDC.Services
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using Blazor.OIDC;
    using Blazor.OIDC.Config;
    using IdentityModel.Client;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.IdentityModel.Protocols.OpenIdConnect;
    using Microsoft.IdentityModel.Tokens;

    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// The Blazor OIDC service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Initialize OIDC as a Singleton
        /// </summary>
        /// <param name="configuration">configuration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddBlazorOidc(this IServiceCollection services, IConfigurationSection configuration)
        {
            // TODO: 1 - Add 'BlazorOidc' appsetting.json section
            var oidcOptions = new OpenIdConnectOptions();
            configuration.Bind(oidcOptions);
            services.TryAddSingleton(oidcOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie("Cookies", options =>
                {
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.Name = "BlazorOidcCookie"; //TODO Add to ConfigurationSection
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    options.SlidingExpiration = true;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = async c =>
                        {
                            Console.WriteLine("New Auth request: ");
                            // this event is fired everytime the cookie has been validated by the cookie middleware, so basically during every authenticated request.
                            // the decryption of the cookie has already happened so we have access to the identity + user claims
                            // and cookie properties - expiration, etc..
                            // source: https://github.com/mderriey/aspnet-core-token-renewal/blob/2fd9abcc2abe92df2b6c4374ad3f2ce585b6f953/src/MvcClient/Startup.cs#L57
                            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                            var exp = c.Properties.ExpiresUtc.GetValueOrDefault().ToUnixTimeSeconds();

                            if (now >= exp) // session cookie expired?
                            {
                                var response = await new HttpClient().RequestRefreshTokenAsync(new RefreshTokenRequest
                                {
                                    Address = oidcOptions.Authority +
                                              "/protocol/openid-connect/token", // AAD="/oauth2/token",
                                    ClientId = configuration[$"{Constants.BlazorOidc}:ClientId"],
                                    ClientSecret = configuration[$"{Constants.BlazorOidc}:ClientSecret"],
                                    RefreshToken =
                                        ((ClaimsIdentity)c.Principal.Identity)
                                        .GetClaimValue(ClaimType
                                            .RefreshToken) //c.Properties.Items[".Token.refresh_token"] // check if present
                                }).ConfigureAwait(true);

                                if (!response.IsError)
                                {
                                    ((ClaimsIdentity)c.Principal.Identity)
                                        .SetIdentityClaims(response.AccessToken, response.RefreshToken);

                                    c.ShouldRenew = true; // renew session cookie
                                }
                                Console.WriteLine("Checking response: " + response.Json);
                            }
                        }
                    };
                })
                .AddOpenIdConnect("oidc", options =>
                    {
                        options.Authority = oidcOptions.Authority;
                        options.ClientId = oidcOptions.ClientId;
                        options.ClientSecret = oidcOptions.ClientSecret;
                        options.ResponseType = oidcOptions.ResponseType;
                        options.Resource = oidcOptions.Resource;
                        options.RequireHttpsMetadata = oidcOptions.RequireHttpsMetadata; // dev only
                        options.SaveTokens = true;
                        options.GetClaimsFromUserInfoEndpoint = true;

                        options.Scope.Add("profile");
                        options.Scope.Add("email");

                        // Avoids having users being presented the select account dialog when they are already signed-in
                        // for instance when going through incremental consent
                        var redirectToIdpHandler = options.Events.OnRedirectToIdentityProvider;
                        options.Events = new OpenIdConnectEvents
                        {
                            // Got a valid Login
                            OnTokenValidated = t =>
                            {
                                // this event is called after the OIDC middleware received the auhorisation code, redeemed it for an access token + a refresh token
                                // and validated the identity token
                                ((ClaimsIdentity)t.Principal.Identity).SetIdentityClaims(
                                        t.TokenEndpointResponse.AccessToken,
                                        t.TokenEndpointResponse.RefreshToken);

                                t.Properties.ExpiresUtc =
                                    new JwtSecurityToken(t.TokenEndpointResponse.AccessToken)
                                        .ValidTo; // align expiration of the cookie with expiration of the access token
                                t.Properties.IsPersistent =
                                        true; // so that we don't issue a session cookie but one with a fixed expiration

                                return Task.CompletedTask;
                            },
                            // When Redirecting the User
                            OnRedirectToIdentityProvider = async context =>
                            {
                                var login = context.Properties.GetParameter<string>(OpenIdConnectParameterNames.LoginHint);
                                if (!string.IsNullOrWhiteSpace(login))
                                {
                                    context.ProtocolMessage.LoginHint = login;

                                    // delete the login_hint from the Properties when we are done otherwise
                                    // it will take up extra space in the cookie.
                                    context.Properties.Parameters.Remove(OpenIdConnectParameterNames.LoginHint);
                                }

                                var domainHint =
                                    context.Properties.GetParameter<string>(OpenIdConnectParameterNames.DomainHint);
                                if (!string.IsNullOrWhiteSpace(domainHint))
                                {
                                    context.ProtocolMessage.DomainHint = domainHint;

                                    // delete the domain_hint from the Properties when we are done otherwise
                                    // it will take up extra space in the cookie.
                                    context.Properties.Parameters.Remove(OpenIdConnectParameterNames.DomainHint);
                                }

                                context.ProtocolMessage.SetParameter(Constants.ClientInfo, Constants.One);

                                // Additional claims
                                if (context.Properties.Items.ContainsKey(OidcConstants.AdditionalClaims))
                                {
                                    context.ProtocolMessage.SetParameter(
                                        OidcConstants.AdditionalClaims,
                                        context.Properties.Items[OidcConstants.AdditionalClaims]);
                                }

                                var toIdpHandler = redirectToIdpHandler;
                                await toIdpHandler(context).ConfigureAwait(true);
                            }
                        };
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            NameClaimType = "name",
                            RoleClaimType = "groups",
                            ValidateIssuer = true
                        };
                    });

            return services;
        }
    }
}