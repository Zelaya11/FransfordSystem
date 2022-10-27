using System.ComponentModel.DataAnnotations;
namespace FransfordSystem.Models
{
    public class FacturaExamen
    {
        [Display(Name = "idFacEx")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int idFacEx { get; set; }

        [RegularExpression(@"^([1-9]+\d*)", ErrorMessage = "Ingrese un nombre válido")]
        public int idExamen { get; set; }

        public int idFactura { get; set; }
        public int idCliente { get; set; }

        [Range(0, 9999.99)]
        [Required(ErrorMessage = "El precio de examen es requerido")]
        public double precioExamen { get; set; }

        public Factura? Factura { get; set; }

        public Examen? Examen  { get; set; }




    }
}
