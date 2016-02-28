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
            switch (configName.ToLower())
            {
                // case "test":
                //    return new MyTestConfiguration();
                default:
                    return new MyConfiguration();
            }
        }
    }
}
