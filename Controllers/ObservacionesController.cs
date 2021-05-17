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
    public class ObservacionesController : Controller
    {
        private FinancieraEntities db = new FinancieraEntities();

        // GET: Observaciones
        public ActionResult Index(int? id)
        {
            var observaciones = db.Observaciones.Include(o => o.TiposObservaciones).Include(o => o.usuarios).Where(c=>c.idSolicitud==id);
            ViewBag.idSolicitud = id;
            return View(observaciones.ToList());
        }

        // GET: Observaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observaciones observaciones = db.Observaciones.Find(id);
            ViewBag.idSolicitud = observaciones.idSolicitud;
            if (observaciones == null)
            {
                return HttpNotFound();
            }
            return View(observaciones);
        }

        // GET: Observaciones/Create
        public ActionResult Create(int? id)
        {
            ViewBag.idTipoObservacion = new SelectList(db.TiposObservaciones, "id", "nombre");
            ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre");
            ViewBag.idSolicitud = id;
            return View();
        }

        // POST: Observaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSolicitud,observacion,idTipoObservacion,idUsuario,fecha")] Observaciones observaciones)
        {
            if (ModelState.IsValid)
            {
                observaciones.idSolicitud = observaciones.idSolicitud;
                db.Observaciones.Add(observaciones);
                db.SaveChanges();
                TempData["Success"] = "Guardado Exitoso";
                return RedirectToAction("Index", new { id = observaciones.idSolicitud });
            }
            ViewBag.idSolicitud = observaciones.idSolicitud;
            ViewBag.idTipoObservacion = new SelectList(db.TiposObservaciones, "id", "nombre", observaciones.idTipoObservacion);
            ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre", observaciones.idUsuario);
            return View(observaciones);
        }

        // GET: Observaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observaciones observaciones = db.Observaciones.Find(id);
            if (observaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoObservacion = new SelectList(db.TiposObservaciones, "id", "nombre", observaciones.idTipoObservacion);
            ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre", observaciones.idUsuario);
            ViewBag.idSolicitud = observaciones.idSolicitud;
            return View(observaciones);
        }

        // POST: Observaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id, idSolicitud,observacion,idTipoObservacion,idUsuario,fecha")] Observaciones observaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(observaciones).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Guardado Exitoso";
                return RedirectToAction("Index", new { id = observaciones.idSolicitud });
            }
            ViewBag.idTipoObservacion = new SelectList(db.TiposObservaciones, "id", "nombre", observaciones.idTipoObservacion);
            ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre", observaciones.idUsuario);
            ViewBag.idSolicitud = observaciones.idSolicitud;
            return View(observaciones);
        }

        // GET: Observaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Observaciones observaciones = db.Observaciones.Find(id);
            ViewBag.idSolicitud = observaciones.idSolicitud;
            if (observaciones == null)
            {
                return HttpNotFound();
            }
            return View(observaciones);
        }

        // POST: Observaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Observaciones observaciones = db.Observaciones.Find(id);
            db.Observaciones.Remove(observaciones);
            db.SaveChanges();
            TempData["Success"] = "Guardado Exitoso";
            return RedirectToAction("Index", new { id = observaciones.idSolicitud });
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
