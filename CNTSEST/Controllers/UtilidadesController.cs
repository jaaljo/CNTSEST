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
    [Access(Funcion = "UsuarioNR")]
    [CustomErrorHandler]
    public class UtilidadesController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        public ActionResult Return(int catalogo,int? id){

            var redirect = false;

            int ns;
            try
            {
                ns = (int)HttpContext.Session["JumpCounter"];
            }
            catch
            {
                ns = 0;
            }
            //Si ns es 0 redireccionamos al index de este catalogo
            if (ns == 0)
            {
                redirect = true;
            }
            else
            {
                List<string> directions = new List<string>();
                try
                {
                    directions = (List<string>)HttpContext.Session["Directions"];
                }
                catch
                {
                    directions = null;
                }

                if (directions == null)
                {
                    redirect = true;
                }
                else
                {
                    string direction = directions.Last();
                    DirectionViewModel dir = Utilidades.Utilidades.getDirection(direction);
                    //disminuimos ns y eliminamos el ultimo elemento de directions
                    ns--;
                    directions.RemoveAt(ns);

                    //Guardamos ambas variables de sesion para seguir trabajando
                    HttpContext.Session["JumpCounter"] = ns;
                    HttpContext.Session["Directions"] = directions;

                    return RedirectToAction(dir.Action, dir.Controller, new { id = dir.Id, redirect = "bfo" });
                }
            }


            if (redirect)
            {
                switch (catalogo)
                {
                    case 1:
                        return RedirectToAction("Index", "Entidad", null);
                    case 2:
                        return RedirectToAction("Index", "MacroProceso", null);
                    case 3:
                        return RedirectToAction("Index", "Proceso", null);
                    case 4:
                        return RedirectToAction("Index", "SubProceso", null);
                    case 5:
                        return RedirectToAction("Index", "Area", null);
                    case 6:
                        return RedirectToAction("Index", "LineaNegocio", null);
                    case 7:
                        return RedirectToAction("Index", "Etapa", null);
                    case 8:
                        return RedirectToAction("Index", "SubEtapa", null);
                    case 9:
                        return RedirectToAction("Index", "CuentaContable", null);
                    case 10:
                        return RedirectToAction("Index", "CentroCosto", null);
                    case 11:
                        return RedirectToAction("Index", "OrigenIncidencia", null);
                    case 12:
                        return RedirectToAction("Index", "Indicador", null);
                    case 13:
                        return RedirectToAction("Index", "TipoEvaluacion", null);
                    case 14:
                        return RedirectToAction("Index", "ActividadCosteo", null);
                    case 15:
                        return RedirectToAction("Index", "TipologiaSubProceso", null);
                    case 16:
                        return RedirectToAction("Index", "PeriodoIndicador", null);
                    case 17:
                        return RedirectToAction("Index", "PeriodoCertificacion", null);
                    case 18:
                        return RedirectToAction("Index", "TipoNormatividad", null);
                    case 19:
                        return RedirectToAction("Index", "CategoriaRiesgo", null);
                    case 20:
                        return RedirectToAction("Index", "TipoRiesgo", null);
                    case 21:
                        return RedirectToAction("Index", "ClaseTipologiaRiesgo", null);
                    case 22:
                        return RedirectToAction("Index", "SubClaseTipologiaRiesgo", null);
                    case 23:
                        return RedirectToAction("Index", "TipologiaRiesgo", null);
                    case 24:
                        return RedirectToAction("Index", "ProbabilidadOcurrencia", null);
                    case 25:
                        return RedirectToAction("Index", "TipoImpacto", null);
                    case 26:
                        return RedirectToAction("Index", "MagnitudImpacto", null);
                    case 27:
                        return RedirectToAction("Index", "GradoCobertura", null);
                    case 28:
                        return RedirectToAction("Index", "FrecuenciaControl", null);
                    case 29:
                        return RedirectToAction("Index", "NaturalezaControl", null);
                    case 30:
                        return RedirectToAction("Index", "TipologiaControl", null);
                    case 31:
                        return RedirectToAction("Index", "CategoriaControl", null);
                    case 32:
                        return RedirectToAction("Index", "TipoEvidencia", null);
                    case 33:
                        return RedirectToAction("Index", "EstatusBDEI", null);
                    case 34:
                        return RedirectToAction("Index", "CategoriaPerdida", null);
                    case 35:
                        return RedirectToAction("Index", "CategoriaError", null);
                    case 36:
                        return RedirectToAction("Index", "Moneda", null);
                    case 37:
                        return RedirectToAction("Index", "TipoSolucion", null);
                    case 38:
                        return RedirectToAction("Index", "Actividad", null);
                    case 39:
                        return RedirectToAction("Index", "ProcesoBenchmark", null);
                    case 40:
                        return RedirectToAction("Index", "SubProcesoBenchmark", null);
                    case 41:
                        return RedirectToAction("Index", "EventoRiesgo", null);
                    case 42:
                        return RedirectToAction("Index", "UnidadIndicador", null);
                    case 43:
                        return RedirectToAction("Index", "Usuario", null);
                    case 44:
                        return RedirectToAction("Riesgos", "Riesgo", new { id = (int)id });
                    case 45:
                        return RedirectToAction("Controles", "Riesgo", new { id = (int)id });
                    case 46:
                        return RedirectToAction("Editar", "Riesgo", new { id = (int)id });
                    case 47:
                        return RedirectToAction("Certificados", "Certificacion", new { id = (int)id });
                    case 48:
                        return RedirectToAction("Index", "BDEI", null);
                    case 49:
                        return RedirectToAction("Evaluaciones", "EvaluacionIndicador", new { id = (int)id });
                    case 50:
                        return RedirectToAction("Index", "Benchmark", null);
                    default:
                        return RedirectToAction("Index", "Entidad", null);
                }
            }
            return null;
        }
    }
}
