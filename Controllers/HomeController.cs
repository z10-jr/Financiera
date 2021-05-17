using Financiera.Models;
using PersimosMVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace PersimosMVC.Controllers
{
    public class HomeController : Controller
    {
        private FinancieraEntities db = new FinancieraEntities();
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult Index()
        {
            var solicitudes = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).OrderByDescending(x=>x.FechaCreacion);
            return View(solicitudes.ToList());
        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}