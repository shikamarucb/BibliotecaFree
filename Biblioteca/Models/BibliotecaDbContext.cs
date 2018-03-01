using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class BibliotecaDbContext:DbContext
    {
        public BibliotecaDbContext():base(nameOrConnectionString: "BibliotecaCS")
        {
            
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Volumen> Volumenes { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
    }
}