// ***********************************************************************
// Assembly         : Blazor.OIDC
// Author           : joshl
// Created          : 02-07-2022
//
// Last Modified By : joshl
// Last Modified On : 02-07-2022
// ***********************************************************************
// <copyright file="Constants.cs" company="LavelyIO">
//     Copyright (c) LavelyIO. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Blazor.OIDC
{
    /// <summary>
    /// Class Constants.
    /// </summary>
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

        /// <summary>
        /// Scope.
        /// </summary>
        public const string Scope = "scope";

        /// <summary>
        /// The tenant discovery endpoint
        /// </summary>
        internal const string TenantDiscoveryEndpoint = "tenant_discovery_endpoint";

        /// <summary>
        /// The API version
        /// </summary>
        internal const string ApiVersion = "api-version";

        /// <summary>
        /// The metadata
        /// </summary>
        internal const string Metadata = "metadata";

        /// <summary>
        /// The preferred network
        /// </summary>
        internal const string PreferredNetwork = "preferred_network";

        /// <summary>
        /// The preferred cache
        /// </summary>
        internal const string PreferredCache = "preferred_cache";

        /// <summary>
        /// The aliases
        /// </summary>
        internal const string Aliases = "aliases";

        /// <summary>
        /// The azure ad issuer metadata URL
        /// </summary>
        internal const string AzureADIssuerMetadataUrl =
            "https://login.microsoftonline.com/common/discovery/instance?authorization_endpoint=https://login.microsoftonline.com/common/oauth2/v2.0/authorize&api-version=1.1";

        /// <summary>
        /// The fallback authority
        /// </summary>
        internal const string FallbackAuthority = "https://login.microsoftonline.com/";

        /// <summary>
        /// The version
        /// </summary>
        internal const string Version = "ver";

        /// <summary>
        /// The v1
        /// </summary>
        internal const string V1 = "1.0";

        /// <summary>
        /// The v2
        /// </summary>
        internal const string V2 = "2.0";

        /// <summary>
        /// The msa tenant identifier
        /// </summary>
        internal const string MsaTenantId = "9188040d-6c67-4c5b-b112-36a304b66dad";

        /// <summary>
        /// The consumers
        /// </summary>
        internal const string Consumers = "consumers";

        /// <summary>
        /// The organizations
        /// </summary>
        internal const string Organizations = "organizations";

        /// <summary>
        /// The common
        /// </summary>
        internal const string Common = "common";

        /// <summary>
        /// The client information
        /// </summary>
        internal const string ClientInfo = "client_info";

        /// <summary>
        /// The one
        /// </summary>
        internal const string One = "1";

        /// <summary>
        /// The media type PKSC12
        /// </summary>
        internal const string MediaTypePksc12 = "application/x-pkcs12";

        /// <summary>
        /// The personal user certificate store path
        /// </summary>
        internal const string PersonalUserCertificateStorePath = "CurrentUser/My";

        /// <summary>
        /// The user agent
        /// </summary>
        internal const string UserAgent = "User-Agent";

        /// <summary>
        /// The JWT security token used to call web API
        /// </summary>
        internal const string JwtSecurityTokenUsedToCallWebApi = "JwtSecurityTokenUsedToCallWebAPI";

        /// <summary>
        /// The preferred user name
        /// </summary>
        internal const string PreferredUserName = "preferred_username";

        /// <summary>
        /// The name claim
        /// </summary>
        internal const string NameClaim = "name";

        /// <summary>
        /// The consent
        /// </summary>
        internal const string Consent = "consent";

        /// <summary>
        /// The consent URL
        /// </summary>
        internal const string ConsentUrl = "consentUri";

        /// <summary>
        /// The scopes
        /// </summary>
        internal const string Scopes = "scopes";

        /// <summary>
        /// The proposed action
        /// </summary>
        internal const string ProposedAction = "proposedAction";

        /// <summary>
        /// The authorization
        /// </summary>
        internal const string Authorization = "Authorization";

        /// <summary>
        /// The application json
        /// </summary>
        internal const string ApplicationJson = "application/json";

        /// <summary>
        /// The i session store
        /// </summary>
        internal const string ISessionStore = "ISessionStore";

        /// <summary>
        /// The blazor challenge URI
        /// </summary>
        internal const string BlazorChallengeUri = "MicrosoftIdentity/Account/Challenge?redirectUri=";

        /// <summary>
        /// The user read scope
        /// </summary>
        internal const string UserReadScope = "user.read";

        /// <summary>
        /// The graph base URL v1
        /// </summary>
        internal const string GraphBaseUrlV1 = "https://graph.microsoft.com/v1.0";

        /// <summary>
        /// The telemetry header key
        /// </summary>
        internal const string TelemetryHeaderKey = "x-client-brkrver";

        /// <summary>
        /// The identifier web sku
        /// </summary>
        internal const string IDWebSku = "IDWeb.";
    }
}