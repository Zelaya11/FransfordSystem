using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FransfordSystem.Models
{
    public class ReporteExamen
    {

        [Display(Name = "idReporteExamen")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int IdReporteExamen { get; set; }

        [Display(Name = "Fecha de Reporte")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime fechaReporte { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idCliente { get; set; }
        public Cliente? cliente { get; set; }


        public ICollection<Resultado>? resultados { get; set; }
    }
}
