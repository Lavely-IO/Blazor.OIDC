// ***********************************************************************
// Assembly         : Blazor.OIDC
// Author           : joshl
// Created          : 02-07-2022
//
// Last Modified By : joshl
// Last Modified On : 02-07-2022
// ***********************************************************************
// <copyright file="Oidc.cs" company="LavelyIO">
//     Copyright (c) LavelyIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace Blazor.OIDC.Config
{
    /// <summary>
    /// Class Oidc.
    /// </summary>
    public static class Oidc
    {
        /// <summary>
        /// Gets or sets the well known configuration.
        /// </summary>
        /// <value>The well known configuration.</value>
        private static OidcWellKnown WellKnownConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the JWT ks.
        /// </summary>
        /// <value>The JWT ks.</value>
        private static JwtKs JwtKs { get; set; }

        /// <summary>
        /// Get well known configuration as an asynchronous operation.
        /// </summary>
        /// <param name="authority">The authority.</param>
        /// <returns>A Task&lt;OidcWellKnown&gt; representing the asynchronous operation.</returns>
        public static async Task<OidcWellKnown> GetWellKnownConfigurationAsync(string authority)
        {
            if (WellKnownConfiguration == null)
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(authority.TrimEnd('/') + "/")
                };
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(".well-known/openid-configuration").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    WellKnownConfiguration = JsonConvert.DeserializeObject<OidcWellKnown>(content);
                }
            }

            return WellKnownConfiguration;
        }

        /// <summary>
        /// Gets the JWT ks.
        /// </summary>
        /// <param name="authority">The authority.</param>
        /// <returns>JwtKs.</returns>
        public static async Task<JwtKs> GetJwtKs(string authority)
        {
            if (JwtKs == null)
            {
                var configuration = await GetWellKnownConfigurationAsync(authority).ConfigureAwait(false);
                var client = new HttpClient
                {
                    BaseAddress = new Uri(configuration.jwks_uri)
                };
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(string.Empty).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    JwtKs = JsonConvert.DeserializeObject<JwtKs>(content) ?? new JwtKs();
                }
            }

            return JwtKs;
        }

        /// <summary>
        /// Updates the token claims.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="authority">The authority.</param>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="force">if set to <c>true</c> [force].</param>
        /// <returns>IIdentity.</returns>
        public static async Task<IIdentity> UpdateTokenClaims(ClaimsIdentity identity, string authority,
                                                              string clientId, string clientSecret, bool force = false)
        {
            // always refresh the token, needs refresh token to be present in identity claims
            // or let a timer doe this constantly? https://github.com/aspnet/AspNetCore/issues/16241
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var exp = long.Parse(identity.GetClaimValue(ClaimType.AccessTokenExpires));
            Console.WriteLine($"++++++ TOKEN now: {now}, exp: {exp} ++++++++");

            if (now >= exp || force)
            {
                // tokens expired https://identitymodel.readthedocs.io/en/latest/client/token.html#requesting-a-token-using-the-refresh-token-grant-type
                Console.WriteLine("++++++ TOKEN expired ++++++++");
                var response = await new HttpClient().RequestRefreshTokenAsync(new RefreshTokenRequest
                {
                    Address = (await GetWellKnownConfigurationAsync(authority).ConfigureAwait(false)).token_endpoint,
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    RefreshToken = identity.GetClaimValue(ClaimType.RefreshToken)
                }).ConfigureAwait(false);

                if (!response.IsError)
                {
                    Console.WriteLine($"++++++ TOKEN refreshed {response.AccessToken} ++++++++");
                    identity.SetIdentityClaims(response.AccessToken, response.RefreshToken);
                }
            }
            else
            {
                Console.WriteLine(
                    $"++++++ TOKEN expires at {exp} in {TimeSpan.FromSeconds(exp - now).Minutes} minutes ++++++++");
            }

            return identity;
        }
    }
}