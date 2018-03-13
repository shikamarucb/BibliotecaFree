using Biblioteca.Models;
using System.Linq;
using System.Web.Mvc;

namespace Biblioteca.Servicios
{
    public class Registros
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static bool RegistrarLibro(AutorLibroVolumenVM model, ModelStateDictionary ModelState)
        {
            var nombreAutor = model.Autor.Nombre;
            var IdAutor = db.Autores.Where(a => a.Nombre == nombreAutor).Select(a => a.Id).FirstOrDefault();
            var nombreLibro = model.Libro.Titulo;
            var IdLibro = db.Libros.Where(l => l.Titulo == nombreLibro && l.IdAutor == IdAutor).Select(l => l.Id).FirstOrDefault();

            if (IdAutor != 0)
            {
                if (IdLibro != 0)
                {
                    var vol = model.Volumen.Vol;
                    var IdVol = db.Volumenes.Where(v => v.Vol == vol && v.IdLibro == IdLibro).Select(v => v.Id).FirstOrDefault();

                    if (IdVol == 0)
                    {
                        model.Volumen.IdLibro = IdLibro;
                        if (ModelState.IsValid)
                        {
                            db.Volumenes.Add(model.Volumen);
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "El libro ya existe");
                        return false;
                    }

                }
                else
                {
                    model.Libro.IdAutor = IdAutor;

                    if (ModelState.IsValid)
                    {
                        db.Libros.Add(model.Libro);
                        db.SaveChanges();

                        var IdLibroNew = db.Libros.Where(l => l.Titulo == nombreLibro && l.IdAutor == IdAutor).Select(l => l.Id).FirstOrDefault();

                        model.Volumen.IdLibro = IdLibroNew;
                        db.Volumenes.Add(model.Volumen);
                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Autores.Add(model.Autor);
                    db.SaveChanges();

                    var nombreAutorNew = model.Autor.Nombre;
                    var IdAutorNew = db.Autores.Where(a => a.Nombre == nombreAutorNew).Select(a => a.Id).FirstOrDefault();

                    model.Libro.IdAutor = IdAutorNew;
                    db.Libros.Add(model.Libro);
                    db.SaveChanges();

                    var IdLibroNew = db.Libros.Where(l => l.Titulo == nombreLibro && l.IdAutor == IdAutorNew).Select(l => l.Id).FirstOrDefault();

                    model.Volumen.IdLibro = IdLibroNew;
                    db.Volumenes.Add(model.Volumen);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }
    }
}