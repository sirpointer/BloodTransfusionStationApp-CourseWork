using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BloodTransfusionStationApp.Models;

namespace BloodTransfusionStationApp.Controllers
{
    public class Поставки_в_больницыController : Controller
    {
        private BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: Поставки_в_больницы
        public ActionResult Index(string searchString)
        {
            if (Request.Cookies["Login"] == null)
                return RedirectToAction("Login", "Home");

            var поставки_в_больницы = db.Поставки_в_больницы.Include(п => п.Хранилища_крови);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                DateTime date = new DateTime();

                try
                {
                    date = DateTime.Parse(searchString);
                }
                catch (FormatException)
                {
                    ViewBag.Error = "Неверный формат.";
                }

                if (date != null)
                    поставки_в_больницы = поставки_в_больницы.Where(x => x.Дата_поставки > date);
            }

            return View(поставки_в_больницы.ToList());
        }

        // GET: Поставки_в_больницы/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Поставки_в_больницы поставки_в_больницы = db.Поставки_в_больницы.Find(id);
            if (поставки_в_больницы == null)
            {
                return HttpNotFound();
            }
            return View(поставки_в_больницы);
        }

        // GET: Поставки_в_больницы/Create
        public ActionResult Create()
        {
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови");
            return View();
        }

        // POST: Поставки_в_больницы/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Название,Адрес,Объем_поставки_цельной_крови,Объем_поставки_плазмы_крови,Объем_поставки_имунной_плазмы,Объем_поставки_эретроцитов_крови,Объем_поставки_тромбоцитного_концентрата,Хранилище,Дата_поставки")] Поставки_в_больницы поставки_в_больницы)
        {
            if (ModelState.IsValid)
            {
                db.Поставки_в_больницы.Add(поставки_в_больницы);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", поставки_в_больницы.Хранилище);
            return View(поставки_в_больницы);
        }

        // GET: Поставки_в_больницы/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Поставки_в_больницы поставки_в_больницы = db.Поставки_в_больницы.Find(id);
            if (поставки_в_больницы == null)
            {
                return HttpNotFound();
            }
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", поставки_в_больницы.Хранилище);
            return View(поставки_в_больницы);
        }

        // POST: Поставки_в_больницы/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Название,Адрес,Объем_поставки_цельной_крови,Объем_поставки_плазмы_крови,Объем_поставки_имунной_плазмы,Объем_поставки_эретроцитов_крови,Объем_поставки_тромбоцитного_концентрата,Хранилище,Дата_поставки")] Поставки_в_больницы поставки_в_больницы)
        {
            if (ModelState.IsValid)
            {
                db.Entry(поставки_в_больницы).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", поставки_в_больницы.Хранилище);
            return View(поставки_в_больницы);
        }

        // GET: Поставки_в_больницы/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Поставки_в_больницы поставки_в_больницы = db.Поставки_в_больницы.Find(id);
            if (поставки_в_больницы == null)
            {
                return HttpNotFound();
            }
            return View(поставки_в_больницы);
        }

        // POST: Поставки_в_больницы/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Поставки_в_больницы поставки_в_больницы = db.Поставки_в_больницы.Find(id);
            db.Поставки_в_больницы.Remove(поставки_в_больницы);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
