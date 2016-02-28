namespace CodeHollow.Samples.AzureActiveDirectoryGroups.Configuration
{
    public interface IClientConfiguration
    {
        string TenantName { get; }
        string TenantId { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        string ClientIdForUserAuthn { get; }
        string AuthString { get; }
        string ResourceUrl { get; }
    }
}
