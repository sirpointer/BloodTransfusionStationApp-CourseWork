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
    public class Хранилища_кровиController : Controller
    {
        private BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: Хранилища_крови
        public ActionResult Index()
        {
            return View(db.Хранилища_крови.ToList());
        }

        // GET: Хранилища_крови/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Хранилища_крови хранилища_крови = db.Хранилища_крови.Find(id);
            if (хранилища_крови == null)
            {
                return HttpNotFound();
            }
            return View(хранилища_крови);
        }

        // GET: Хранилища_крови/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Хранилища_крови/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_хранилища,Группа_крови,Расположение__номер_кабинета_")] Хранилища_крови хранилища_крови)
        {
            if (ModelState.IsValid)
            {
                db.Хранилища_крови.Add(хранилища_крови);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(хранилища_крови);
        }

        // GET: Хранилища_крови/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Хранилища_крови хранилища_крови = db.Хранилища_крови.Find(id);
            if (хранилища_крови == null)
            {
                return HttpNotFound();
            }
            return View(хранилища_крови);
        }

        // POST: Хранилища_крови/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_хранилища,Группа_крови,Расположение__номер_кабинета_")] Хранилища_крови хранилища_крови)
        {
            if (ModelState.IsValid)
            {
                db.Entry(хранилища_крови).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(хранилища_крови);
        }

        // GET: Хранилища_крови/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Хранилища_крови хранилища_крови = db.Хранилища_крови.Find(id);
            if (хранилища_крови == null)
            {
                return HttpNotFound();
            }
            return View(хранилища_крови);
        }

        // POST: Хранилища_крови/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Хранилища_крови хранилища_крови = db.Хранилища_крови.Find(id);
            db.Хранилища_крови.Remove(хранилища_крови);
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
