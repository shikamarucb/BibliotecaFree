using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;

namespace Biblioteca.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Título del Libro")]
        [StringLength(1500)]
        public string Titulo { get; set; }

        [Display(Name = "Año de Escritura")]
        public uint? Anio { get; set; }

        [Required]
        [Display(Name = "ISBN")]
        [StringLength(30)]
        public string Isbn { get; set; }

        [StringLength(100)]
        public string Editorial { get; set; }
        public List<Volumen> Volumenes { get; set; }

        // [ScriptIgnore]: usar este atributo cuando se genere un referencia circular, ponerlo
        // en las dos entidades relacionadas
        [ForeignKey("IdAutor")]
        public Autor Autor { get; set; }

        [Required]
        public int IdAutor { get; set; }
    }
}