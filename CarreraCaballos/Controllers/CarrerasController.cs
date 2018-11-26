using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarreraCaballos.Database;
using CarreraCaballos.Helpers;
using CarreraCaballos.Models;

namespace CarreraCaballos.Controllers
{
    public class CarrerasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carreras
        public ActionResult Index()
        {
            return View(db.Carreras.ToList());
        }

        // GET: Carreras/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var carrera = db.Carreras.Find(id);
            if (carrera == null)
            {
                return HttpNotFound();
            }
            return View(carrera);
        }

        // GET: Carreras/Create
        public ActionResult Create()
        {
            var model = new CarreraViewModel
            {
                CaballosSeleccionadosPresentacion = new List<Caballo>()
            };
            PopulateModel(model);
            return View(model);
        }

        // POST: Carreras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarreraViewModel model)
        {
            var carrera = new Carrera { Inicio = model.Inicio, Caballos = new List<Caballo>() };

            CommonTask(model, carrera);

            if (ModelState.IsValid)
            {

                db.Carreras.Add(carrera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            PopulateModel(model);
            return View(model);
        }

        // GET: Carreras/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var carrera = db.Carreras.Find(id);

            if (carrera == null)
            {
                return HttpNotFound();
            }

            var model = new CarreraViewModel { Inicio = carrera.Inicio, CaballosSeleccionadosPresentacion = carrera.Caballos.ToList() };

            PopulateModel(model);

            return View(model);
        }

        // POST: Carreras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarreraViewModel model)
        {
            var carrera = db.Carreras.Find(model.Id);
            if (carrera == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CommonTask(model, carrera);
            if (ModelState.IsValid)
            {
                db.Entry(carrera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateModel(model);

            return View(model);
        }

        // GET: Carreras/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var carrera = db.Carreras.Find(id);

            if (carrera == null)
            {
                return HttpNotFound();
            }
            return View(carrera);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var carrera = db.Carreras.Find(id);

            if (carrera == null)
            {
                return HttpNotFound();
            }

            db.Carreras.Remove(carrera);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Correr")]
        [ValidateAntiForgeryToken]
        public ActionResult Correr(long id)
        {
            var carrera = db.Carreras.Find(id);

            if (carrera == null)
            {
                return HttpNotFound();
            }

            if (carrera.Posiciones.Count != 0)
            {
                return RedirectToAction("Index");
            }

            var idCaballos = carrera.Caballos.Select(x => x.Id);
            var result = idCaballos.Shuffle(new Random());
            short orden = 1;
            foreach (var caballo in result)
            {
                var posicion = new Posicion
                {
                    CaballoId = caballo,
                    CarreraId = id,
                    Numero = orden
                };
                db.Posiciones.Add(posicion);

                orden++;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        private void PopulateModel(CarreraViewModel model)
        {
            model.Caballos = db.Caballos.ToList();
        }

        private void CommonTask(CarreraViewModel model, Carrera carrera)
        {
            carrera.Inicio = model.Inicio;
            carrera.Caballos = new List<Caballo>();
            var allExists = true;
            model.CaballosSeleccionadosPresentacion = new List<Caballo>();

            foreach (var caballoId in model.CaballosSeleccionados)
            {
                var caballo = db.Caballos.Find(caballoId);
                if (caballo != null)
                {
                    carrera.Caballos.Add(caballo);
                    model.CaballosSeleccionadosPresentacion.Add(caballo);
                }
                else
                {
                    allExists = false;
                }

            }

            if (!allExists)
            {
                ModelState.AddModelError("", "Selecciono un caballo inexistente");
            }
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
