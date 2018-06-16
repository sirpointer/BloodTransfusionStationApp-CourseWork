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
    public class ВрачиController : Controller
    {
        private BloodTransfusionStationDBEntities db = new BloodTransfusionStationDBEntities();

        // GET: Врачи
        public ActionResult Index(string doctorLastName)
        {
            if (!string.IsNullOrWhiteSpace(doctorLastName))
            {
                Врачи doc;

                try
                {
                    doc = db.Врачи.Where(d => d.Фамилия.Equals(doctorLastName, StringComparison.CurrentCultureIgnoreCase)).First();
                }
                catch(InvalidOperationException)
                {
                    doc = null;
                }

                if (doc != null)
                {
                    return RedirectToRoute(new
                    {
                        controller = "Врачи",
                        action = "Details",
                        id = doc.Id
                    });
                }
                else
                    ViewBag.Error = "Информация об этом враче отсутствует.";
            }

            return View(db.Врачи.ToList());
        }

        // GET: Врачи/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Врачи врачи = db.Врачи.Find(id);
            if (врачи == null)
            {
                return HttpNotFound();
            }
            return View(врачи);
        }

        // GET: Врачи/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Врачи/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Имя,Фамилия,Отчество,Должность,Ставка,Стаж")] Врачи врачи)
        {
            if (ModelState.IsValid)
            {
                db.Врачи.Add(врачи);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(врачи);
        }

        // GET: Врачи/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Врачи врачи = db.Врачи.Find(id);
            if (врачи == null)
            {
                return HttpNotFound();
            }
            return View(врачи);
        }

        // POST: Врачи/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Имя,Фамилия,Отчество,Должность,Ставка,Стаж")] Врачи врачи)
        {
            if (ModelState.IsValid)
            {
                db.Entry(врачи).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(врачи);
        }

        // GET: Врачи/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Врачи врачи = db.Врачи.Find(id);
            if (врачи == null)
            {
                return HttpNotFound();
            }
            return View(врачи);
        }

        // POST: Врачи/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Врачи врачи = db.Врачи.Find(id);
            db.Врачи.Remove(врачи);
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
