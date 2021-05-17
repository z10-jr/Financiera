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
    public class SolicitudBancosController : Controller
    {
        private FinancieraEntities db = new FinancieraEntities();

        // GET: SolicitudBancos
        public ActionResult Index(int? id)
        {
            var solicitudBancos = db.SolicitudBancos.Include(s => s.Entidades).Include(s => s.Solicitudes).Where(c => c.idSolicitud == id); ;
            ViewBag.SolicitudId = id;
            return View(solicitudBancos.ToList());
        }

        // GET: SolicitudBancos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudBancos solicitudBancos = db.SolicitudBancos.Find(id);
            ViewBag.idSolicitud = solicitudBancos.idSolicitud;
            if (solicitudBancos == null)
            {
                return HttpNotFound();
            }
            return View(solicitudBancos);
        }

        // GET: SolicitudBancos/Create
        public ActionResult Create(int id)
        {
            ViewBag.idEntidad = new SelectList(db.Entidades, "id", "nombre");
            ViewBag.idSolicitud = id;
            return View();
        }

        // POST: SolicitudBancos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSolicitud,idEntidad,Fecha,Plazo,Monto,TNA,Observaciones")] SolicitudBancos solicitudBancos)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudBancos.Add(solicitudBancos);
                db.SaveChanges();
                TempData["Success"] = "Guardado Exitoso";
                return RedirectToAction("Index",new { id = solicitudBancos.idSolicitud });
            }

            ViewBag.idEntidad = new SelectList(db.Entidades, "id", "nombre", solicitudBancos.idEntidad);
            ViewBag.idSolicitud = solicitudBancos.idSolicitud;
            return View(solicitudBancos);
        }

        // GET: SolicitudBancos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudBancos solicitudBancos = db.SolicitudBancos.Find(id);
            if (solicitudBancos == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEntidad = new SelectList(db.Entidades, "id", "nombre", solicitudBancos.idEntidad);
            ViewBag.idSolicitud = solicitudBancos.idSolicitud;
            return View(solicitudBancos);
        }

        // POST: SolicitudBancos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idSolicitud,idEntidad,Fecha,Plazo,Monto,TNA,Observaciones")] SolicitudBancos solicitudBancos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudBancos).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Guardado Exitoso";
                return RedirectToAction("Index", new { id = solicitudBancos.idSolicitud });
            }
            ViewBag.idEntidad = new SelectList(db.Entidades, "id", "nombre", solicitudBancos.idEntidad);
            ViewBag.idSolicitud = solicitudBancos.idSolicitud;
            return View(solicitudBancos);
        }

        // GET: SolicitudBancos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudBancos solicitudBancos = db.SolicitudBancos.Find(id);
            ViewBag.idSolicitud = solicitudBancos.idSolicitud;

            if (solicitudBancos == null)
            {
                return HttpNotFound();
            }
            return View(solicitudBancos);
        }

        // POST: SolicitudBancos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudBancos solicitudBancos = db.SolicitudBancos.Find(id);
            db.SolicitudBancos.Remove(solicitudBancos);
            db.SaveChanges();
            TempData["Success"] = "Guardado Exitoso";
            return RedirectToAction("Index", new { id = solicitudBancos.idSolicitud });
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
