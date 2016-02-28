using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHollow.Samples.AzureActiveDirectoryGroups.Configuration
{
    public class AppConfigConfiguration : IClientConfiguration
    {
        public string TenantName { get { return ConfigurationManager.AppSettings["TenantName"]; } }
        public string TenantId { get { return ConfigurationManager.AppSettings["TenantId"]; } }
        public string ClientId { get { return ConfigurationManager.AppSettings["ClientId"]; } }
        public string ClientSecret { get { return ConfigurationManager.AppSettings["ClientSecret"]; } }
        public string ClientIdForUserAuthn { get { return ""; } }
        public string AuthString { get { return "https://login.microsoftonline.com/" + TenantName; } }
        public string ResourceUrl { get { return "https://graph.windows.net"; } }
    }
}
