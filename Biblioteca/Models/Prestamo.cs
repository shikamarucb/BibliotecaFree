using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Prestamo
    {
        public int Id { get; set; }

        public DateTime FechaPrestamo { get; set; }

        public DateTime FechaDevolucion { get; set; }

        public DateTime FechaMaxima { get; set; }

        [ForeignKey("IdApplicationUser")]
        public ApplicationUser ApplicationUser { get; set; }
        public string IdApplicationUser { get; set; }

        [ForeignKey("IdVolumen")]
        public Volumen Volumen { get; set; }
        public int IdVolumen { get; set; }
    }
}