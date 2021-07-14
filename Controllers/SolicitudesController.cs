using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Financiera.Models;
using Newtonsoft.Json;

namespace Financiera.Controllers
{
    public class SolicitudesController : Controller
    {
        private FinancieraEntities db = new FinancieraEntities();

        // GET: Solicitudes
        public ActionResult Index()
        {
            var solicitudes = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Clientes1).Include(s => s.Entidades).Include(s => s.Estados).Include(s => s.TiposRechazos).Include(s => s.usuarios);
            return View(solicitudes.ToList());
        }

        // GET: Solicitudes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            Clientes cliente = db.Clientes.Find(solicitudes.idCliente);
            Clientes conyuge = db.Clientes.Find(solicitudes.idConyuge);
            Vendedores vendedor = db.Vendedores.Find(solicitudes.idVendedor);
            ViewBag.Cliente = cliente != null ? cliente.dni + " - " + cliente.apellido + ", " + cliente.nombre : string.Empty;
            ViewBag.Conyuge = conyuge != null ? conyuge.dni + " - " + conyuge.apellido + ", " + conyuge.nombre : string.Empty;
            ViewBag.Vendedor = vendedor != null ? vendedor.DNI + " - " + vendedor.Apellido + ", " + vendedor.Nombre : string.Empty;
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        // GET: Solicitudes/Create
        public ActionResult Create()
        {
            //ViewBag.idCliente = new SelectList(db.Clientes, "id", "nombre");
            ViewBag.idConyuge = new SelectList(db.Clientes, "id", "nombre");
            ViewBag.idEntidad = new SelectList(db.Entidades.Where(p => p.Habilitado == true), "id", "nombre");
            ViewBag.idEstado = new SelectList(db.Estados, "id", "nombre");
            ViewBag.idRechazo = new SelectList(db.TiposRechazos, "id", "nombre");
            ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre");
            ViewBag.idCliente = db.Clientes.Select(p => new SelectListItem
            {
                Text = p.apellido + ", " + p.nombre,
                Value = p.id.ToString()
            });
            ViewBag.idEntidades = db.Entidades.Where(p => p.Habilitado == true);

            return View();
        }

        // POST: Solicitudes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idCliente,idConyuge,idUsuario,idVendedor,idEntidad,idRechazo,FechaCreacion,idEstado,monto,codigoEntidad,entregaGestor,fechaEntregaGestor,prendaInscripta,fechaInscripcionPrenda,fechaUltimoPago,fechaFirma,fechaNuevaLlamada,Cuotas,Vehiculo,Observaciones")] Solicitudes solicitudes, IList<string> entidadList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Clientes cliente = db.Clientes.Find(solicitudes.idCliente);
                    if (cliente.telefono != string.Empty && cliente.apellido != string.Empty && cliente.nombre != string.Empty && solicitudes.monto != null)
                    {
                        var appSettings = ConfigurationManager.AppSettings;
                        solicitudes.idEstado = Convert.ToInt32(appSettings["EnAnalisis"]);
                    }
                    else
                    {
                        var appSettings = ConfigurationManager.AppSettings;
                        solicitudes.idEstado = Convert.ToInt32(appSettings["Inicial"]);
                    }

                    solicitudes.FechaCreacion = DateTime.Now;
                    solicitudes.idUsuario = ((usuarios)Session["User"]).id;
                    db.Solicitudes.Add(solicitudes);

                    if(entidadList!= null)
                    {
                        foreach (string entidadId in entidadList)
                        {
                            SolicitudBancos solicitudBancoToSave = new SolicitudBancos();
                            solicitudBancoToSave.idEntidad = Convert.ToInt32(entidadId);
                            solicitudBancoToSave.idSolicitud = solicitudes.id;
                            db.SolicitudBancos.Add(solicitudBancoToSave);
                        }
                    }
                    
                    db.SaveChanges();
                    TempData["Success"] = "Guardado Exitoso";
                    return RedirectToAction("Index");
                }


                ViewBag.idConyuge = new SelectList(db.Clientes, "id", "nombre");
                ViewBag.idEntidad = new SelectList(db.Entidades.Where(p => p.Habilitado == true), "id", "nombre");
                ViewBag.idEstado = new SelectList(db.Estados, "id", "nombre");
                ViewBag.idRechazo = new SelectList(db.TiposRechazos, "id", "nombre");
                ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre");
                ViewBag.idCliente = db.Clientes.Select(p => new SelectListItem
                {
                    Text = p.apellido + ", " + p.nombre,
                    Value = p.id.ToString()
                });
                ViewBag.idEntidades = db.Entidades.Where(p => p.Habilitado == true);
                return View(solicitudes);
            }
            catch (Exception ex)
            {
                ViewBag.idCliente = db.Clientes.Select(p => new SelectListItem
                {
                    Text = p.apellido + ", " + p.nombre,
                    Value = p.id.ToString()
                });
                ViewBag.idCliente = new SelectList(db.Clientes, "id", "nombre", solicitudes.idCliente);
                ViewBag.idConyuge = new SelectList(db.Clientes, "id", "nombre", solicitudes.idConyuge);
                ViewBag.idEntidad = new SelectList(db.Entidades, "id", "nombre", solicitudes.idEntidad);
                ViewBag.idEstado = new SelectList(db.Estados, "id", "nombre", solicitudes.idEstado);
                ViewBag.idRechazo = new SelectList(db.TiposRechazos, "id", "nombre", solicitudes.idRechazo);
                ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre", solicitudes.idUsuario);
                TempData["Error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Solicitudes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            Clientes cliente = db.Clientes.Find(solicitudes.idCliente);
            Clientes conyuge = db.Clientes.Find(solicitudes.idConyuge);
            Vendedores vendedor = db.Vendedores.Find(solicitudes.idVendedor);
            ViewBag.Cliente = cliente != null ? cliente.dni + " - " + cliente.apellido +", "+ cliente.nombre : string.Empty;
            ViewBag.Conyuge = conyuge != null ? conyuge.dni + " - " + conyuge.apellido + ", " + conyuge.nombre : string.Empty;
            ViewBag.Vendedor = vendedor != null ? vendedor.DNI + " - " + vendedor.Apellido + ", " + vendedor.Nombre : string.Empty;
            ViewBag.idConyuge = new SelectList(db.Clientes, "id", "nombre", solicitudes.idConyuge);
            ViewBag.idEntidades = db.SolicitudBancos.Where(p => p.idSolicitud == id)
                                .Select(x => new SelectListItem
                                {
                                    Value = x.Entidades.id.ToString(),
                                    Text = x.Entidades.nombre,
                                });


            ViewBag.idEntidad = solicitudes.idEntidad.HasValue ? solicitudes.idEntidad.Value : 0;
            ViewBag.idEstado = new SelectList(db.Estados, "id", "nombre", solicitudes.idEstado); //corregir el where para que me traiga todos los post6eriores estados
            ViewBag.idRechazo = new SelectList(db.TiposRechazos, "id", "nombre", solicitudes.idRechazo);
            ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre", solicitudes.idUsuario);
            ViewBag.idCliente = db.Clientes.Select(p => new SelectListItem
            {
                Text = p.apellido + ", " + p.nombre,
                Value = p.id.ToString()
            });

            return View(solicitudes);
        }

        // POST: Solicitudes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idCliente,idConyuge,idUsuario,idVendedor,idEntidad,idRechazo,FechaCreacion,idEstado,monto,codigoEntidad,entregaGestor,fechaEntregaGestor,prendaInscripta,fechaInscripcionPrenda, fechaDevolucionPrenda,fechaUltimoPago,fechaFirma,fechaNuevaLlamada,Cuotas,Vehiculo,Observaciones")] Solicitudes solicitudes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var appSettings = ConfigurationManager.AppSettings;
                    bool flag = true;
                    string message = string.Empty;

                    //cambios manuales
                    if (solicitudes.idEstado == Convert.ToInt32(appSettings["CompletoParaCargar"]) && (solicitudes.idEntidad == null || solicitudes.Cuotas == null ))
                    {
                        flag = false;
                        message = "Debe detallar la Entidad Financiera y el Plazo";
                    }
                    else if (solicitudes.idEstado == Convert.ToInt32(appSettings["ParaLLamarAFirmar"]) && solicitudes.fechaFirma == null)
                    {
                        flag = false;
                        message = "Debe cargar una Fecha de Firma";
                    }
                    else if (solicitudes.idEstado == Convert.ToInt32(appSettings["Desaprobado"]) && solicitudes.idRechazo == null)
                    {
                        flag = false;
                        message = "Debe cargar Motivo de Rechazo";
                    }
                    else if (solicitudes.idEstado == Convert.ToInt32(appSettings["DesistioElCliente"]) && (solicitudes.idRechazo == null || solicitudes.fechaNuevaLlamada == null) )
                    {
                        flag = false;
                        message = "Debe cargar motivo desistimiento y nueva fecha de llamada";
                    }

                    //cambios automaticos
                    Clientes cliente = db.Clientes.Find(solicitudes.idCliente);
                    if (solicitudes.idEstado == Convert.ToInt32(appSettings["Inicial"]) && cliente.telefono != string.Empty && cliente.apellido != string.Empty && cliente.nombre != string.Empty && solicitudes.monto != null && solicitudes.idVendedor != null)
                    {
                        solicitudes.idEstado = Convert.ToInt32(appSettings["EnAnalisis"]);
                    }
                    else if (solicitudes.idEstado == Convert.ToInt32(appSettings["CompletoParaCargar"]) && solicitudes.codigoEntidad != string.Empty)
                    {
                        solicitudes.idEstado = Convert.ToInt32(appSettings["AEsperaDeResultado"]);
                    }
                    else if (solicitudes.idEstado == Convert.ToInt32(appSettings["Firmado"]) && solicitudes.entregaGestor == true && solicitudes.fechaEntregaGestor != null && solicitudes.fechaDevolucionPrenda != null)
                    {
                        solicitudes.idEstado = Convert.ToInt32(appSettings["PrendaAGestor"]);
                    }
                    else if (solicitudes.idEstado == Convert.ToInt32(appSettings["PrendaAGestor"]) && solicitudes.prendaInscripta == true && solicitudes.fechaInscripcionPrenda != null && solicitudes.fechaUltimoPago != null)
                    {
                        solicitudes.idEstado = Convert.ToInt32(appSettings["PrendaInscripta"]);
                    }

                    if (flag)
                    {
                        Observaciones observacion = new Observaciones();
                        observacion.idUsuario = ((usuarios)Session["User"]).id;
                        observacion.fecha = DateTime.Now;
                        observacion.idTipoObservacion = db.TiposObservaciones.Find(Convert.ToInt32(appSettings["TiposObservacionMovimiento"])).id;
                        observacion.observacion = solicitudes.idEstado != null ? db.Estados.Find(solicitudes.idEstado).nombre : string.Empty;
                        observacion.Solicitudes = solicitudes;
                        db.Observaciones.Add(observacion);

                        db.Entry(solicitudes).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Success"] = "Guardado Exitoso";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = message;
                        return RedirectToAction("Index");
                    }
                }
                
                ViewBag.idConyuge = new SelectList(db.Clientes, "id", "nombre", solicitudes.idConyuge);
                ViewBag.idEntidad = new SelectList(db.Entidades, "id", "nombre", solicitudes.idEntidad);
                ViewBag.idEstado = new SelectList(db.Estados, "id", "nombre", solicitudes.idEstado);
                ViewBag.idRechazo = new SelectList(db.TiposRechazos, "id", "nombre", solicitudes.idRechazo);
                ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre", solicitudes.idUsuario);
                ViewBag.idCliente = db.Clientes.Select(p => new SelectListItem
                {
                    Text = p.apellido + ", " + p.nombre,
                    Value = p.id.ToString()
                });
                return View(solicitudes);
            }
            catch (Exception ex)
            {
                
                ViewBag.idConyuge = new SelectList(db.Clientes, "id", "nombre", solicitudes.idConyuge);
                ViewBag.idEntidad = new SelectList(db.Entidades, "id", "nombre", solicitudes.idEntidad);
                ViewBag.idEstado = new SelectList(db.Estados, "id", "nombre", solicitudes.idEstado);
                ViewBag.idRechazo = new SelectList(db.TiposRechazos, "id", "nombre", solicitudes.idRechazo);
                ViewBag.idUsuario = new SelectList(db.usuarios, "id", "nombre", solicitudes.idUsuario);
                ViewBag.idCliente = db.Clientes.Select(p => new SelectListItem
                {
                    Text = p.apellido + ", " + p.nombre,
                    Value = p.id.ToString()
                });
                TempData["Error"] = ex.InnerException.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Solicitudes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            Clientes cliente = db.Clientes.Find(solicitudes.idCliente);
            Clientes conyuge = db.Clientes.Find(solicitudes.idConyuge);
            Vendedores vendedor = db.Vendedores.Find(solicitudes.idVendedor);
            ViewBag.Cliente = cliente != null ? cliente.apellido + ", " + cliente.nombre : string.Empty;
            ViewBag.Conyuge = conyuge != null ? conyuge.apellido + ", " + conyuge.nombre : string.Empty;
            ViewBag.Vendedor = vendedor != null ? vendedor.Apellido + ", " + vendedor.Nombre : string.Empty;
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(solicitudes);
        }

        // POST: Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Solicitudes solicitudes = db.Solicitudes.Find(id);
                db.Solicitudes.Remove(solicitudes);
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

        public ActionResult Obervations(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitudes solicitudes = db.Solicitudes.Find(id);
            if (solicitudes == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = db.Clientes.Select(p => new SelectListItem
            {
                Text = p.apellido + ", " + p.nombre,
                Value = p.id.ToString()
            });
            ViewBag.idEstado = db.Clientes.Where(p => p.id > solicitudes.id)
                                    .Select(p => new SelectListItem
                                    {
                                        Text = p.nombre,
                                        Value = p.id.ToString()
                                    });


            return View(solicitudes);
        }

        public ActionResult DetailsByState(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estado = db.Estados.Find(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            else
            {
                var solicitudes = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == estado.id);
                return View(solicitudes.ToList());
            }
        }

        public JsonResult CountByState()    // el método debe ser de static
        {
            IList<string> resultados = new List<string>();
            var appSettings = ConfigurationManager.AppSettings;
            try
            {
                int stateInicial = Convert.ToInt32(appSettings["Inicial"]);
                var Inicial = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateInicial);
                resultados.Add(Inicial.ToList().Count().ToString());

                int stateEnAnalisis = Convert.ToInt32(appSettings["EnAnalisis"]);
                var EnAnalisis = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateEnAnalisis);
                resultados.Add(EnAnalisis.ToList().Count().ToString());

                int stateParaLlamar = Convert.ToInt32(appSettings["ParaLlamar"]);
                var ParaLlamar = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateParaLlamar);
                resultados.Add(ParaLlamar.ToList().Count().ToString());

                int stateDesechado = Convert.ToInt32(appSettings["Desechado"]);
                var Desechado = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateDesechado);
                resultados.Add(Desechado.ToList().Count().ToString());

                int stateCompletoParaCargar = Convert.ToInt32(appSettings["CompletoParaCargar"]);
                var CompletoParaCargar = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateCompletoParaCargar);
                resultados.Add(CompletoParaCargar.ToList().Count().ToString());

                int stateDesistioElCliente = Convert.ToInt32(appSettings["DesistioElCliente"]);
                var DesistioElCliente = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateDesistioElCliente);
                resultados.Add(DesistioElCliente.ToList().Count().ToString());

                int stateAEsperaDeResultado = Convert.ToInt32(appSettings["AEsperaDeResultado"]);
                var AEsperaDeResultado = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateAEsperaDeResultado);
                resultados.Add(AEsperaDeResultado.ToList().Count().ToString());

                int stateParaLLamarAFirmar = Convert.ToInt32(appSettings["ParaLLamarAFirmar"]);
                var ParaLLamarAFirmar = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateParaLLamarAFirmar);
                resultados.Add(ParaLLamarAFirmar.ToList().Count().ToString());

                int stateDesaprobado = Convert.ToInt32(appSettings["Desaprobado"]);
                var Desaprobado = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateDesaprobado);
                resultados.Add(Desaprobado.ToList().Count().ToString());

                int stateFirmado = Convert.ToInt32(appSettings["Firmado"]);
                var Firmado = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == stateFirmado);
                resultados.Add(Firmado.ToList().Count().ToString());

                int statePrendaAGestor = Convert.ToInt32(appSettings["PrendaAGestor"]);
                var PrendaAGestor = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == statePrendaAGestor);
                resultados.Add(PrendaAGestor.ToList().Count().ToString());

                int statePrendaInscripta = Convert.ToInt32(appSettings["PrendaInscripta"]);
                var PrendaInscripta = db.Solicitudes.Include(s => s.Clientes).Include(s => s.Estados).Where(p => p.idEstado == statePrendaInscripta);
                resultados.Add(PrendaInscripta.ToList().Count().ToString());



            }
            catch (Exception ex)
            {

            }

            return Json(resultados);
        }

        public JsonResult Alerts()
        {
            try
            {
                DateTime dateIni = DateTime.Today;
                DateTime dateEnd = DateTime.Today.AddDays(1);


                var resultsFechaDevolucionPrenda = db.Solicitudes.Where(s => s.fechaDevolucionPrenda >= dateIni && s.fechaDevolucionPrenda < dateEnd)
                                               .Select(x => new
                                               {
                                                   id = x.id,
                                                   fecha = x.fechaDevolucionPrenda,
                                                   label = x.Estados != null ? x.Estados.nombre : string.Empty
                                               }).ToList();

                var resultsFechaEntregaGestor = db.Solicitudes.Where(s => s.fechaEntregaGestor >= dateIni && s.fechaEntregaGestor < dateEnd)
                                              .Select(x => new
                                              {
                                                  id = x.id,
                                                  fecha = x.fechaEntregaGestor,
                                                  label = x.Estados != null ? x.Estados.nombre : string.Empty
                                              }).ToList();

                var resultsFechaFirma = db.Solicitudes.Where(s => s.fechaFirma >= dateIni && s.fechaFirma < dateEnd)
                                            .Select(x => new
                                            {
                                                id = x.id,
                                                fecha = x.fechaFirma,
                                                label = x.Estados != null ? x.Estados.nombre : string.Empty
                                            }).ToList();

                var resultsFechaInscripcionPrenda = db.Solicitudes.Where(s => s.fechaInscripcionPrenda >= dateIni && s.fechaInscripcionPrenda < dateEnd)
                                            .Select(x => new
                                            {
                                                id = x.id,
                                                fecha = x.fechaInscripcionPrenda,
                                                label = x.Estados != null ? x.Estados.nombre : string.Empty
                                            }).ToList();

                var resultsFechaNuevaLlamada = db.Solicitudes.Where(s => s.fechaNuevaLlamada >= dateIni && s.fechaNuevaLlamada < dateEnd)
                                           .Select(x => new
                                           {
                                               id = x.id,
                                               fecha = x.fechaNuevaLlamada,
                                               label = x.Estados != null ? x.Estados.nombre : string.Empty
                                           }).ToList();

                var resultsFechaUltimoPago = db.Solicitudes.Where(s => s.fechaUltimoPago >= dateIni && s.fechaUltimoPago < dateEnd)
                                           .Select(x => new
                                           {
                                               id = x.id,
                                               fecha = x.fechaUltimoPago,
                                               label = x.Estados != null ? x.Estados.nombre : string.Empty
                                           }).ToList();

                var results = resultsFechaDevolucionPrenda.Concat(resultsFechaEntregaGestor).Concat(resultsFechaFirma).Concat(resultsFechaInscripcionPrenda).Concat(resultsFechaNuevaLlamada).Concat(resultsFechaUltimoPago);
                IList<object> objects = new List<object>();
                objects = results.ToList<object>();
                return Json(new { Result = objects }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(null);
            }
            
        }

    }
}
