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
    public class EntidadesController : Controller
    {
        private FinancieraEntities db = new FinancieraEntities();

        // GET: Entidades
        public ActionResult Index()
        {
            var entidades = db.Entidades.Include(e => e.Categorias).Include(e => e.Provincias);
            return View(entidades.ToList());
        }

        // GET: Entidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidades entidades = db.Entidades.Find(id);
            if (entidades == null)
            {
                return HttpNotFound();
            }
            return View(entidades);
        }

        // GET: Entidades/Create
        public ActionResult Create()
        {
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "nombre");
            ViewBag.idProvincia = new SelectList(db.Provincias, "id", "nombre");
            return View();
        }

        // POST: Entidades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,Habilitado,idCategoria,idProvincia")] Entidades entidades)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entidades.Add(entidades);
                    db.SaveChanges();
                    TempData["Success"] = "Guardado Exitoso";
                    return RedirectToAction("Index");
                }

                ViewBag.idCategoria = new SelectList(db.Categorias, "id", "nombre", entidades.idCategoria);
                ViewBag.idProvincia = new SelectList(db.Provincias, "id", "nombre", entidades.idProvincia);
                return View(entidades);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Entidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidades entidades = db.Entidades.Find(id);
            if (entidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCategoria = new SelectList(db.Categorias, "id", "nombre", entidades.idCategoria);
            ViewBag.idProvincia = new SelectList(db.Provincias, "id", "nombre", entidades.idProvincia);
            return View(entidades);
        }

        // POST: Entidades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,Habilitado,idCategoria,idProvincia")] Entidades entidades)
        {
            

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(entidades).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Success"] = "Guardado Exitoso";
                    return RedirectToAction("Index");
                }
                ViewBag.idCategoria = new SelectList(db.Categorias, "id", "nombre", entidades.idCategoria);
                ViewBag.idProvincia = new SelectList(db.Provincias, "id", "nombre", entidades.idProvincia);
                return View(entidades);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Entidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entidades entidades = db.Entidades.Find(id);
            if (entidades == null)
            {
                return HttpNotFound();
            }
            return View(entidades);
        }

        // POST: Entidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Entidades entidades = db.Entidades.Find(id);
                db.Entidades.Remove(entidades);
                db.SaveChanges();
                TempData["Success"] = "Guardado Exitoso";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
