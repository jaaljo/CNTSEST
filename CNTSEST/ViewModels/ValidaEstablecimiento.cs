using CNTS.Models;
using CNTS.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CNTS.ViewModels
{
    public class ValidaEstablecimiento
    {
        public bool EsEstablecimientoExistente { get; set; }
        public string CodigoEstablecimiento { get; set; }
    }
}
