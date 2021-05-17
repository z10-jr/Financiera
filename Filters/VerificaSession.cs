using Financiera.Models;
using PersimosMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersimosMVC.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private usuarios oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUsuario = (usuarios)HttpContext.Current.Session["User"];
                if (oUsuario == null)
                {
                   
                        if (filterContext.Controller is AccesoController == false)
                        {
                            filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                        }
                   
                    

                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }

        }
    }
}