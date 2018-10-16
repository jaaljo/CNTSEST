using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;
using CNTS.Seguridad;
using CNTS.ViewModels;
using CNTS.Utilidades;
using CNTS.Validaciones;
using System.Data.Entity.Core;

namespace CNTS.Controllers
{
    [Authorize]
    [Access(Funcion = null)]
    [CustomErrorHandler]
    public class HomeController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        public ActionResult Index()
        {
            //Obtenemos el valor de la variable de sesion
            int segundos_restantes;
            try
            {
                segundos_restantes = Int32.Parse(HttpContext.Session["STCP"].ToString());
            }
            catch
            {
                segundos_restantes = 253800;
            }
            //si quedan menos de 253800 segundos (3 días) construir el mensaje que se mostrará
            if(segundos_restantes < 253800)
            {
                int dias = segundos_restantes/86400;
                segundos_restantes = segundos_restantes % 86400;
                int horas = segundos_restantes/3600;
                segundos_restantes = segundos_restantes % 3600;
                int minutos = segundos_restantes/60;
                string mensaje = "Quedan: " + dias + " días " + horas + " horas y " + minutos + " minutos para que la contraseña actual expire";
                ViewBag.Mensaje = mensaje;
            }
            else
            {
                ViewBag.Mensaje = "false";
            }

            HttpContext.Session["STCP"] = "253800";

            // Obtiene los datos generales para mostrarlos en la página de inicio
            IdentityPersonalizado identity = (IdentityPersonalizado)ControllerContext.HttpContext.User.Identity;
            c_establecimiento c_establecimiento = db.c_establecimiento.Find(identity.Id_establecimiento);
            ViewBag.responsable = "Responsable: " + c_establecimiento.nb_responsable;

            var periodo = db.c_periodo.Where(u => u.esta_activo == true).FirstOrDefault();
            ViewBag.periodo = periodo != null ? "Periodo Activo: " + periodo.nb_periodo : "Sin Periodo Activo";
            return View();
        }
    }
}