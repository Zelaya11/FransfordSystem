using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FransfordSystem.Models
{
    public class Cita
    {
        [Key]
        [Required]
        public int idCita { get; set; }


        [Display(Name = "Cliente")]
        public string? idCliente { get; set; }
        public Cliente? cliente { get; set; }


        [Display(Name = "Nombre de la empresa")]
        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙÑñ.-]+", ErrorMessage = "Ingrese un nombre válido")]
        public string? nombreEmpresa { get; set; }


        [Display(Name = "Fecha de la cita")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime fechaCita { get; set; }


        [Display(Name = "Hora de la cita")]
        [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Ingrese un formato horario válido")]
        public string? horaCita { get; set; }
  

    }
}
