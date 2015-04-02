using System.Linq;
using System.Web.Mvc;
using OnlineShop.Logic.Interface;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 6;
        private readonly IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }

        public ViewResult List(string category, int page = 1)
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
            return View(viewModel);
        }

        public ViewResult Search(string value, string category = "Search", int pages = 1)
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

        public ViewResult Details(int id)
        {
            var model = _product.GetProduct(id);

            return View(model);
        }
    }
}