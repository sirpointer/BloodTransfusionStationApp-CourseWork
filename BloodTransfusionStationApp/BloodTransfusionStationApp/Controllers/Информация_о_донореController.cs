using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodTransfusionStationApp.Models;

namespace BloodTransfusionStationApp.Controllers
{
    public class Информация_о_донореController : Controller
    {
        private BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: Информация_о_доноре
        public ActionResult Index()
        {
            var информация_о_доноре = db.Информация_о_доноре.Include(и => и.Доноры);
            return View(информация_о_доноре.ToList());
        }

        // GET: Информация_о_доноре/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Информация_о_доноре информация_о_доноре = db.Информация_о_доноре.Find(id);
            if (информация_о_доноре == null)
            {
                return HttpNotFound();
            }
            return View(информация_о_доноре);
        }

        // GET: Информация_о_доноре/Create
        public ActionResult Create()
        {
            ViewBag.Номер_донора = new SelectList(db.Доноры, "Номер_донора", "Имя");
            return View();
        }

        // POST: Информация_о_доноре/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_донора,Количество_сдач_цельной_крови_за_последний_год,Последняя_дата_сдачи_имунной_плазмы,Последий_тромбоцитаферез,Последний_плазмаферез,Последний_эритроцитаферез,Пройдено_медицинское_обследование,Окончание_действия_мед__обследования")] Информация_о_доноре информация_о_доноре)
        {
            if (ModelState.IsValid)
            {
                db.Информация_о_доноре.Add(информация_о_доноре);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Номер_донора = new SelectList(db.Доноры, "Номер_донора", "Имя", информация_о_доноре.Номер_донора);
            return View(информация_о_доноре);
        }

        // GET: Информация_о_доноре/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Информация_о_доноре информация_о_доноре = db.Информация_о_доноре.Find(id);
            if (информация_о_доноре == null)
            {
                return HttpNotFound();
            }
            ViewBag.Номер_донора = new SelectList(db.Доноры, "Номер_донора", "Имя", информация_о_доноре.Номер_донора);
            return View(информация_о_доноре);
        }

        // POST: Информация_о_доноре/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_донора,Количество_сдач_цельной_крови_за_последний_год,Последняя_дата_сдачи_имунной_плазмы,Последий_тромбоцитаферез,Последний_плазмаферез,Последний_эритроцитаферез,Пройдено_медицинское_обследование,Окончание_действия_мед__обследования")] Информация_о_доноре информация_о_доноре)
        {
            if (ModelState.IsValid)
            {
                db.Entry(информация_о_доноре).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Номер_донора = new SelectList(db.Доноры, "Номер_донора", "Имя", информация_о_доноре.Номер_донора);
            return View(информация_о_доноре);
        }

        // GET: Информация_о_доноре/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Информация_о_доноре информация_о_доноре = db.Информация_о_доноре.Find(id);
            if (информация_о_доноре == null)
            {
                return HttpNotFound();
            }
            return View(информация_о_доноре);
        }

        // POST: Информация_о_доноре/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Информация_о_доноре информация_о_доноре = db.Информация_о_доноре.Find(id);
            db.Информация_о_доноре.Remove(информация_о_доноре);
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
