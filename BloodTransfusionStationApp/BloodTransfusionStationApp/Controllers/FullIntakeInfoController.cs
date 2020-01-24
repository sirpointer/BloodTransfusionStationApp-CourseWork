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
            return Request.Cookies["Login"] != null ? View(db.FullTakingBloodInf().ToList()) : (ActionResult)RedirectToAction("Login", "Home");
        }

        public FileResult GetResultTable()
        {
            if (Request.Cookies["Login"] == null)
            {
                return null;
            }
            else
            {
                byte[] data = PrintedForm.FullTakingBloodExls();
                var response = new FileContentResult(data, "application/octet-stream")
                {
                    FileDownloadName = "Расширенная информация о донорах.xlsx"
                };
                return response;
            }
        }
    }
}