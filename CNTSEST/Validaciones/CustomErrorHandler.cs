using System;
using System.Web.Mvc;
using System.Web;
using CNTS.Models;
using CNTS.Seguridad;
using CNTS.Utilidades;

namespace CNTS.Validaciones
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CustomErrorHandlerAttribute : HandleErrorAttribute
    {
        //private string _funcion;
        private SeguridadUtilidades utilidades = new SeguridadUtilidades();
        private CNTSEntities db = new CNTSEntities();
        

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            h_excepcion excepcion = new h_excepcion();

            excepcion.fe_excepcion = DateTime.Now;
            excepcion.ds_excepcion = filterContext.Exception.Message;
            excepcion.nb_metodo = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString();

            var aux = filterContext.Controller.GetType().GetCustomAttributesData();
            string funcion = "";

            foreach (var attribute in aux)
            {
                
                //if (attribute.AttributeType.Name == "OverloadAvoiderAttribute") return;

                    if (attribute.AttributeType.Name == "AccessAttribute")
                {
                    funcion = (string)attribute.NamedArguments[0].TypedValue.Value;
                    if(funcion == null)
                    {
                        //Regresar una vista con buena presentacion con los datos del error
                        return;
                    }
                    break;
                }
            }
            excepcion.id_funcion = utilidades.IdFuncion(funcion);

            db.h_excepcion.Add(excepcion);
            db.SaveChanges();

            //Regresar una vista con buena presentacion con los datos del error
            return;
        }
    }
}