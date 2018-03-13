using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Biblioteca.Models
{
    public class Volumen
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="¿Está Deteriorado?")]
        public bool Deteriorado { get; set; }

        [Required]
        [Display(Name ="Volumen")]
        public int Vol { get; set; }

        [Required]
        [Display(Name = "Número de Unidades")]
        public int NUnidades { get; set; }

        [ForeignKey("IdLibro")]
        public virtual Libro Libro { get; set; }

        [Required]
        public int IdLibro { get; set; }

        [ScriptIgnore]
        public virtual List<Prestamo> Prestamos { get; set; }
    }
}