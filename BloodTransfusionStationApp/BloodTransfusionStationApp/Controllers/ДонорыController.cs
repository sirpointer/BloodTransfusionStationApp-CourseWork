using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BloodTransfusionStationApp.Models;

namespace BloodTransfusionStationApp.Controllers
{
    public class ДонорыController : Controller
    {
        private BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: Доноры
        public ActionResult Index(string searchString)
        {
            if (Request.Cookies["Login"] == null)
                return RedirectToAction("Login", "Home");

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                var elem = db.Доноры.AsQueryable();
                elem = db.Доноры.Where(d => d.Группа_крови.Trim().Equals(searchString.Trim(), StringComparison.OrdinalIgnoreCase));
                return View(elem.ToList());
            }

            return View(db.Доноры.ToList());
        }

        // GET: Доноры/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Доноры доноры = db.Доноры.Find(id);
            if (доноры == null)
            {
                return HttpNotFound();
            }
            return View(доноры);
        }

        // GET: Доноры/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Доноры/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_донора,Имя,Фамилия,Отчество,Дата_рождения,Пол,Группа_крови,Телефон,Адрес,Паспортные_данные")] Доноры доноры)
        {
            if (ModelState.IsValid)
            {
                db.Доноры.Add(доноры);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(доноры);
        }

        // GET: Доноры/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Доноры доноры = db.Доноры.Find(id);
            if (доноры == null)
            {
                return HttpNotFound();
            }
            return View(доноры);
        }

        // POST: Доноры/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_донора,Имя,Фамилия,Отчество,Дата_рождения,Пол,Группа_крови,Телефон,Адрес,Паспортные_данные")] Доноры доноры)
        {
            if (ModelState.IsValid)
            {
                db.Entry(доноры).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(доноры);
        }

        // GET: Доноры/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Доноры доноры = db.Доноры.Find(id);
            if (доноры == null)
            {
                return HttpNotFound();
            }
            return View(доноры);
        }

        // POST: Доноры/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Доноры доноры = db.Доноры.Find(id);
            db.Доноры.Remove(доноры);
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
