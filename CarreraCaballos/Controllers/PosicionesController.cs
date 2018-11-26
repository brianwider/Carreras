using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarreraCaballos.Database;

namespace CarreraCaballos.Controllers
{
    public class PosicionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posiciones
        public ActionResult Index(long id)
        {
            var posiciones = db.Posiciones.Where(x => x.Carrera.Id == id).OrderBy(x=>x.Numero).ToList();
            return View(posiciones);
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