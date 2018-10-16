using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CNTS.Models;
using CNTS.Utilidades;
using CNTS.ViewModels;
using Newtonsoft.Json;
using reCAPTCHA.MVC;

namespace CNTS.Controllers
{
    [AllowAnonymous]
    public class SolicitudIncorporacionController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        public ActionResult Paso1()
        {
            ValidaEstablecimiento model = new ValidaEstablecimiento();
            model.CodigoEstablecimiento = "";
            model.EsEstablecimientoExistente = false;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Paso2(ValidaEstablecimiento model)
        {
            ViewBag.Mensaje = null;
            k_solicitud_incorporacion solicitud = new k_solicitud_incorporacion();

            if (model.EsEstablecimientoExistente)
            {
                try
                {
                    // Verifica si existe un establecimiento con el código que escribieron en la página de solicitud
                    c_establecimiento_inicial establecimiento_inicial =
                        db.c_establecimiento_inicial.Where(u => u.codigo_establecimiento == model.CodigoEstablecimiento).First();

                    // Si encontró el establecimiento en la base de datos, regresa los datos que existen para mostrarse en la siguiente página
                    solicitud.codigo_establecimiento = establecimiento_inicial.codigo_establecimiento;
                    solicitud.folio_licencia_sanitaria = establecimiento_inicial.licencia_sanitaria;
                    solicitud.nb_establecimiento = establecimiento_inicial.nb_establecimiento;
                    solicitud.calle = establecimiento_inicial.calle;
                    solicitud.colonia = establecimiento_inicial.colonia;
                    solicitud.cp = establecimiento_inicial.cp;
                    solicitud.municipio = establecimiento_inicial.municipio;
                    solicitud.ciudad = establecimiento_inicial.ciudad;
                    solicitud.id_entidad_federativa = establecimiento_inicial.id_entidad_federativa ?? 1;
                    solicitud.id_tipo_establecimiento = establecimiento_inicial.id_tipo_establecimiento ?? 1;
                    solicitud.id_institucion = establecimiento_inicial.id_institucion ?? 1;

                    ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "nb_entidad_federativa", solicitud.id_entidad_federativa);
                    ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "nb_institucion", solicitud.id_institucion);
                    ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "nb_tipo_establecimiento", solicitud.id_tipo_establecimiento);
                }
                catch
                {
                    ViewBag.Mensaje = "El Establecimiento no se encuentra registrado en la base de datos del CNTS.";
                    return View("Paso1", model);
                }
            }
            else
            {
                ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "nb_entidad_federativa");
                ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "nb_institucion");
                ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "nb_tipo_establecimiento");
            }

            return View(solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Paso3(k_solicitud_incorporacion solicitud)
        {
            ViewBag.Mensaje = null;

            if (ModelState.IsValid)
            {
                return View(solicitud);
            }

            ViewBag.id_entidad_federativa = new SelectList(db.c_entidad_federativa, "id_entidad_federativa", "nb_entidad_federativa", solicitud.id_entidad_federativa);
            ViewBag.id_institucion = new SelectList(db.c_institucion, "id_institucion", "nb_institucion", solicitud.id_institucion);
            ViewBag.id_tipo_establecimiento = new SelectList(db.c_tipo_establecimiento, "id_tipo_establecimiento", "nb_tipo_establecimiento", solicitud.id_tipo_establecimiento);
            ViewBag.Mensaje = "No has llenado varios campos requeridos. Por favor verifica.";
            return View("Paso2", solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidator(ErrorMessage = "Validación Captcha incorrecta.", RequiredMessage = "La validación Captcha es requerida.")]
        public ActionResult ConfirmaSolicitud(k_solicitud_incorporacion solicitud, HttpPostedFileBase file1, HttpPostedFileBase file2, bool captchaValid)
        {
            solicitud.fe_solicitud = DateTime.Now;
            solicitud.uuid = Guid.NewGuid().ToString();
            bool docsValid = validateFiles(ModelState, file1, file2);
            if (ModelState.IsValid && docsValid && captchaValid)
            {
                byte[] dataFile1, dataFile2;
                using (BinaryReader br = new BinaryReader(file1.InputStream))
                {
                    dataFile1 = br.ReadBytes(file1.ContentLength);
                }
                solicitud.data_archivo_1 = dataFile1;

                using (BinaryReader br = new BinaryReader(file2.InputStream))
                {
                    dataFile2 = br.ReadBytes(file2.ContentLength);
                }
                solicitud.data_archivo_2 = dataFile2;

                db.k_solicitud_incorporacion.Add(solicitud);
                db.SaveChanges();

                string contenido =
                    "<strong>" + solicitud.nb_establecimiento + "</strong>" +
                    "<br /> Hemos recibido tu solicitud de incorporación a la plataforma y en breve se te notificará el resultado de la evaluación." +
                    "<br /><br />  Folio de solicitud: " + solicitud.id_solicitud_incorporacion +
                    "<br /> Código de Verificación: " + solicitud.uuid;

                Utilidades.Utilidades.EnviaCorreo(solicitud.email_principal_responsable, "Confirmación de solicitud de incorporación", contenido);
                
                return View();
            }
            ViewBag.Mensaje =
                ((!docsValid && !captchaValid) ? "Debe de adjuntar los documentos solicitados en formato PDF y confirmar la validación CAPTCHA." : (!docsValid ? "Debe de adjuntar los documentos solicitados en formato PDF." : "Debe de confirmar la validación CAPTCHA."));

            return View("Paso3", solicitud);
        }

        private bool validateFiles(ModelStateDictionary modelState, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            if (file1 == null) modelState.AddModelError("nb_archivo_1", "Por favor seleccione un archivo");
            if (file2 == null) modelState.AddModelError("nb_archivo_2", "Por favor seleccione un archivo");

            return(!(file1 == null || file2 == null || file1.ContentType != "application/pdf" || file2.ContentType != "application/pdf"));
        }




        public string PostalCode(string cp)
        {

            /*var conColonia = db.c_cp.Where(c => c.nb_asentamiento != null).ToList();
            foreach(var codigo in conColonia)
            {
                Debug.WriteLine("Colonia: " + codigo.nb_asentamiento);
            }*/

            try
            {
                var codeP = db.c_cp.Where(c => c.cp == cp).First();
                return JsonConvert.SerializeObject(codeP);
            }
            catch
            {
                return null;
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
