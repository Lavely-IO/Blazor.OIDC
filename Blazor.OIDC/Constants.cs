using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.OIDC
{
    public static class Constants
    {
        /// <summary>
        /// LoginHint.
        /// Represents the preferred_username claim in the ID token.
        /// </summary>
        public const string LoginHint = "loginHint";

        /// <summary>
        /// DomainHint.
        /// Determined by the tenant Id.
        /// </summary>
        public const string DomainHint = "domainHint";

        /// <summary>
        /// Claims.
        /// Determined from the signed-in user.
        /// </summary>
        public const string Claims = "claims";

        /// <summary>
        /// Bearer.
        /// Predominant type of access token used with OAuth 2.0.
        /// </summary>
        public const string Bearer = "Bearer";

        /// <summary>
        /// CertLynx
        /// Configuration section name for CertLynx Authentication.
        /// </summary>
        public const string BlazorOidc = "BlazorOidc";

        /// <summary>
        /// KeyCloak.
        /// Configuration section name for KeyCloak.
        /// </summary>
        public const string KeyCloak = "KeyCloak";

        /// <summary>
        /// CertLynx.
        /// Configuration section name for AzureAd.
        /// </summary>
        public const string AzureAd = "AzureAd";

        /// <summary>
        /// AzureAdB2C.
        /// Configuration section name for AzureAdB2C.
        /// </summary>
        public const string AzureAdB2C = "AzureAdB2C";

        /// <summary>Scope.</summary>
        public const string Scope = "scope";

        internal const string TenantDiscoveryEndpoint = "tenant_discovery_endpoint";
        internal const string ApiVersion = "api-version";
        internal const string Metadata = "metadata";
        internal const string PreferredNetwork = "preferred_network";
        internal const string PreferredCache = "preferred_cache";
        internal const string Aliases = "aliases";
        internal const string AzureADIssuerMetadataUrl = "https://login.microsoftonline.com/common/discovery/instance?authorization_endpoint=https://login.microsoftonline.com/common/oauth2/v2.0/authorize&api-version=1.1";
        internal const string FallbackAuthority = "https://login.microsoftonline.com/";
        internal const string Version = "ver";
        internal const string V1 = "1.0";
        internal const string V2 = "2.0";
        internal const string MsaTenantId = "9188040d-6c67-4c5b-b112-36a304b66dad";
        internal const string Consumers = "consumers";
        internal const string Organizations = "organizations";
        internal const string Common = "common";
        internal const string ClientInfo = "client_info";
        internal const string One = "1";
        internal const string MediaTypePksc12 = "application/x-pkcs12";
        internal const string PersonalUserCertificateStorePath = "CurrentUser/My";
        internal const string UserAgent = "User-Agent";
        internal const string JwtSecurityTokenUsedToCallWebApi = "JwtSecurityTokenUsedToCallWebAPI";
        internal const string PreferredUserName = "preferred_username";
        internal const string NameClaim = "name";
        internal const string Consent = "consent";
        internal const string ConsentUrl = "consentUri";
        internal const string Scopes = "scopes";
        internal const string ProposedAction = "proposedAction";
        internal const string Authorization = "Authorization";
        internal const string ApplicationJson = "application/json";
        internal const string ISessionStore = "ISessionStore";
        internal const string BlazorChallengeUri = "MicrosoftIdentity/Account/Challenge?redirectUri=";
        internal const string UserReadScope = "user.read";
        internal const string GraphBaseUrlV1 = "https://graph.microsoft.com/v1.0";
        internal const string TelemetryHeaderKey = "x-client-brkrver";
        internal const string IDWebSku = "IDWeb.";
    }
}