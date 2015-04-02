using System.Linq;
using System.Web.Mvc;
using OnlineShop.Logic;
using OnlineShop.Logic.Interface;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        private readonly IAccess _access;
        private readonly IEmail _email;
        private readonly IAddress _address;
        private readonly IProduct _product;
        private readonly IOrder _order;

        public CartController(IAccess access, IAddress address, IEmail email, IProduct product, IOrder order)
        {
            _access = access;
            _address = address;
            _email = email;
            _product = product;
            _order = order;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,
            int productId, string returnUrl)
        {
            var product = _product.GetProduct(productId);

            cart.RemoveLine(product);
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult AddToCart(Cart cart, int Id, string returnUrl)
        {
            var product = _product.GetProduct(Id);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public ViewResult Checkout(Cart cart, string returnUrl)
        {
            var address = _address.GetAllAddress(User.Identity.Name).ToList();
            return View(new CartCheckoutViewModel
            {
                Cart = cart,
                Address = address
            });
        }

        public ViewResult Order(Cart cart, int addressId)
        {
            var address = _address.GetAllAddress(User.Identity.Name).Where(x => x.Id == addressId).ToList();
            Session["AddressId"] = addressId;
            ViewBag.Order = "true";
            return View(new CartCheckoutViewModel
            {
                Cart = cart,
                Address = address
            });
        }

        public RedirectToRouteResult OrderConfirm(Cart cart)
        {
            var addressId = (int) Session["AddressId"];
            var address = _address.GetAdress(addressId);
            var products = cart.Lines;
            _order.AddOrder(addressId, products, User.Identity.Name);
            _email.NewOrder(_access.GetUserName(User.Identity.Name), User.Identity.Name, cart, address);
            cart.Clear();
            return RedirectToAction("Index", "Manage");
        }

        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }

    }
}