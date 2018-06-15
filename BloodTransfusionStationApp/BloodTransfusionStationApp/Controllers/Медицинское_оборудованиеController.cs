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
    public class Медицинское_оборудованиеController : Controller
    {
        private BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: Медицинское_оборудование
        public ActionResult Index()
        {
            var медицинское_оборудование = db.Медицинское_оборудование.Include(м => м.Врачи);
            return View(медицинское_оборудование.ToList());
        }

        // GET: Медицинское_оборудование/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Медицинское_оборудование медицинское_оборудование = db.Медицинское_оборудование.Find(id);
            if (медицинское_оборудование == null)
            {
                return HttpNotFound();
            }
            return View(медицинское_оборудование);
        }

        // GET: Медицинское_оборудование/Create
        public ActionResult Create()
        {
            ViewBag.Номер_ответственного_сотрудника = new SelectList(db.Врачи, "Id", "Имя");
            return View();
        }

        // POST: Медицинское_оборудование/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_оборудования,Наименование,Дата_начала_эксплуатации,Срок_окончания_эксплуатации,Расположение__номер_кабинета_,Номер_ответственного_сотрудника")] Медицинское_оборудование медицинское_оборудование)
        {
            if (ModelState.IsValid)
            {
                db.Медицинское_оборудование.Add(медицинское_оборудование);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Номер_ответственного_сотрудника = new SelectList(db.Врачи, "Id", "Имя", медицинское_оборудование.Номер_ответственного_сотрудника);
            return View(медицинское_оборудование);
        }

        // GET: Медицинское_оборудование/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Медицинское_оборудование медицинское_оборудование = db.Медицинское_оборудование.Find(id);
            if (медицинское_оборудование == null)
            {
                return HttpNotFound();
            }
            ViewBag.Номер_ответственного_сотрудника = new SelectList(db.Врачи, "Id", "Имя", медицинское_оборудование.Номер_ответственного_сотрудника);
            return View(медицинское_оборудование);
        }

        // POST: Медицинское_оборудование/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_оборудования,Наименование,Дата_начала_эксплуатации,Срок_окончания_эксплуатации,Расположение__номер_кабинета_,Номер_ответственного_сотрудника")] Медицинское_оборудование медицинское_оборудование)
        {
            if (ModelState.IsValid)
            {
                db.Entry(медицинское_оборудование).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Номер_ответственного_сотрудника = new SelectList(db.Врачи, "Id", "Имя", медицинское_оборудование.Номер_ответственного_сотрудника);
            return View(медицинское_оборудование);
        }

        // GET: Медицинское_оборудование/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Медицинское_оборудование медицинское_оборудование = db.Медицинское_оборудование.Find(id);
            if (медицинское_оборудование == null)
            {
                return HttpNotFound();
            }
            return View(медицинское_оборудование);
        }

        // POST: Медицинское_оборудование/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Медицинское_оборудование медицинское_оборудование = db.Медицинское_оборудование.Find(id);
            db.Медицинское_оборудование.Remove(медицинское_оборудование);
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
