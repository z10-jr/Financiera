using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Financiera.Models;

namespace Financiera.Controllers
{
    public class TiposObservacionesController : Controller
    {
        private FinancieraEntities db = new FinancieraEntities();

        // GET: TiposObservaciones
        public ActionResult Index()
        {
            return View(db.TiposObservaciones.ToList());
        }

        // GET: TiposObservaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposObservaciones tiposObservaciones = db.TiposObservaciones.Find(id);
            if (tiposObservaciones == null)
            {
                return HttpNotFound();
            }
            return View(tiposObservaciones);
        }

        // GET: TiposObservaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposObservaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,habilitado")] TiposObservaciones tiposObservaciones)
        {
            if (ModelState.IsValid)
            {
                db.TiposObservaciones.Add(tiposObservaciones);
                db.SaveChanges();
                TempData["Success"] = "Guardado Exitoso";
                return RedirectToAction("Index");
            }

            return View(tiposObservaciones);
        }

        // GET: TiposObservaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposObservaciones tiposObservaciones = db.TiposObservaciones.Find(id);
            if (tiposObservaciones == null)
            {
                return HttpNotFound();
            }
            return View(tiposObservaciones);
        }

        // POST: TiposObservaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,habilitado")] TiposObservaciones tiposObservaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposObservaciones).State = EntityState.Modified;
                db.SaveChanges(); 
                TempData["Success"] = "Guardado Exitoso";
                return RedirectToAction("Index");
            }
            return View(tiposObservaciones);
        }

        // GET: TiposObservaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposObservaciones tiposObservaciones = db.TiposObservaciones.Find(id);
            if (tiposObservaciones == null)
            {
                return HttpNotFound();
            }
            return View(tiposObservaciones);
        }

        // POST: TiposObservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiposObservaciones tiposObservaciones = db.TiposObservaciones.Find(id);
            db.TiposObservaciones.Remove(tiposObservaciones);
            db.SaveChanges();
            TempData["Success"] = "Guardado Exitoso";
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
