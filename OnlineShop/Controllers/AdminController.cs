using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineShop.Logic;
using OnlineShop.Logic.Entity;
using OnlineShop.Logic.Interface;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "Moderator, Admin")]
    public class AdminController : Controller
    {
        public int PageSize = 15;
        private readonly IOrder _order;
        private readonly IProduct _product;
        private readonly IEmail _email;
        private readonly ICategory _category;
        private readonly IAccess _access;
        private IAddress _address;
        private readonly string INITIAL_CATALOG = AppDomain.CurrentDomain.BaseDirectory;

        public AdminController(IOrder order, IProduct product, IEmail email, ICategory category, IAccess access,
            IAddress address)
        {
            _order = order;
            _product = product;
            _email = email;
            _category = category;
            _access = access;
            _address = address;
        }

        public ActionResult ControlProducts(string category = "", int page = 1)
        {
            var viewModel = new ProductsListViewModel
            {
                Products = _product.GetProducts(category)
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null
                        ? _product.GetProducts("").Count()
                        : _product.GetProducts(category).Count()
                },
                CurrentCategory = category
            };
            TempData["cat"] = category;
            return View(viewModel);
        }

        public ViewResult Search(string value, string category = "Admin Search", int pages = 1)
        {
            var viewSearch = new ProductsListViewModel
            {
                Products = _product.GetProducts(category, value)
                    .Skip((pages - 1)*PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pages,
                    ItemsPerPage = PageSize,
                    TotalItems = _product.GetProducts(category, value).Count()
                },
                CurrentCategory = category
            };
            ViewBag.SearchValue = value;
            return View(viewSearch);
        }


        public ActionResult ConfirmOrder()
        {
            var model = _order.GetOrders("");
            return View(model);
        }

        public ViewResult UpdateProduct(int productId, string returnUrl)
        {
            var product = _product.GetProduct(productId) ?? new Product();
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.CategoryList = new SelectList(_category.GetCategory(), "Id", "Name");
            return View(product);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product, string returnUrl)
        {
            object image = Request.Files[0];
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var img = image as HttpPostedFileBase;
                    if (img.ContentLength > 0)
                    {
                        img.SaveAs(INITIAL_CATALOG + @"/Images/Items/" + img.FileName);
                        product.Image = @"/Images/Items/" + img.FileName;
                    }
                    else
                    {
                        if (product.Image == null)
                            product.Image = @"/Images/Items/default.jpg";
                    }
                }
                _product.SaveProduct(product);
            }

            return Redirect(returnUrl);
        }

        public ActionResult RemoveProduct(int productId, string returnUrl)
        {
            _product.RemoveProduct(productId);

            return Redirect(returnUrl);
        }

        public RedirectToRouteResult ChangeOrderStatus(int orderId)
        {
            _order.ChangeOrderStatus(orderId);
            var order = _order.GetOrder(orderId);
            _email.OrderProcess(order.User.FirstName + " " + order.User.LastName, order.User.Email, order);
            return RedirectToAction("ConfirmOrder");
        }

        [Authorize(Roles = "Admin")]
        public ViewResult AccessUsers()
        {
            var model = _access.GetAccessModel();
            ViewBag.CategoryList = new SelectList(_access.GetRoles(), "Name");
            return View(model);
        }

        public RedirectToRouteResult ChangeAccessUsers(AccessModel model)
        {
            var roles = Roles.GetRolesForUser(model.Email);
            foreach (var role in roles)
            {
                Roles.RemoveUserFromRole(model.Email, role);
            }
            Roles.AddUserToRole(model.Email, model.Role);
            return RedirectToAction("AccessUsers");
        }

        [Authorize(Roles = "Admin")]
        public ViewResult ControlCategory()
        {
            var model = _category.GetCategory();
            return View(model);
        }

        [HttpPost]
        public RedirectToRouteResult AddCategory(string name)
        {
            if (name != "") { 
             _category.AddCategory(name);
            }

            return RedirectToAction("ControlCategory");
        }


        public RedirectToRouteResult DeleteCategory(int categoryId)
        {
            var isCheck = _category.DeleteCategory(categoryId);
            if (isCheck)
            {
                TempData["Message"] = "Unable to delete a category containing products.";
            }
            else
            {
                TempData["Message"] = "Category successfully deleted!";
            }
            return RedirectToAction("ControlCategory");
        }

        public ViewResult OrderDetails(int orderId)
        {
            var order = _order.GetOrderModel(orderId);
            var address = _address.GetAdress(order.AddressId);
            var model = new OrderDetailsModel {OrderModel = order, Address = address};
            ViewBag.Order = "true";
            return View(model);
        }
    }
}