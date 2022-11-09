using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FransfordSystem.Models
{
    public class Inventario
    {
        //ID del inventario
        [Display(Name = "idInventario")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int idInventario { get; set; }

        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idProducto { get; set; }
        public Producto? producto { get; set; }

        //Fecha vencimiento
        [Display(Name = "Fecha de vencimiento")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime fechaVencimiento { get; set; }

        [Display(Name = "Cantidad")]
        [Range(0, 1000000, ErrorMessage = "Ingrese un valor valido")]
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        public int? stock { get; set; }

        [Display(Name = "Entrada")]
        [Range(0, 1000000, ErrorMessage = "Ingrese un valor valido")]
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        public int? entrada { get; set; }

        [Display(Name = "Salida")]
        [Range(0, 1000000, ErrorMessage = "Ingrese un valor valido")]
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        public int? salida { get; set; }

     
    }
}