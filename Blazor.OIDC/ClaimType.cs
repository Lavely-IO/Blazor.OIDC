using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.OIDC
{
    public struct ClaimType
    {
        public static string AccessToken => "access_token";

        public static string RefreshToken => "refresh_token";

        public static string AccessTokenExpires => "access_token_expires";

        public static string RefreshTokenExpires => "refresh_token_expires";
    }
}