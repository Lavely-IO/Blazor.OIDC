using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blazor.OIDC.Pages
{
    using Blazor.OIDC.State;

    public class _HostModel : PageModel
    {
        public readonly CachedState Cache;

        public _HostModel(CachedState cache)
        {
            Cache = cache;
        }

        public async Task<IActionResult> OnGet()
        {
            System.Diagnostics.Debug.WriteLine($"\n_Host checking userIsAuthenticated: {User?.Identity?.IsAuthenticated}");

            if (User != null && this.User.Identity!.IsAuthenticated)
            {
                var sid = User.GetClaim("sid").Value;

                System.Diagnostics.Debug.WriteLine($"sid: {sid}");

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

        public IActionResult OnGetLogin()
        {
            System.Diagnostics.Debug.WriteLine("\n_Host OnGetLogin");
            var authProps = new AuthenticationProperties
            {
                IsPersistent = true,
                //ExpiresUtc = DateTimeOffset.UtcNow.AddHours(15),
                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(30),
                RedirectUri = Url.Content("~/")
            };
            return Challenge(authProps, "oidc");
        }

        public async Task OnGetLogout()
        {
            var authProps = new AuthenticationProperties
            {
                RedirectUri = Url.Content("~/")
            };
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc", authProps);
        }
    }
}