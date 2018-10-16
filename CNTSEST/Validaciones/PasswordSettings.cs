using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CNTS.Seguridad;
using CNTS.Utilidades;
using CNTS.ViewModels;

namespace CNTS.Validaciones
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PasswordSettingsAttribute : ValidationAttribute
    {
        private static Models.CNTSEntities db = new Models.CNTSEntities();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (CambiarContrasenaViewModel)validationContext.ObjectInstance;
            string Original = model.original_password;

            if (value != null)
            {
                string encPass = SeguridadUtilidades.SHA256Encripta(value.ToString());

                //Leer cada atributo necesario

                int MaxLongitudPass = Utilidades.Utilidades.GetIntSecurityProp("MaxLongitudPass", "10");

                int MinLongitudPass = Utilidades.Utilidades.GetIntSecurityProp("MinLongitudPass", "6");

                bool ReutilizarPass = Utilidades.Utilidades.GetBoolSecurityProp("ReutilizarPass","false");

                bool Mayuscula = Utilidades.Utilidades.GetBoolSecurityProp("Mayuscula","false");

                bool Numero = Utilidades.Utilidades.GetBoolSecurityProp("Numero","false");
                
                bool RepetirUsuario = Utilidades.Utilidades.GetBoolSecurityProp("RepetirUsuario","false");

                IdentityPersonalizado Ident = (IdentityPersonalizado)HttpContext.Current.User.Identity;

                //Verificar si existe validacion de logintud
                if (value.ToString().Length > MaxLongitudPass)
                {
                    return new ValidationResult("La longitud máxima de la contraseña es de " + MaxLongitudPass + " caracteres.");
                }
                if (value.ToString().Length < MinLongitudPass)
                {
                    return new ValidationResult("La longitud mínima de la contraseña es de " + MinLongitudPass + " caracteres.");
                }

                //Verificar si se puede reutilizar el password
                //if (!ReutilizarPass)
                //{
                //    var passwords = db.h_password.Where(p => p.id_usuario == Ident.Id_usuario).ToList();

                //    foreach(var pass in passwords)
                //    {
                //        if (pass.password == encPass)
                //        {
                //            return new ValidationResult("No esta permitido reutilizar contraseñas anteriores.");
                //        }
                //    }                    
                //}
                //Requiere una mayuscula
                if (Mayuscula)
                {
                    bool capital = false;
                    foreach (char c in value.ToString())
                    {
                        if ((int)c < 91 && (int)c > 64)
                        {
                            capital = true;
                            break;
                        }
                    }
                    if (!capital)
                    {
                        return new ValidationResult("La contraseña debe contener al menos una mayúscula");
                    }
                }
                //Requiere un número
                if (Numero)
                {
                    bool num = false;
                    foreach (char c in value.ToString())
                    {
                        if ((int)c < 58 && (int)c > 47)
                        {
                            num = true;
                            break;
                        }
                    }
                    if (!num)
                    {
                        return new ValidationResult("La contraseña debe contener al menos un número");
                    }
                }
                //Puede incluir el usuario?
                //if (!RepetirUsuario)
                //{
                //    string nb_usuario = Ident.Email_principal;
                //    if (value.ToString().Contains(nb_usuario))
                //    {
                //        return new ValidationResult("La contraseña no puede contener tu nombre de usuario");
                //    }
                //}
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Ingresa tu contraseña.");
            }
        }
    }
}