using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CNTS.Models;
using CNTS.Validaciones;

namespace CNTS.ViewModels
{
    public /*partial*/ class AgregarRolViewModel
    {
        public int id_rol { get; set; }
        [Required(ErrorMessage = "La clave es un campo requerido.")]
        [StringLength(20, ErrorMessage = "La clave puede tener hasta 20 caracteres.")]
        public string cl_rol { get; set; }
        [Required(ErrorMessage = "El nombre es un campo requerido.")]
        [StringLength(256, ErrorMessage = "El nombre puede tener hasta 256 caracteres.")]
        [Exists(ErrorMessage = "Ya existe un rol con ese nombre.")]
        public string nb_rol { get; set; }
        public int[] id_funcion { get; set; }
    }
}