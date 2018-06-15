using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BloodTransfusionStationApp.Models;
using Microsoft.AspNet;

namespace BloodTransfusionStationApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(DataOfUser model, string returnUrl)
        {
            UsersModel db = new UsersModel();
            DataOfUser dataItem;

            try
            {
                dataItem = db.DataOfUsers.Where(x => x.Login == model.Login && x.Password == model.Password).First();
            }
            catch (InvalidOperationException)
            {
                dataItem = null;
            }

            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.Login, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неверный Логин/Пароль");
                return View();
            }
        }

        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}