using Microsoft.Azure.ActiveDirectory.GraphClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHollow.Samples.AzureActiveDirectoryGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(150 > Console.LargestWindowWidth ? Console.LargestWindowWidth : 150, Console.WindowHeight);

            string configName = GetConfigName();
            AzureAdConnector connector = new AzureAdConnector(Configuration.Factory.GetConfiguration(configName));
            
            var tenant = connector.GetTenantDetails();
            if (tenant == null)
            {
                Console.WriteLine("Tenant not found");
                return;
            }
            PrintTenantDetails(tenant);
            PrintLine();

            Console.WriteLine("Get all AD groups...");

            List<IGroup> foundGroups = connector.GetAllAdGroups();
            PrintGroups(foundGroups);
            PrintLine();

            var users = connector.GetUsers();
            PrintUsers(users);
            PrintLine();

            var contacts = connector.GetContacts();
            PrintContacts(contacts);
            PrintLine();

            Console.WriteLine("Search for group: ");
            var searchStr = Console.ReadLine();
            foundGroups = connector.SearchAdGroups(searchStr);
            PrintGroups(foundGroups);

            Console.WriteLine("done");
            Console.ReadLine();
        }
        
        private static string GetConfigName()
        {
            Console.WriteLine("Please enter the name of the configuration you want to use:");
            return Console.ReadLine();
        }

        private static void PrintLine()
        {
            Console.WriteLine(new string('=', Console.WindowWidth - 1));
        }

        private static void PrintContacts(List<IContact> contacts)
        {
            contacts.ForEach(c => Console.WriteLine(c.DisplayName));
        }
        
        private static void PrintUsers(List<IUser> users)
        {
            int pad = 30;
            users.ForEach(user => 
                Console.WriteLine((user.DisplayName ?? "").PadRight(pad) + (user.GivenName ?? "").PadRight(pad) + 
                user.UserPrincipalName));
        }

        private static void PrintTenantDetails(ITenantDetail tenant)
        {
            TenantDetail tenantDetail = (TenantDetail)tenant;
            Console.WriteLine("Tenant Display Name: " + tenantDetail.DisplayName);

            var initialDomain = tenantDetail.VerifiedDomains.First(x => x.Initial.HasValue && x.Initial.Value);
            Console.WriteLine("Initial Domain Name: " + initialDomain.Name);
            var defaultDomain = tenantDetail.VerifiedDomains.First(x => x.@default.HasValue && x.@default.Value);
            Console.WriteLine("Default Domain Name: " + defaultDomain.Name);

            // Get Tenant's Tech Contacts
            foreach (string techContact in tenantDetail.TechnicalNotificationMails)
            {
                Console.WriteLine("Tenant Tech Contact: " + techContact);
            }
        }

        private static void PrintGroups(List<IGroup> groups)
        {
            groups.ForEach(g => Console.WriteLine(g.DisplayName.PadRight(30) + g.ObjectId));
        }
    }
}
