using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FransfordSystem.Models
{
    public class Descripcion
    {
        [Key]
        [Required]
        public int idDescripcion { get; set; }

        [Display(Name = "Examen")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idExamen { get; set; }
        public Examen? examen { get; set; }


        [Required(ErrorMessage = "Debe de ingresar la descripción del examen")]
        [StringLength(50, ErrorMessage = "No puede escribir más de 50 caracteres")]
        [Display(Name = "Descripcion del examen")]
        public string descripcionExamen { get; set; }

        [Display(Name = "Valor mínimo")]
        [RegularExpression(@"^-?(([1-9]\d*)|0)(.0*[1-9](0*[1-9])*)?$", ErrorMessage = "Valor invalido, debe ser de 0 a 15")]
        public float? valorMinimo { get; set; }

        [Display(Name = "Valor máximo")]
        [RegularExpression(@"^-?(([1-9]\d*)|0)(.0*[1-9](0*[1-9])*)?$", ErrorMessage = "Valor invalido, debe ser de 0 a 15")]
        public float? valorMaximo { get; set; }

        [Display(Name = "Unidades")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idUnidad { get; set; }
        public Unidad? unidad { get; set; }

        public ICollection<Resultado>? resultados { get; set; }
    }
}
