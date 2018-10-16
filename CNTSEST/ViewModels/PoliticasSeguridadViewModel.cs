using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CNTS.Utilidades;
using CNTS.Validaciones;

namespace CNTS.ViewModels
{
    public class PoliticasSeguridadViewModel
    {
        [Range(5, Int32.MaxValue, ErrorMessage = "La longitud máxima no puede ser menor a 5.")]
        public int MaxLongitudPass { get; set; }
        [Range(0, 10, ErrorMessage = "La longitud mímina no puede ser más grande que 10 caracteres o menor a 0.")]
        public int MinLongitudPass { get; set; }
        public string TiempoCaducidad { get; set; }
        public string BloqueoNoIngreso { get; set; }
        [Range(3, Int32.MaxValue, ErrorMessage = "El valor mínimo aceptado es 3.")]
        public int IntentosMaximos { get; set; }
        [Range(5, 3600, ErrorMessage = "No se puede establecer el tiempo en mas de 3600 o menor a 5 minutos.")]
        public int TiempoEntreIntentos { get; set; }
        [Range(5, 3600, ErrorMessage = "No se puede establecer el tiempo en mas de 3600 o menor a 5 minutos.")]
        public int TiempoSesion { get; set; }
        public bool ReutilizarPass { get; set; }
        public bool Mayuscula { get; set; }
        public bool Numero { get; set; }
        public bool RepetirUsuario { get; set; }
        public bool BSI { get; set; } //bloqueo por superar el maximo de intentos


        [Range(1, 365, ErrorMessage = "No se pueden usar valores menores que 1 ni rangos mayores a 365 días.")]
        public int DDtc { get; set; }
        public int MMMtc { get; set; }
        public int AAtc { get; set; }
        public int hhtc { get; set; }
        public int mmtc { get; set; }
        public int sstc { get; set; }

        [Range(1, 365, ErrorMessage = "No se pueden usar valores menores que 1 ni rangos mayores a 365 días.")]
        public int DDbni { get; set; }
        public int MMMbni { get; set; }
        public int AAbni { get; set; }
        public int hhbni { get; set; }
        public int mmbni { get; set; }
        public int ssbni { get; set; }

    }
}