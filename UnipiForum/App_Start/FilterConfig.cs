using System.Web;
using System.Web.Mvc;
using UnipiForum.Infrastructure;

namespace UnipiForum
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TransactionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
