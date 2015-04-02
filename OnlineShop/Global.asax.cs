using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OnlineShop.Infrastructure;
using OnlineShop.Models;

namespace OnlineShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            ModelBinders.Binders.Add(typeof (Cart), new CartModelBinder());
            
        }
    }
}