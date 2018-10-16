using CNTS.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CNTS.Controllers
{
    [AllowAnonymous]
    public class InicioController : Controller
    {
        private CNTSEntities db = new CNTSEntities();

        // GET: Inicio
        public ActionResult Index()
        { 
            ViewBag.Mensaje = null;
            c_establecimiento model = new c_establecimiento();
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(c_establecimiento model)
        {
            string pass = model.password;

            var listUsers = db.c_establecimiento.ToList();

            try
            {
                model = db.c_establecimiento.Where(u => u.codigo_establecimiento == model.codigo_establecimiento).First();
            }
            catch
            {
                ViewBag.Mensaje = "false";
                ViewBag.Error = "Nombre de usuario o contraseña incorrectos.";
                return View(model);
            }

            ////obtenemos El numero de intentos máximo y el tiempo entre intentos
            //int NoIntentos = Utilidades.Utilidades.GetIntSecurityProp("IntentosMaximos", "3");
            //int intentosRestantes = 0;

            ////Tomaremos los tiempos en segundos
            //    int TiempoEntreIntentos = Utilidades.Utilidades.GetIntSecurityProp("TiempoEntreIntentos", "30") * 60;
            //    int TiempoDesdeUtimoIntento = (int)DateTime.Now.Subtract(model.fe_ultimo_intento_acceso ?? DateTime.Now).TotalSeconds;
            //    int TiempoSiguienteIntento = (TiempoEntreIntentos - TiempoDesdeUtimoIntento) / 60;

            //    if (TiempoDesdeUtimoIntento > TiempoEntreIntentos || TiempoDesdeUtimoIntento < 1)
            //    {
            //        model.no_intento_acceso = 0;
            //    }

            //    if (model.no_intento_acceso < NoIntentos)
            //    {
            //        model.no_intento_acceso++;
            //        model.fe_ultimo_intento_acceso = DateTime.Now;
            //    }
            //    intentosRestantes = NoIntentos - model.no_intento_acceso;



            if (Membership.ValidateUser(model.codigo_establecimiento, pass))
            {
                model.fe_ultimo_acceso = DateTime.Now;

                db.SaveChanges();

                int aux;
                int TiempoSesion = Int32.TryParse(Utilidades.Utilidades.GetSecurityProp("TiempoSesion","20"), out aux) ? ((aux < 0) ? 30 : aux) : (30);

                //Variable de sesion para realizar 1 vez la validacion de la caducidad de la contraseña
                FormsAuthentication.SetAuthCookie(model.codigo_establecimiento, false);
                FormsAuthenticationTicket ticket =
                    new FormsAuthenticationTicket(1, model.codigo_establecimiento, DateTime.Now, DateTime.Now.AddMinutes(TiempoSesion), false, model.id_establecimiento.ToString());
                string encTicket = FormsAuthentication.Encrypt(ticket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { Expires = ticket.Expiration });
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Redirect(FormsAuthentication.GetRedirectUrl(model.codigo_establecimiento, false));
                HttpContext.Session["CHECKEDPASS"] = false;
                //Seconds To Change Password
                //Si quedan menos de 3 días para cambiar la contraseña, se mostrara un aviso en la pantalla de inicio
                HttpContext.Session["STCP"] = "253800";
                HttpContext.Session["SCC"] = "false";

                HttpContext.Session.Timeout = 30;

                var cadena = HttpContext.Session["SCC"].ToString();
                return null;
            }
            //en caso de que ValidateUser retorne false, verificar si el usuario esta bloqueado

            if (model.id_estatus_establecimiento == 4)
            {
                ViewBag.Mensaje = "Su usuario se encuentra bloqueado, por favor acuda con el Administrador del sistema";
            }
            else
            {
                ViewBag.Mensaje = "false";
                

                
                
            }
            db.SaveChanges();
            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}