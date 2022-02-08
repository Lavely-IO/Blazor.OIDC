// ***********************************************************************
// Assembly         : Blazor.OIDC
// Author           : joshl
// Created          : 02-07-2022
//
// Last Modified By : joshl
// Last Modified On : 02-07-2022
// ***********************************************************************
// <copyright file="ClaimType.cs" company="Blazor.OIDC">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Blazor.OIDC
{
    /// <summary>
    /// Struct ClaimType
    /// </summary>
    public struct ClaimType
    {
        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public static string AccessToken => "access_token";

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        /// <value>The refresh token.</value>
        public static string RefreshToken => "refresh_token";

        /// <summary>
        /// Gets the access token expires.
        /// </summary>
        /// <value>The access token expires.</value>
        public static string AccessTokenExpires => "access_token_expires";

        /// <summary>
        /// Gets the refresh token expires.
        /// </summary>
        /// <value>The refresh token expires.</value>
        public static string RefreshTokenExpires => "refresh_token_expires";
    }
}