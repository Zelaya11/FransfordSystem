using System.ComponentModel.DataAnnotations;
namespace FransfordSystem.Models
{
    public class FacturaExamen
    {
        [Display(Name = "idFacEx")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int idFacEx { get; set; }
      
        public int idExamen { get; set; }

        public int idFactura { get; set; }
        public int idCliente { get; set; }
        public int precioExamen { get; set; }

        public Factura? Factura { get; set; }

        public Examen? Examen  { get; set; }




    }
}
