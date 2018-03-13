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
    public class Autor
    {
        public int Id { get; set; }

        [Index(IsUnique =true)]
        [Required]
        [Display(Name ="Nombre Autor")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Nacimiento { get; set; }

        [ScriptIgnore]
        public virtual List<Libro> Libros { get; set; }
    }
}