// lavely.io

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Logging;

namespace Blazor.OIDC.State
{
    /// <summary>
    /// Class AuthState.
    /// Implements the <see cref="Microsoft.AspNetCore.Components.Server.RevalidatingServerAuthenticationStateProvider" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Components.Server.RevalidatingServerAuthenticationStateProvider" />
    /// <remarks>lavely.io</remarks>
    public class AuthState : RevalidatingServerAuthenticationStateProvider
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private readonly CachedState Cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthState" /> class.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="cache">The cache.</param>
        /// <remarks>lavely.io</remarks>
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
        /// <value>The revalidation interval.</value>
        /// <remarks>lavely.io</remarks>
        protected override TimeSpan RevalidationInterval
            => TimeSpan.FromSeconds(10); // TODO read from ConfigurationSection

        /// <summary>
        /// The validate authentication state async.
        /// </summary>
        /// <param name="authenticationState">The authentication state.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task" />.</returns>
        /// <remarks>lavely.io</remarks>
        protected override Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState,
            CancellationToken cancellationToken)
        {
            var sid = authenticationState.User.GetClaim("sid").Value;
            var email = authenticationState.User.GetClaim("email").Value;
            var name = authenticationState.User.GetClaim("name").Value;

            if (Cache.HasSubjectId(sid))
            {
                var data = Cache.Get(sid);

                System.Diagnostics.Debug.WriteLine($"NowUtc: {DateTimeOffset.UtcNow:o}");
                System.Diagnostics.Debug.WriteLine($"ExpUtc: {data.Expiration:o}");

                if (DateTimeOffset.UtcNow < data.Expiration)
                {
                    return Task.FromResult(true);
                }

                System.Diagnostics.Debug.WriteLine($"Claim Status: EXPIRED");
                Cache.Remove(sid);
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