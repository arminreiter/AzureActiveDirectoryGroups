using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHollow.Samples.AzureActiveDirectoryGroups.Extensions
{
    public static class PagedCollectionExtensions
    {
        public static List<T> GetAll<T>(this IPagedCollection<T> p)
        {
            List<T> result = p.CurrentPage.ToList();
            while(p.MorePagesAvailable)
            {
                p = p.GetNextPageAsync().Result;
                result.AddRange(p.CurrentPage);
            }
            return result;
        }
    }
}
