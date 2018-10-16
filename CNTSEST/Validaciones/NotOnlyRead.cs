using System;
using System.Web;
using System.Web.Mvc;
using CNTS.Seguridad;
using CNTS.Utilidades;

namespace CNTS.Validaciones
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class NotOnlyReadAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //IdentityPersonalizado Ident = (IdentityPersonalizado)HttpContext.Current.User.Identity;
            //if (Ident.Solo_lectura)
            //{
            //    filterContext.Result = new RedirectResult(string.Format("~/Error/OnlyRead", filterContext.HttpContext.Request.Url.Host));
            //}
            //else
            //{
            //    return;
            //}
            return;
        }
    }
}