// lavely.io

namespace Blazor.OIDC.Pages
{
    using Blazor.OIDC.State;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Class _HostModel.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    public class _HostModel : PageModel
    {
        /// <summary>
        /// The cache
        /// </summary>
        public readonly CachedState Cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="_HostModel"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        public _HostModel(CachedState cache)
        {
            Cache = cache;
        }

        /// <summary>Called when [get].</summary>
        /// <returns>IActionResult.</returns>
        public async Task<IActionResult> OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                var sid = User.GetClaim("sid").Value;

                if (Cache.HasSubjectId(sid))
                {
                    var authResult = await HttpContext.AuthenticateAsync("oidc");
                    DateTimeOffset expiration = authResult.Properties.ExpiresUtc.Value;
                    string accessToken = await HttpContext.GetTokenAsync("access_token");
                    string refreshToken = await HttpContext.GetTokenAsync("refresh_token");
                    Cache.Add(sid, expiration, accessToken, refreshToken);
                }
            }

            return Page();
        }

        /// <summary>
        /// Called when [get login].
        /// </summary>
        /// <returns>IActionResult.</returns>
        public IActionResult OnGetLogin()
        {
            var authProps = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(15),
                RedirectUri = Url.Content("~/")
            };
            return Challenge(authProps, "oidc");
        }

        /// <summary>
        /// Called when [get logout].
        /// </summary>
        /// <returns> void </returns>
        public async Task OnGetLogout()
        {
            var authProps = new AuthenticationProperties
            {
                RedirectUri = Url.Content("~/")
            };
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc", authProps).ConfigureAwait(true);
        }
    }
}