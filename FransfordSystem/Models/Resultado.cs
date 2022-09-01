using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FransfordSystem.Models
{
    public class Resultado
    {

        [Display(Name = "idResultado")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int IdResultado { get; set; }

        [Display(Name = "Resultado del examen")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string resultadoExamen { get; set; }

        [Display(Name = "Descripcion Examen")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idDescripcion { get; set; }
        public Descripcion? descripcion { get; set; }

        [Display(Name = "Reporte Examen")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idReporteExamen { get; set; }
        public ReporteExamen? reporteExamen { get; set; }

        
    }
}
