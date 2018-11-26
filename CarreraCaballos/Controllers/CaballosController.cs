using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarreraCaballos.Database;

namespace CarreraCaballos.Controllers
{
    public class CaballosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Caballos
        public ActionResult Index()
        {
            return View(db.Caballos.ToList());
        }

        // GET: Caballos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caballo caballo = db.Caballos.Find(id);
            if (caballo == null)
            {
                return HttpNotFound();
            }
            return View(caballo);
        }

        // GET: Caballos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Caballos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion")] Caballo caballo)
        {
            if (ModelState.IsValid)
            {
                db.Caballos.Add(caballo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(caballo);
        }

        // GET: Caballos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caballo caballo = db.Caballos.Find(id);
            if (caballo == null)
            {
                return HttpNotFound();
            }
            return View(caballo);
        }

        // POST: Caballos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion")] Caballo caballo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caballo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(caballo);
        }

        // GET: Caballos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caballo caballo = db.Caballos.Find(id);
            if (caballo == null)
            {
                return HttpNotFound();
            }
            return View(caballo);
        }

        // POST: Caballos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Caballo caballo = db.Caballos.Find(id);
            db.Caballos.Remove(caballo);
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
