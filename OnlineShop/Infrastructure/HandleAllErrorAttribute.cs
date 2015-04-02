using System;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Infrastructure
{
    public class HandleAllErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var exception = filterContext.Exception;

            if (!ExceptionType.IsInstanceOfType(exception))
            {
                return;
            }

            string controllerName = (string) filterContext.RouteData.Values["controller"];
            string actionName = (string) filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
            filterContext.Result = new ViewResult
            {
                ViewName = View,
                MasterName = Master,
                ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                TempData = filterContext.Controller.TempData
            };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = new HttpException(null, exception).GetHttpCode();

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}