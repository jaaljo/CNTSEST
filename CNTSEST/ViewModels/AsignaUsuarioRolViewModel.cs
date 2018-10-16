using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CNTS.Models;
using CNTS.Validaciones;

namespace CNTS.ViewModels
{
    public /*partial*/ class AsignaUsuarioRolViewModel
    {
        public int id_rol { get; set; }
        public int[] id_usuario { get; set; }
    }
}