using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.OIDC.State
{
    public class TokenState
    {
        public string SubjectId;
        public DateTimeOffset Expiration;
        public string AccessToken;
        public string RefreshToken;
    }
}