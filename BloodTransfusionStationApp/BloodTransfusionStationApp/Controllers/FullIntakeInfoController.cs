using BloodTransfusionStationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodTransfusionStationApp.Controllers
{
    [Authorize]
    public class FullIntakeInfoController : Controller
    {
        private readonly BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: FullDonorInfo
        public ActionResult Index()
        {
            return View(db.FullTakingBloodInf().ToList());
        }
    }
}