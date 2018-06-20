using BloodTransfusionStationApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace BloodTransfusionStationApp.Controllers
{
    public class FullIntakeInfoController : Controller
    {
        private readonly BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: FullDonorInfo
        public ActionResult Index()
        {
            if (Request.Cookies["Login"] != null)
                return View(db.FullTakingBloodInf().ToList());
            else
                return RedirectToAction("Login", "Home");
        }
    }
}