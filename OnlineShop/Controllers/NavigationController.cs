using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineShop.Logic.Interface;
using OnlineShop.Models;
using OnlineShop.Logic.Entity;

namespace OnlineShop.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ICategory _category;

        public NavigationController(ICategory category)
        {
            _category = category;
        }

        public ViewResult Menu(string category = null)
        {
            
            using(var dbContext=new Context()){

                List<Category> model = dbContext.Categories.ToList();


            //ViewBag.SelectedCategory = category;

            //var categories = _category.GetCategory().Select(x => x.Name);
            
            //var model = new List<string>();
            //model.AddRange(categories);
            return View(model);
            }
        }

        public ViewResult Categories(string category = null)
        {
            ViewBag.SelectedCategory = category;

            var categories = _category.GetCategory().Select(x => x.Name);

            var model = new List<string>();
            model.AddRange(categories);
            return View(model);
        }

    }
}