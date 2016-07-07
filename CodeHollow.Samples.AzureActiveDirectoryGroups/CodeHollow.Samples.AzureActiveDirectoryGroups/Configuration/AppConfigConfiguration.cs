using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.Azure;

namespace CodeHollow.Samples.AzureActiveDirectoryGroups.Configuration
{
    public class AppConfigConfiguration : IClientConfiguration
    {
        public string TenantName { get { return CloudConfigurationManager.GetSetting("TenantName"); } }
        public string TenantId { get { return CloudConfigurationManager.GetSetting("TenantId"); } }
        public string ClientId { get { return CloudConfigurationManager.GetSetting("ClientId"); } }
        public string ClientSecret { get { return CloudConfigurationManager.GetSetting("ClientSecret"); } }
        public string ClientIdForUserAuthn { get { return ""; } }
        public string AuthString { get { return "https://login.microsoftonline.com/" + TenantName; } }
        public string ResourceUrl { get { return "https://graph.windows.net"; } }
    }
}
