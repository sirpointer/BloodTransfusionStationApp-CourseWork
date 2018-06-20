using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BloodTransfusionStationApp.Models;

namespace BloodTransfusionStationApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.Cookies["Login"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }
        
        public ActionResult Tables()
        {
            if (Request.Cookies["Login"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(DataOfUser model)
        {
            var db = new UserModel();
            DataOfUser log;
            try
            {
                log = db.DataOfUser.Where(x => x.Login == model.Login && x.Password == model.Password).First();
            }
            catch (InvalidOperationException)
            {
                log = null;
            }

            if (log != null)
            {
                Response.SetCookie(new HttpCookie("Login", model.Login));

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Неверный Логин/Пароль");
                return View();
            }
        }
        
        public ActionResult SignOut()
        {
            Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Login", "Home");
        }
    }
}