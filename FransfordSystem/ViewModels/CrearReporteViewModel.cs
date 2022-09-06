using FransfordSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace FransfordSystem.ViewModels
{
    public class CrearReporteViewModel
    {

        public Examen examen;
        public Descripcion descripcion;
        public Cliente cliente;
        public ReporteExamen reporte;

        [Display(Name = "idResultado")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int IdResultado { get; set; }
        [Display(Name = "Resultado del examen")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string resultadoExamen { get; set; }
        [Display(Name = "Descripcion Examen")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idDescripcion { get; set; }
        [Display(Name = "Reporte Examen")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idReporteExamen { get; set; }
    }
}
