using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class AutorLibroVolumenVM
    {
        public Autor Autor { get; set; }
        public Libro Libro { get; set; }
        public Volumen Volumen { get; set; }
    }
}