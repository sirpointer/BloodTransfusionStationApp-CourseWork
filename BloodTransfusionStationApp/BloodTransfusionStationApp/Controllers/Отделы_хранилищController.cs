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
    [Authorize]
    public class Отделы_хранилищController : Controller
    {
        private BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: Отделы_хранилищ
        public ActionResult Index(string searchString)
        {
            if (!String.IsNullOrWhiteSpace(searchString))
            {
                var elem = db.Отделы_хранилищ.Include(о => о.Хранилища_крови).AsQueryable();
                elem = elem.Where(e => e.Тип_крови.Trim().Contains(searchString.Trim()));
                return View(elem.ToList());
            }

            return View(db.Отделы_хранилищ.Include(о => о.Хранилища_крови).ToList());
        }

        // GET: Отделы_хранилищ/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Отделы_хранилищ отделы_хранилищ = db.Отделы_хранилищ.Find(id);
            if (отделы_хранилищ == null)
            {
                return HttpNotFound();
            }
            return View(отделы_хранилищ);
        }

        // GET: Отделы_хранилищ/Create
        public ActionResult Create()
        {
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови");
            return View();
        }

        // POST: Отделы_хранилищ/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Хранилище,Тип_крови,Объем")] Отделы_хранилищ отделы_хранилищ)
        {
            if (ModelState.IsValid)
            {
                db.Отделы_хранилищ.Add(отделы_хранилищ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", отделы_хранилищ.Хранилище);
            return View(отделы_хранилищ);
        }

        // GET: Отделы_хранилищ/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Отделы_хранилищ отделы_хранилищ = db.Отделы_хранилищ.Find(id);
            if (отделы_хранилищ == null)
            {
                return HttpNotFound();
            }
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", отделы_хранилищ.Хранилище);
            return View(отделы_хранилищ);
        }

        // POST: Отделы_хранилищ/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Хранилище,Тип_крови,Объем")] Отделы_хранилищ отделы_хранилищ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(отделы_хранилищ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", отделы_хранилищ.Хранилище);
            return View(отделы_хранилищ);
        }

        // GET: Отделы_хранилищ/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Отделы_хранилищ отделы_хранилищ = db.Отделы_хранилищ.Find(id);
            if (отделы_хранилищ == null)
            {
                return HttpNotFound();
            }
            return View(отделы_хранилищ);
        }

        // POST: Отделы_хранилищ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Отделы_хранилищ отделы_хранилищ = db.Отделы_хранилищ.Find(id);
            db.Отделы_хранилищ.Remove(отделы_хранилищ);
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
