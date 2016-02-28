using CodeHollow.Samples.AzureActiveDirectoryGroups.Configuration;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace CodeHollow.Samples.AzureActiveDirectoryGroups
{
    // inspired by: https://github.com/Azure-Samples/active-directory-dotnet-graphapi-console
    internal class AzureAuthenticationHelper
    {
        private string _tokenForUser;
        private IClientConfiguration _config;

        private Uri ServiceRoot { get { return new Uri(new Uri(_config.ResourceUrl), _config.TenantId); } }

        public AzureAuthenticationHelper(IClientConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Get Active Directory Client for Application.
        /// </summary>
        /// <returns>ActiveDirectoryClient for Application.</returns>
        public ActiveDirectoryClient GetActiveDirectoryClientAsApplication()
        {
            var adClient = new ActiveDirectoryClient(ServiceRoot, async () => await AcquireTokenAsyncForApplication());
            return adClient;
        }

        /// <summary>
        /// Get Active Directory Client for User.
        /// </summary>
        /// <returns>ActiveDirectoryClient for User.</returns>
        public ActiveDirectoryClient GetActiveDirectoryClientAsUser()
        {
            var adClient = new ActiveDirectoryClient(ServiceRoot, async () => await AcquireTokenAsyncForUser());
            return adClient;
        }

        /// <summary>
        /// Async task to acquire token for Application.
        /// </summary>
        /// <returns>Token for application.</returns>
        private async Task<string> AcquireTokenAsyncForApplication()
        {
            return await Task.Run<string>(() => GetTokenForApplication());
            // return GetTokenForApplication() would also be enought, but compiler will throw a warning
        }

        /// <summary>
        /// Async task to acquire token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        private async Task<string> AcquireTokenAsyncForUser()
        {
            return await Task.Run<string>(() => GetTokenForUser());
        }

        /// <summary>
        /// Get Token for Application.
        /// </summary>
        /// <returns>Token for application.</returns>
        private string GetTokenForApplication()
        {
            AuthenticationContext authenticationContext = new AuthenticationContext(_config.AuthString, false);
            // Config for OAuth client credentials 
            ClientCredential clientCred = new ClientCredential(_config.ClientId, _config.ClientSecret);
            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(_config.ResourceUrl,
                clientCred);
            string token = authenticationResult.AccessToken;
            return token;
        }

        /// <summary>
        /// Get Token for User.
        /// </summary>
        /// <returns>Token for user.</returns>
        private string GetTokenForUser()
        {
            if (_tokenForUser == null)
            {
                var redirectUri = new Uri("https://localhost");
                AuthenticationContext authenticationContext = new AuthenticationContext(_config.AuthString, false);
                AuthenticationResult userAuthnResult = authenticationContext.AcquireToken(_config.ResourceUrl,
                    _config.ClientIdForUserAuthn, redirectUri, PromptBehavior.Always);
                _tokenForUser = userAuthnResult.AccessToken;
                Console.WriteLine("\n Welcome " + userAuthnResult.UserInfo.GivenName + " " +
                                  userAuthnResult.UserInfo.FamilyName);
            }
            return _tokenForUser;
        }
    }
}
