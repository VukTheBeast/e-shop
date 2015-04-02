using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "", // Only matches the empty URL (i.e. /)
                new
                {
                    controller = "Product",
                    action = "List",
                    category = "Products",
                    page = 1
                }
                );

            routes.MapRoute(null,
                "search/{pages}",
                new
                {
                    controller = "Product",
                    action = "Search",
                    category = "Search",
                    pages = @"\d+"
                });

            routes.MapRoute(null,
                "login",
                new {controller = "Account", action = "Login"});

            routes.MapRoute(null,
                "login{returnUrl}",
                new {controller = "Account", action = "Login", returnUrl = "ReturnUrl"});

            routes.MapRoute(null,
                "logoff",
                new {controller = "Account", action = "Logoff"});


            routes.MapRoute(null,
                "register",
                new {controller = "Account", action = "Register"});

            routes.MapRoute(null,
                "login/reset",
                new {controller = "Account", action = "ResetPassword"});

            routes.MapRoute(null,
                "login/restore",
                new {controller = "Account", action = "RestorePassword"});

            routes.MapRoute(null,
                "profile/change-password",
                new {controller = "Account", action = "Manage"});

            routes.MapRoute(null,
                "profile/orders",
                new {controller = "Manage", action = "Index"});

            routes.MapRoute(null,
                "profile/address",
                new {controller = "Manage", action = "Address"});

            routes.MapRoute(null,
                "profile/edit-address/{id}",
                new {controller = "Manage", action = "EditAddress", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "order/cart",
                new {controller = "Cart", action = "Index"});

            routes.MapRoute(null,
                "order/cart-add-item/{id}",
                new {controller = "Cart", action = "AddToCart", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "order/cart-remove-item/{id}",
                new {controller = "Cart", action = "RemoveFromCart", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "order/checkout/{id}",
                new {controller = "Cart", action = "Checkout", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "order/order-process",
                new {controller = "Cart", action = "OrderConfirm"});


            routes.MapRoute(null,
                "order/{id}",
                new {controller = "Cart", action = "Order", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "control/product/search/{page}",
                new { controller = "Admin", action = "Search", category = "Search", pages = @"\d+" });

            routes.MapRoute(null,
                "control/products/{category}/{page}/{id}",
                new
                {
                    controller = "Admin",
                    action = "ControlProducts",
                    category = "category",
                    page = @"\d+",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(null,
                "control/edit-product/{id}",
                new {controller = "Admin", action = "UpdateProduct", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "control/orders",
                new {controller = "Admin", action = "ConfirmOrder"});

            routes.MapRoute(null,
                "control/orders-details/{orderId}",
                new {controller = "Admin", action = "OrderDetails", orderId = UrlParameter.Optional});


            routes.MapRoute(null,
                "control/change-order/{id}",
                new {controller = "Admin", action = "ChangeOrderStatus", id = UrlParameter.Optional});


            routes.MapRoute(null,
                "control/category",
                new {controller = "Admin", action = "ControlCategory", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "control/access",
                new {controller = "Admin", action = "AccessUsers"});

            routes.MapRoute(null,
                "control/product-remove/{productId}",
                new {controller = "Admin", action = "RemoveProduct", id = UrlParameter.Optional});


            routes.MapRoute(null,
                "control/change-access",
                new {controller = "Admin", action = "ChangeAccessUsers"});


            routes.MapRoute(null,
                "control/category-add/{id}",
                new {controller = "Admin", action = "AddCategory", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "control/category-remove/{id}",
                new {controller = "Admin", action = "DeleteCategory", id = UrlParameter.Optional});

            //routes.MapRoute(null,
            //    "control/product-add/{id}",
            //    new {controller = "Admin", action = "RemoveProduct", id = UrlParameter.Optional});


            //routes.MapRoute(null,
            //    "{page}",
            //    new {controller = "Product", action = "List", category = (string) null},
            //    new {page = @"\d+"} // Constraints: page must be numerical
            //    );


            routes.MapRoute(null,
                "item/{id}",
                new {controller = "Product", action = "Details", id = UrlParameter.Optional});

            routes.MapRoute(null,
                "{category}/{page}/{id}",
                new
                {
                    controller = "Product",
                    action = "List",
                    category = "category",
                    page = @"\d+",
                    id = UrlParameter.Optional
                }
                );

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Product", action = "List", id = UrlParameter.Optional}
                );
        }
    }
}