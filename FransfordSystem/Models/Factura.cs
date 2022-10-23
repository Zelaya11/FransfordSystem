﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FransfordSystem.Models
{
    public class Factura
    {

        [Display(Name = "idFactura")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Key]
        public int IdFactura { get; set; }

        [Display(Name = "Fecha de Factura")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime fechaFactura { get; set; }

        [Display(Name = "Total")]
        public float totalFactura { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int idCliente { get; set; }
        public Cliente? cliente { get; set; }

        


    }
}
