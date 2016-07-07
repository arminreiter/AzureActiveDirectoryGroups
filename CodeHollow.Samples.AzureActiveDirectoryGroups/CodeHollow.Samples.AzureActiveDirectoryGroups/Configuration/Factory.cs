using System.Globalization;

namespace CodeHollow.Samples.AzureActiveDirectoryGroups.Configuration
{
    public static class Factory
    {
        public static IClientConfiguration GetConfiguration()
        {
            return new MyConfiguration();
        }
        public static IClientConfiguration GetConfiguration(string configName)
        {
            switch (configName.ToLower(CultureInfo.CurrentCulture))
            {
                // case "test":
                //    return new MyTestConfiguration();
                case "appsettings":
                    return new AppConfigConfiguration();
                default:
                    return new MyConfiguration();
            }
        }
    }
}
