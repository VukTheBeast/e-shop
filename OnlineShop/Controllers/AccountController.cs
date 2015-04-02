using System;
using System.Web.Mvc;
using System.Web.Security;
using OnlineShop.Infrastructure.Autorization;
using OnlineShop.Logic;
using OnlineShop.Logic.Interface;
using OnlineShop.Models;
using WebMatrix.WebData;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        private readonly IAccess _access;
        private readonly IEmail _email;

        public AccountController(IAccess access, IEmail email)
        {
            _access = access;
            _email = email;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var status = Autorization.CheckPassword(model.Email, model.Password);
                if (status == AutorizationEnum.InvalidEmail)
                {
                    ModelState.AddModelError("", "Email is invalid. ");
                    return View(model);
                }
                if (status == AutorizationEnum.InvalidPassword)
                {
                    ModelState.AddModelError("", "Password is incorrect .");
                    return View(model);
                }
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                Response.Cookies["UserName"].Value = Server.UrlEncode(_access.GetUserName(model.Email));
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(10);
                if (_access.IsControl(model.Email))
                    return RedirectToAction("ControlProducts", "Admin", new { category = "All Products" });
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "Email or password is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            if (Request.Cookies["UserName"] != null)
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            }

            return RedirectToAction("List", "Product");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = Autorization.CreateUser(model.FirstName, model.LastName, model.Email, model.Password);
                    if (status == AutorizationEnum.DuplicateEmail)
                    {
                        ModelState.AddModelError("", "This Email is already in use");
                        return View(model);
                    }
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    Response.Cookies["UserName"].Value = Server.UrlEncode(_access.GetUserName(model.Email));
                    _email.Register(model.Email, model.Password);
                    Roles.AddUserToRole(model.Email, "User");
                    return RedirectToAction("List", "Product");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Registration Error");
                }
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                string token = Autorization.GeneratePasswordResetToken(40);
                Autorization.RestoreToken(model.Email, token);
                _email.ResetPassword(model.Email, token);
                TempData["Message"] = "The key is sent to the specified email address .";
                return RedirectToAction("RestorePassword", "Account");
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            ModelState.AddModelError("", "Email is invalid .");
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult RestorePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RestorePassword(RestoreModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var status = Autorization.RestorePassword(model.Email, model.Token, model.NewPassword);
                    if (status == AutorizationEnum.InvalidEmail)
                    {
                        ModelState.AddModelError("", "Email is invalid");
                        return View(model);
                    }
                    if (status == AutorizationEnum.InvalidToken)
                    {
                        ModelState.AddModelError("", "The key is not valid");
                        return View(model);
                    }
                    _email.RestorePassword(model.Email, model.NewPassword);
                    TempData["Message"] = "Password successfully changed";

                    return RedirectToAction("Login", "Account");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", "Error recovery");
                }
            }
            return View(model);
        }


        //
        // GET: /Account/Manage

        public ActionResult Manage()
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (ModelState.IsValid)
            {
                AutorizationEnum status;
                try
                {
                    status = Autorization.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    if (status == AutorizationEnum.Success)
                    {
                        _email.ChangePassword(User.Identity.Name, model.NewPassword);
                        TempData["Message"] = "Password successfully changed";
                    }
                    if (status == AutorizationEnum.InvalidPassword)
                    {
                        ModelState.AddModelError("", "Wrong current password.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Wrong current password is invalid or a new password.");
                }
            }
            return View(model);
        }

        #region helper methods

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("List", "Product");
        }

        #endregion
    }
}