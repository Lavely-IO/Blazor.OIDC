// lavely.io

namespace Blazor.OIDC.Config
{
    /// <summary>
    /// Class OidcWellKnown.
    /// </summary>
    public class OidcWellKnown
    {
        /// <summary>
        /// Gets or sets the authorization endpoint.
        /// </summary>
        /// <value>The authorization endpoint.</value>
        public string authorization_endpoint { get; set; }

        /// <summary>
        /// Gets or sets the token endpoint.
        /// </summary>
        /// <value>The token endpoint.</value>
        public string token_endpoint { get; set; }

        /// <summary>
        /// Gets or sets the token endpoint authentication methods supported.
        /// </summary>
        /// <value>The token endpoint authentication methods supported.</value>
        public List<string> token_endpoint_auth_methods_supported { get; set; }

        /// <summary>
        /// Gets or sets the JWKS URI.
        /// </summary>
        /// <value>The JWKS URI.</value>
        public string jwks_uri { get; set; }

        /// <summary>
        /// Gets or sets the response modes supported.
        /// </summary>
        /// <value>The response modes supported.</value>
        public List<string> response_modes_supported { get; set; }

        /// <summary>
        /// Gets or sets the subject types supported.
        /// </summary>
        /// <value>The subject types supported.</value>
        public List<string> subject_types_supported { get; set; }

        /// <summary>
        /// Gets or sets the identifier token signing alg values supported.
        /// </summary>
        /// <value>The identifier token signing alg values supported.</value>
        public List<string> id_token_signing_alg_values_supported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [HTTP logout supported].
        /// </summary>
        /// <value><c>true</c> if [HTTP logout supported]; otherwise, <c>false</c>.</value>
        public bool http_logout_supported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [frontchannel logout supported].
        /// </summary>
        /// <value><c>true</c> if [frontchannel logout supported]; otherwise, <c>false</c>.</value>
        public bool frontchannel_logout_supported { get; set; }

        /// <summary>
        /// Gets or sets the end session endpoint.
        /// </summary>
        /// <value>The end session endpoint.</value>
        public string end_session_endpoint { get; set; }

        /// <summary>
        /// Gets or sets the response types supported.
        /// </summary>
        /// <value>The response types supported.</value>
        public List<string> response_types_supported { get; set; }

        /// <summary>
        /// Gets or sets the scopes supported.
        /// </summary>
        /// <value>The scopes supported.</value>
        public List<string> scopes_supported { get; set; }

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        /// <value>The issuer.</value>
        public string issuer { get; set; }

        /// <summary>
        /// Gets or sets the claims supported.
        /// </summary>
        /// <value>The claims supported.</value>
        public List<string> claims_supported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [request URI parameter supported].
        /// </summary>
        /// <value><c>true</c> if [request URI parameter supported]; otherwise, <c>false</c>.</value>
        public bool request_uri_parameter_supported { get; set; }

        /// <summary>
        /// Gets or sets the tenant region scope.
        /// </summary>
        /// <value>The tenant region scope.</value>
        public string tenant_region_scope { get; set; }

        /// <summary>
        /// Gets or sets the name of the cloud instance.
        /// </summary>
        /// <value>The name of the cloud instance.</value>
        public string cloud_instance_name { get; set; }

        /// <summary>
        /// Gets or sets the name of the cloud graph host.
        /// </summary>
        /// <value>The name of the cloud graph host.</value>
        public string cloud_graph_host_name { get; set; }

        /// <summary>
        /// Gets or sets the msgraph host.
        /// </summary>
        /// <value>The msgraph host.</value>
        public string msgraph_host { get; set; }

        /// <summary>
        /// Gets or sets the rbac URL.
        /// </summary>
        /// <value>The rbac URL.</value>
        public string rbac_url { get; set; }
    }
}