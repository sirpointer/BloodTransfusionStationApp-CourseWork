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
    //[Authorize]
    public class Прием_кровиController : Controller
    {
        private BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: Прием_крови
        public ActionResult Index(string date)
        {

            if (!string.IsNullOrWhiteSpace(date))
            {
                var прием_крови = db.Прием_крови.Include(п => п.Врачи).Include(п => п.Доноры).Include(п => п.Хранилища_крови);
                DateTime dateTime = new DateTime();

                try
                {
                    dateTime = DateTime.Parse(date);
                }
                catch (FormatException)
                {
                    ViewBag.Error = "Неверный формат.";
                    return View(прием_крови.ToList());
                }

                Прием_крови прием;

                try
                {
                    прием = прием_крови.Where(i => i.Дата_приема.Day == dateTime.Day
                                                            && i.Дата_приема.Month == dateTime.Month
                                                            && i.Дата_приема.Year == dateTime.Year).First();
                }
                catch (InvalidOperationException)
                {
                    ViewBag.Error = "Прием не найден.";
                    return View(прием_крови.ToList());
                }

                if (прием != null)
                {
                    return RedirectToRoute(new
                    {
                        controller = "Прием_крови",
                        action = "Details",
                        id = прием.Номер_посещения
                    });
                }
            }

            return View(db.Прием_крови.Include(п => п.Врачи).Include(п => п.Доноры).Include(п => п.Хранилища_крови).ToList());
        }

        // GET: Прием_крови/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Прием_крови прием_крови = db.Прием_крови.Find(id);
            if (прием_крови == null)
            {
                return HttpNotFound();
            }
            return View(прием_крови);
        }

        // GET: Прием_крови/Create
        public ActionResult Create()
        {
            ViewBag.Врач = new SelectList(db.Врачи, "Id", "Имя");
            ViewBag.Донор = new SelectList(db.Доноры, "Номер_донора", "Имя");
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови");
            return View();
        }

        // POST: Прием_крови/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Номер_посещения,Дата_приема,Вознаграждение,Вид_донорства,Врач,Донор,Хранилище,Объем")] Прием_крови прием_крови)
        {
            if (ModelState.IsValid)
            {
                var bloodGroup = db.Доноры.Find(прием_крови.Донор).Группа_крови;

                прием_крови.Хранилище = db.Хранилища_крови.Where(x => x.Группа_крови == bloodGroup).First().Номер_хранилища;

                db.Прием_крови.Add(прием_крови);

                db.UpdateOfDonorInformation(прием_крови.Дата_приема, прием_крови.Вид_донорства, прием_крови.Донор);

                db.AddToTheVault(прием_крови.Вид_донорства, bloodGroup, прием_крови.Объем);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Врач = new SelectList(db.Врачи, "Id", "Имя", прием_крови.Врач);
            ViewBag.Донор = new SelectList(db.Доноры, "Номер_донора", "Имя", прием_крови.Донор);
            //ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", прием_крови.Хранилище);
            return View(прием_крови);
        }

        // GET: Прием_крови/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Прием_крови прием_крови = db.Прием_крови.Find(id);
            if (прием_крови == null)
            {
                return HttpNotFound();
            }
            ViewBag.Врач = new SelectList(db.Врачи, "Id", "Имя", прием_крови.Врач);
            ViewBag.Донор = new SelectList(db.Доноры, "Номер_донора", "Имя", прием_крови.Донор);
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", прием_крови.Хранилище);
            return View(прием_крови);
        }

        // POST: Прием_крови/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Номер_посещения,Дата_приема,Вознаграждение,Вид_донорства,Врач,Донор,Хранилище,Объем")] Прием_крови прием_крови)
        {
            if (ModelState.IsValid)
            {
                db.Entry(прием_крови).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Врач = new SelectList(db.Врачи, "Id", "Имя", прием_крови.Врач);
            ViewBag.Донор = new SelectList(db.Доноры, "Номер_донора", "Имя", прием_крови.Донор);
            ViewBag.Хранилище = new SelectList(db.Хранилища_крови, "Номер_хранилища", "Группа_крови", прием_крови.Хранилище);
            return View(прием_крови);
        }

        // GET: Прием_крови/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Прием_крови прием_крови = db.Прием_крови.Find(id);
            if (прием_крови == null)
            {
                return HttpNotFound();
            }
            return View(прием_крови);
        }

        // POST: Прием_крови/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Прием_крови прием_крови = db.Прием_крови.Find(id);
            db.Прием_крови.Remove(прием_крови);
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
