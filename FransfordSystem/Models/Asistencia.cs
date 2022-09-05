using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FransfordSystem.Models
{
    public class Asistencia
    {
        //ID de la asistencia
        [Display(Name = "idAsistencia")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int IdAsistencia { get; set; }

        [Display(Name = "idUsuario")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idUsuario { get; set; }
        public Usuario? usuario { get; set; }

        //Hora de entrada
        [Display(Name = "Hora de entrada")]
        public DateTime horaEntrada { get; set; }

        //Hora de salida
        [Display(Name = "Hora de salida")]
        public DateTime horaSalida { get; set; }

    }
}
