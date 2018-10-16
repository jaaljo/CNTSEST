using CNTS.Models;
using CNTS.Utilidades;
using System;
using System.Web;
using System.Web.Security;

namespace CNTS.Seguridad
{
    public class UsuarioMembership:MembershipUser
    {
        private SeguridadUtilidades utilidades = new SeguridadUtilidades();

        public int Id_establecimiento { get; set; }
        public string Codigo_establecimiento { get; set; }
        public string Nb_establecimiento { get; set; }
        public string Password { get; set; }
        public bool Esta_activo { get; set; }
        public int Id_estatus_establecimiento { get; set; }
        public DateTime Fe_cambio_password { get; set; }
        public DateTime Fe_ultimo_acceso { get; set; }

        public UsuarioMembership(c_establecimiento us)
        {
            Id_establecimiento = us.id_establecimiento;
            Codigo_establecimiento = us.codigo_establecimiento;
            Nb_establecimiento = us.nb_establecimiento;
            Password = us.password;
            Esta_activo = us.esta_activo;
            Id_estatus_establecimiento = us.id_estatus_establecimiento; // ?? 2;
            Fe_cambio_password = us.fe_cambio_password ?? DateTime.Now;
            try
            {
                Fe_ultimo_acceso = (DateTime)HttpContext.Current.Session["UltimoAcceso"];
            }
            catch
            {
                Fe_ultimo_acceso = DateTime.Now;
            }
        }
    }
}