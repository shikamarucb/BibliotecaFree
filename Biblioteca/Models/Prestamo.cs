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

        [ForeignKey("IdPersona")]
        public Persona Persona { get; set; }

        public int IdPersona { get; set; }

        public virtual List<Volumen> Volumenes { get; set; }
    }
}