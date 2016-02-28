namespace CodeHollow.Samples.AzureActiveDirectoryGroups.Configuration
{
    public class MyConfiguration : IClientConfiguration
    {
        // enter your azure ad configuration here
        public string TenantName           { get { return ""; } }
        public string TenantId             { get { return ""; } }
        public string ClientId             { get { return ""; } }
        public string ClientSecret         { get { return ""; } }
        public string ClientIdForUserAuthn { get { return ""; } }
        public string AuthString           { get { return "https://login.microsoftonline.com/" + TenantName; } }
        public string ResourceUrl          { get { return "https://graph.windows.net"; } }
    }
}
