using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.OIDC.State
{
    using System.Collections.Concurrent;

    public class CachedState
    {
        private ConcurrentDictionary<string, TokenState> Cache
            = new ConcurrentDictionary<string, TokenState>();

        public bool HasSubjectId(string subjectId)
            => Cache.ContainsKey(subjectId);

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

        public TokenState Get(string subjectId)
        {
            Cache.TryGetValue(subjectId, out var data);
            return data;
        }

        public void Remove(string subjectId)
        {
            System.Diagnostics.Debug.WriteLine($"Removing sid: {subjectId}");
            Cache.TryRemove(subjectId, out _);
        }
    }
}