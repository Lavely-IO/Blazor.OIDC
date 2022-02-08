// ***********************************************************************
// Assembly         : Blazor.OIDC
// Author           : joshl
// Created          : 02-07-2022
//
// Last Modified By : joshl
// Last Modified On : 02-07-2022
// ***********************************************************************
// <copyright file="CachedState.cs" company="LavelyIO">
//     Copyright (c) LavelyIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Concurrent;

namespace Blazor.OIDC.State
{
    /// <summary>
    /// Class CachedState.
    /// </summary>
    public class CachedState
    {
        /// <summary>
        /// The cache
        /// </summary>
        private ConcurrentDictionary<string, TokenState> Cache
            = new ConcurrentDictionary<string, TokenState>();

        /// <summary>
        /// Determines whether [has subject identifier] [the specified subject identifier].
        /// </summary>
        /// <param name="subjectId">The subject identifier.</param>
        /// <returns><c>true</c> if [has subject identifier] [the specified subject identifier]; otherwise, <c>false</c>.</returns>
        public bool HasSubjectId(string subjectId)
            => Cache.ContainsKey(subjectId);

        /// <summary>
        /// Adds the specified subject identifier.
        /// </summary>
        /// <param name="subjectId">The subject identifier.</param>
        /// <param name="expiration">The expiration.</param>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        public void Add(string subjectId, DateTimeOffset expiration, string accessToken, string refreshToken)
        {
            System.Diagnostics.Debug.WriteLine($"Caching sid: {subjectId}");

            var data = new TokenState
            {
                SubjectId = subjectId,
                Expiration = expiration,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            Cache.AddOrUpdate(subjectId, data, (k, v) => data);
        }

        /// <summary>
        /// Gets the specified subject identifier.
        /// </summary>
        /// <param name="subjectId">The subject identifier.</param>
        /// <returns>TokenState.</returns>
        public TokenState Get(string subjectId)
        {
            Cache.TryGetValue(subjectId, out var data);
            return data;
        }

        /// <summary>
        /// Removes the specified subject identifier.
        /// </summary>
        /// <param name="subjectId">The subject identifier.</param>
        public void Remove(string subjectId)
        {
            System.Diagnostics.Debug.WriteLine($"Removing sid: {subjectId}");
            Cache.TryRemove(subjectId, out _);
        }
    }
}