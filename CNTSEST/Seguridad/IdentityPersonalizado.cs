using System;
using System.Security.Principal;
using System.Web.Security;

namespace CNTS.Seguridad
{
    public class IdentityPersonalizado : IIdentity
    {
        public string Name
        {
            get { return Nb_establecimiento; }
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public int Id_establecimiento { get; set; }
        public string Codigo_establecimiento { get; set; }
        public string Nb_establecimiento { get; set; }
        public string Password { get; set; }
        public bool Esta_activo { get; set; }
        public int Id_estatus_establecimiento { get; set; }
        public DateTime Fe_cambio_password { get; set;}
        public DateTime Fe_ultimo_acceso { get; set; }

        public IIdentity Identity { get; set; }

        public IdentityPersonalizado(IIdentity identity)
        {
            this.Identity = identity;
            var us = Membership.GetUser(identity.Name) as UsuarioMembership;
            
            Id_establecimiento = us.Id_establecimiento;
            Codigo_establecimiento = us.Codigo_establecimiento;
            Nb_establecimiento = us.Nb_establecimiento;
            Password = us.Password;
            Esta_activo = us.Esta_activo;
            Id_estatus_establecimiento = us.Id_estatus_establecimiento;
            Fe_cambio_password = us.Fe_cambio_password;
            Fe_ultimo_acceso = us.Fe_ultimo_acceso;
        }
    }
}