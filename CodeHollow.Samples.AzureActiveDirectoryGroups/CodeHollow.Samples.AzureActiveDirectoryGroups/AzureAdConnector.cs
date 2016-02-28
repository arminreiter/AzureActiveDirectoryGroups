using CodeHollow.Samples.AzureActiveDirectoryGroups.Configuration;
using CodeHollow.Samples.AzureActiveDirectoryGroups.Extensions;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CodeHollow.Samples.AzureActiveDirectoryGroups
{
    // inspired by: https://github.com/Azure-Samples/active-directory-dotnet-graphapi-console
    public class AzureAdConnector
    {
        ActiveDirectoryClient _client;
        IClientConfiguration _config;

        public AzureAdConnector(IClientConfiguration config)
        {
            _config = config;
            var auth = new AzureAuthenticationHelper(config);
            _client = auth.GetActiveDirectoryClientAsApplication();
        }

        public ITenantDetail GetTenantDetails()
        {
            List<ITenantDetail> tenantsList = _client.TenantDetails
                    .Where(tenantDetail => tenantDetail.ObjectId.Equals(_config.TenantId))
                    .ExecuteAsync().Result.CurrentPage.ToList();
            if (tenantsList.Count > 0)
                return tenantsList.First();

            return null;
        }
        
        public List<IGroup> GetAllAdGroups()
        {
            // you can read a maximum of 999 groups per query
            return _client.Groups.Take(999).ExecuteAsync().Result.GetAll();
            
        }

        public List<IGroup> SearchAdGroups(string name)
        {
            return _client.Groups.Where(group => group.DisplayName.StartsWith(name)).
                Take(999).ExecuteAsync().Result.GetAll();
        }

        internal void CreateUser(IUser user)
        {
            _client.Users.AddUserAsync(user).Wait();
        }

        public List<IUser> GetUsers()
        {
            return _client.Users.Take(999).ExecuteAsync().Result.GetAll();
        }

        public List<IUser> SearchUser(string searchString)
        {
            IUserCollection userCollection = _client.Users;
            var searchResults = userCollection.Where(user =>
                user.UserPrincipalName.StartsWith(searchString) ||
                user.DisplayName.StartsWith(searchString) ||
                user.GivenName.StartsWith(searchString) ||
                user.Surname.StartsWith(searchString)).ExecuteAsync().Result;
            return searchResults.CurrentPage.ToList();
        }

        public List<IDevice> GetDevices()
        {
            return _client.Devices.ExecuteAsync().Result.GetAll();
        }

        public List<IContact> GetContacts()
        {
            return _client.Contacts.ExecuteAsync().Result.GetAll();
        }

        public void CreateGroup(IGroup group)
        {
            _client.Groups.AddGroupAsync(group).Wait();
        }
    }
}
