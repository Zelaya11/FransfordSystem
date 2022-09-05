using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FransfordSystem.Models
{
    public class Producto
    {
        //ID del producto
        [Display(Name = "idProducto")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int IdProducto { get; set; }

        //Nombre del producto
        [Display(Name = "Nombre del producto")]
        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙÑñ.-]+", ErrorMessage = "Ingrese nombres con caracteres válidos")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string nombreProducto { get; set; }

        //Nombre del proveedor
        [Display(Name = "Nombre del proveedor")]
        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙÑñ.-]+", ErrorMessage = "Ingrese nombres con caracteres válidos")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string proveedorProducto { get; set; }


    }
}
