using System.Web.Mvc;
using OnlineShop.Logic.Entity;
using OnlineShop.Logic.Interface;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "User")]
    public class ManageController : Controller
    {
        //
        // GET: /Manage/
        private readonly IAccess _access;
        private readonly IOrder _order;
        private readonly IAddress _address;

        public ManageController(IAccess access, IOrder order, IAddress address)
        {
            _access = access;
            _order = order;
            _address = address;
        }

        public ActionResult Index()
        {
            var model = _order.GetOrders(User.Identity.Name);
            return View(model);
        }


        public ActionResult Address()
        {
            var model = _address.GetAllAddress(User.Identity.Name);
            ViewBag.Edit = "true";
            return View(model);
        }

        public ActionResult EditAddress(int id = 0)
        {
            var model = _address.GetAdress(id) ?? new Address();
            return View(model);
        }

        [HttpPost]
        public ActionResult EditAddress(Address address)
        {
            if (ModelState.IsValid)
            {
                address.UserId = _access.GetUserId(User.Identity.Name);
                _address.SaveAdress(address);
                return RedirectToAction("Address");
            }
            return View(address);
        }
    }
}