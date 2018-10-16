using System.ComponentModel.DataAnnotations;

namespace CNTS.ViewModels
{
    
    public class EditarDatosUsuarioViewModel
    {
        [Required(ErrorMessage = "El nombre es un campo requerido.")]
        [StringLength(128, ErrorMessage = "Longitud 128.")]
        public string nb_establecimiento { get; set; }

        [Required(ErrorMessage = "El correo principal es olbigatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese una cuenta de correo valida.")]
        [StringLength(128, ErrorMessage = "Longitud 128.")]
        public string email_principal { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Ingrese un número de teléfono valido.")]
        public string telefono { get; set; }
    }
}