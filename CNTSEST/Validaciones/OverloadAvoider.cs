using System;
using System.Web;
using System.Web.Mvc;
using CNTS.Seguridad;
using CNTS.Utilidades;

namespace CNTS.Validaciones
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class OverloadAvoiderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            return;
        }
    }
}