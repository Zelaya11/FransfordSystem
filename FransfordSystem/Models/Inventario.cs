using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FransfordSystem.Models
{
    public class Inventario
    {
        //ID de la asistencia
        [Display(Name = "idInventario")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int idInventario { get; set; }

        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string idProducto { get; set; }
        public Producto? producto { get; set; }

        public DateTime fechaNacimiento { get; set; }
        //Fecha ingreso
        [Display(Name = "Fecha de ingreso")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime fechaIngreso { get; set; }

        //Fecha vencimiento
        [Display(Name = "Fecha de vencimiento")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime fechaVencimiento { get; set; }

        [Display(Name = "Stock")]
        [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "Ingrese un stock valido")]
        //[Required(ErrorMessage = "Este campo es obligatorio")]
        public int? stock { get; set; }

    }
}