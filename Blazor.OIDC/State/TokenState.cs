// ***********************************************************************
// Assembly         : Blazor.OIDC
// Author           : joshl
// Created          : 02-07-2022
//
// Last Modified By : joshl
// Last Modified On : 02-07-2022
// ***********************************************************************
// <copyright file="TokenState.cs" company="LavelyIO">
//     Copyright (c) LavelyIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Blazor.OIDC.State
{
    /// <summary>
    /// Class TokenState.
    /// </summary>
    public class TokenState
    {
        /// <summary>
        /// The access token
        /// </summary>
        public string AccessToken;

        /// <summary>
        /// The expiration
        /// </summary>
        public DateTimeOffset Expiration;

        /// <summary>
        /// The refresh token
        /// </summary>
        public string RefreshToken;

        /// <summary>
        /// The subject identifier
        /// </summary>
        public string SubjectId;
    }
}