using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CNTS.Utilidades;
using CNTS.Validaciones;

namespace CNTS.ViewModels
{
    public class CambiarContrasenaViewModel 
    {

        public string original_password { get; set; }
        [Required(ErrorMessage = "La contraseña es un campo requerido.")]
        [CompararClaveCifrada]
        public string password { get; set; }
        [Required(ErrorMessage = "La contraseña nueva es un campo requerido.")]
        [Compare("repeat_password",ErrorMessage = "Las contraseñas no coninciden")]
        [PasswordSettings]
        public string new_password { get; set; }
        [Required(ErrorMessage = "La confirmacion de contraseña es un campo requerido.")]
        public string repeat_password { get; set; }

    }
}