using Microsoft.Azure.ActiveDirectory.GraphClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHollow.Samples.AzureActiveDirectoryGroups.TestData
{
    public class TestDataGenerator
    {
        private const string TEST_GROUPS_PREFIX = "Testgroup";
        private const string TEST_USERS_PREFIX = "Testuser";
        private const string TEST_USERS_PASSWORD = "Testpassword";

        AzureAdConnector _client;
        public TestDataGenerator(AzureAdConnector client)
        {
            _client = client;
        }

        public void GenerateTestGroups(int numberOfGroups)
        {
            for (int i = 1; i <= numberOfGroups; i++)
            {
                IGroup group = new Group();
                group.DisplayName = TEST_GROUPS_PREFIX + i.ToString().PadLeft(4, '0');
                group.MailNickname = TEST_GROUPS_PREFIX + i.ToString().PadLeft(4, '0');
                group.MailEnabled = false;
                group.SecurityEnabled = true;
                _client.CreateGroup(group);
            }
        }

        public void DeleteTestGroups()
        {
            var groups = _client.SearchAdGroups(TEST_GROUPS_PREFIX);
            foreach(var g in groups)
            {
                g.DeleteAsync().Wait();
            }
        }

        public void GenerateTestUsers(int numberOfUsers, string tenantname)
        {
            for(int i = 1; i <= numberOfUsers; i++)
            {
                IUser user = new User();
                user.MailNickname = user.GivenName = user.DisplayName = TEST_USERS_PREFIX + i.ToString().PadLeft(4, '0');
                user.UserPrincipalName = user.MailNickname + "@" + tenantname;
                user.PasswordProfile = new PasswordProfile() { Password = TEST_USERS_PASSWORD + i.ToString(), ForceChangePasswordNextLogin = false };
                user.AccountEnabled = false;
                _client.CreateUser(user);
            }
        }

        public void DeleteTestUsers()
        {
            var users = _client.SearchUsers(TEST_USERS_PREFIX);
            foreach(var u in users)
            {
                u.DeleteAsync().Wait();
            }
        }
    }
}
