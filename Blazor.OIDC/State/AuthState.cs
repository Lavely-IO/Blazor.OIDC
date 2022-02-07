using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.OIDC.State
{
    using Blazor.OIDC;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.Server;
    using Microsoft.Extensions.Logging;

    public class AuthState : RevalidatingServerAuthenticationStateProvider
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private readonly CachedState Cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthState"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        /// <param name="cache">
        /// The cache.
        /// </param>
        public AuthState(
            ILoggerFactory loggerFactory,
            CachedState cache)
            : base(loggerFactory)
        {
            Cache = cache;
        }

        /// <summary>
        /// The revalidation interval.
        /// </summary>
        protected override TimeSpan RevalidationInterval
            => TimeSpan.FromSeconds(10); // TODO read from ConfigurationSection

        /// <summary>
        /// The validate authentication state async.
        /// </summary>
        /// <param name="authenticationState">
        /// The authentication state.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            var sid = authenticationState.User.GetClaim("sid").Value;
            var email = authenticationState.User.GetClaim("email").Value;
            var name = authenticationState.User.GetClaim("name").Value;

            if (this.Cache.HasSubjectId(sid))
            {
                var data = this.Cache.Get(sid);

                System.Diagnostics.Debug.WriteLine($"NowUtc: {DateTimeOffset.UtcNow:o}");
                System.Diagnostics.Debug.WriteLine($"ExpUtc: {data.Expiration:o}");

                if (DateTimeOffset.UtcNow < data.Expiration)
                {
                    return Task.FromResult(true);
                }

                System.Diagnostics.Debug.WriteLine($"Claim Status: EXPIRED");
                this.Cache.Remove(sid);
                return Task.FromResult(false);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"(Claim is not cached)");
            }

            return Task.FromResult(true);
        }
    }
}