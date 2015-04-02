using System.Web.Mvc;
using OnlineShop.Infrastructure;

namespace OnlineShop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleAllErrorAttribute());
        }
    }
}