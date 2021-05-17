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
    public class ProvinciasController : Controller
    {
        private FinancieraEntities db = new FinancieraEntities();

        // GET: Provincias
        public ActionResult Index()
        {
            var provincias = db.Provincias.Include(p => p.Paises);
            return View(provincias.ToList());
        }

        // GET: Provincias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = db.Provincias.Find(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            return View(provincias);
        }

        // GET: Provincias/Create
        public ActionResult Create()
        {
            ViewBag.idPais = new SelectList(db.Paises, "id", "nombre");
            return View();
        }

        // POST: Provincias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,Habilitado,idPais")] Provincias provincias)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Provincias.Add(provincias);
                    db.SaveChanges();
                    TempData["Success"] = "Guardado Exitoso";
                    return RedirectToAction("Index");
                }

                ViewBag.idPais = new SelectList(db.Paises, "id", "nombre", provincias.idPais);
                return View(provincias);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
            }

        }

        // GET: Provincias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = db.Provincias.Find(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPais = new SelectList(db.Paises, "id", "nombre", provincias.idPais);
            return View(provincias);
        }

        // POST: Provincias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,Habilitado,idPais")] Provincias provincias)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(provincias).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success"] = "Guardado Exitoso";
                    return RedirectToAction("Index");
                }
                ViewBag.idPais = new SelectList(db.Paises, "id", "nombre", provincias.idPais);
                return View(provincias);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
            }


        }

        // GET: Provincias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincias provincias = db.Provincias.Find(id);
            if (provincias == null)
            {
                return HttpNotFound();
            }
            return View(provincias);
        }

        // POST: Provincias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Provincias provincias = db.Provincias.Find(id);
                db.Provincias.Remove(provincias);
                db.SaveChanges();
                TempData["Success"] = "Guardado Exitoso";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
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
