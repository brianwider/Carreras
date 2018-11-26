using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarreraCaballos.Database;
using CarreraCaballos.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CarreraCaballos.Controllers
{
    [Authorize]
    public class ApuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Apuestas
        public ActionResult Index()
        {
            var usuarioId = User.Identity.GetUserId();
            var apuestas = db.Apuestas.Where(x => x.UsuarioId == usuarioId).ToList();
            return View(apuestas);
        }

        // GET: Apuestas/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apuesta apuesta = db.Apuestas.Find(id);
            if (apuesta == null)
            {
                return HttpNotFound();
            }
            return View(apuesta);
        }

        // GET: Apuestas/Create
        public ActionResult Create(long carreraId)
        {
            var carrera = db.Carreras.Find(carreraId);
            if (carrera == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new ApuestaViewModel { Carrera = carrera, CarreraId = carrera.Id };
            PopulateModel(model);
            return View(model);
        }

        // POST: Apuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApuestaViewModel model)
        {
            var apuesta = new Apuesta();
            CommonTask(model, apuesta);
            if (ModelState.IsValid)
            {
                db.Apuestas.Add(apuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateModel(model);
            return View(model);
        }

        // GET: Apuestas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Apuesta apuesta = db.Apuestas.Find(id);
            if (apuesta == null)
            {
                return HttpNotFound();
            }

            var model = new ApuestaViewModel
            {
                Carrera = apuesta.Carrera,
                CarreraId = apuesta.Carrera.Id,
                Valor = apuesta.Valor,
                CaballoId = apuesta.Caballo.Id
            };
            PopulateModel(model);

            return View(model);
        }

        // POST: Apuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApuestaViewModel model)
        {
            var apuesta = db.Apuestas.Find(model.Id);
            if (apuesta == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommonTask(model, apuesta);
            if (ModelState.IsValid)
            {
                db.Entry(apuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateModel(model);
            return View(model);
        }

        // GET: Apuestas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apuesta apuesta = db.Apuestas.Find(id);
            if (apuesta == null)
            {
                return HttpNotFound();
            }
            return View(apuesta);
        }

        // POST: Apuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Apuesta apuesta = db.Apuestas.Find(id);
            db.Apuestas.Remove(apuesta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void CommonTask(ApuestaViewModel model, Apuesta apuesta)
        {

            var caballo = db.Caballos.Find(model.CaballoId);
            if (caballo == null)
            {
                ModelState.AddModelError("", "No existe el caballo");
            }

            var carrera = db.Carreras.Find(model.CarreraId);

            if (carrera == null)
            {
                ModelState.AddModelError("", "No existe la carrera");
                return;
            }

            if (!carrera.Caballos.Contains(caballo))
            {
                ModelState.AddModelError("", "El caballo no participa de la carrera");
            }


            if (carrera.Inicio > DateTime.Now)
            {
                ModelState.AddModelError("", "La carrera ya comenzo no es posible apostar");
            }
            apuesta.CaballoId = caballo.Id;
            apuesta.CarreraId = carrera.Id;
            apuesta.Valor = model.Valor;
            apuesta.UsuarioId = User.Identity.GetUserId();
        }


        private void PopulateModel(ApuestaViewModel model)
        {
            model.Caballos = db.Carreras.Find(model.CarreraId)?.Caballos.ToList();
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
