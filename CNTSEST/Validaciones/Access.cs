using System;
using System.Web.Mvc;
using System.Web;
using CNTS.Models;
using CNTS.Seguridad;
using CNTS.Utilidades;

namespace CNTS.Validaciones
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AccessAttribute : ActionFilterAttribute
    {
        private string _funcion;
        private SeguridadUtilidades utilidades = new SeguridadUtilidades();
        private CNTSEntities db = new CNTSEntities();
        

        public string Funcion
        {
            get { return _funcion ?? String.Empty; }
            set
            {
                _funcion = value;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if(_funcion == "UsuarioNR")
            {
                return;
            }

            IdentityPersonalizado Ident = (IdentityPersonalizado)HttpContext.Current.User.Identity;

            bool checkedpass;
            try
            {
                checkedpass = (bool)HttpContext.Current.Session["CHECKEDPASS"];
            }
            catch
            {
                checkedpass = true;
            }
            

            if (!checkedpass)
            {
                double total_segundos = Utilidades.Utilidades.SegundosTiempoCaducidad();
                
                if (total_segundos > -1)
                {
                    //obtener fecha actual y fecha de ultimo cambio de contraseña
                    DateTime actual = DateTime.Now;
                    DateTime ultima = Ident.Fe_cambio_password;
                    //la diferencia se medira en segundos
                    double diferencia = actual.Subtract(ultima).TotalSeconds;

                    double tiempo_restante = total_segundos - diferencia;

                    //que pasa si quedan menos de 3 días pero mas de 0 segundo
                    if (tiempo_restante < 253800 && tiempo_restante > 0)
                    {

                        //fijamos la variable de sesion con el tiempo restante
                        HttpContext.Current.Session["STCP"] = ((int)tiempo_restante).ToString();
                        //Marcamos la variable de sesion para evitar dobles validaciones
                        HttpContext.Current.Session["CHECKEDPASS"] = true;
                        //redirigimos a la funcion home/index
                        filterContext.Result = new RedirectResult(string.Format("~/Home/Index", filterContext.HttpContext.Request.Url.Host));
                    }

                    //que pasa si es negativo el resultado
                    if (tiempo_restante <= 0)
                    {
                        //fijamos la variable de sesion con un -1 para mostrar un aviso al cargar la vista de cambio de contraseña
                        HttpContext.Current.Session["STCP"] = "-1";
                        //redirigimos a la funcion home/index
                        filterContext.Result = new RedirectResult(string.Format("~/UsuarioNR/ChangePassword", filterContext.HttpContext.Request.Url.Host));
                    }
                }
            }

            //Si la contraseña fue establecida por el administrador, solicitar el cambio
            if(Ident.Id_estatus_establecimiento == 1)
            {
                HttpContext.Current.Session["SCC"] = "true";
                filterContext.Result = new RedirectResult(string.Format("~/UsuarioNR/ChangePassword", filterContext.HttpContext.Request.Url.Host));
            }

            if (_funcion == null)
            {
                return;
            }
            //string[] funciones = utilidades.ObtenerFunciones();

            //foreach (string fn in funciones)
            //{
            //    if (_funcion.Equals(fn))
            //    {
            //        return;
            //    }
            //}

            filterContext.Result = new RedirectResult(string.Format("~/Error/Denied", filterContext.HttpContext.Request.Url.Host));
        }

        public override void OnActionExecuted (ActionExecutedContext context)
        {
            string controlador = context.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            string action = context.Controller.ControllerContext.RouteData.Values["action"].ToString();
            string mensaje = controlador + "/" + action;
            string ruta = context.Controller.ControllerContext.RouteData.ToString();
            if(_funcion == "UsuarioNR")
            {
                return;
            }
            if(_funcion != null)
            {
                IdentityPersonalizado Ident = (IdentityPersonalizado)HttpContext.Current.User.Identity;

                h_acceso_establecimiento acceso = new h_acceso_establecimiento();

                acceso.fe_acceso = DateTime.Now;
                acceso.id_establecimiento = Ident.Id_establecimiento;
                acceso.nb_funcion = mensaje;

                db.h_acceso_establecimiento.Add(acceso);
                db.SaveChanges();
            }
            
            return;
        }
    }
}