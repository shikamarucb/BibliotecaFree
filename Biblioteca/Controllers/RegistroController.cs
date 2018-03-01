using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Servicios;

namespace Biblioteca.Controllers
{
    public class RegistroController : Controller
    {
        private BibliotecaDbContext db = new BibliotecaDbContext();
        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }

        // GET: Registro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Registro/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Registro/Create
        [HttpPost]
        public ActionResult Create(AutorLibroVolumenVM model)
        {
            if(Registros.RegistrarLibro(model, ModelState))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Error", "Algo ha pasado, intenta nuevamente");
                return View();
            }
        }

        [HttpPost]
        public JsonResult GetAutor(string prefijo)
        {
            var listaAutores = db.Autores.Where(c => c.Nombre.StartsWith(prefijo)).Select(x => x.Nombre);
            var n = listaAutores.Count();
            return Json(listaAutores, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLibro(string prefijo, string prefix)
        {
            var idAutor = db.Autores.Where(a => a.Nombre == prefix).Select(a => a.Id).FirstOrDefault();
            var listaAutores = db.Libros.Where(c => c.Titulo.StartsWith(prefijo) && c.IdAutor==idAutor);
            var n = listaAutores.Count();
            return Json(listaAutores, JsonRequestBehavior.AllowGet);
        }



        // GET: Registro/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Registro/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Registro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Registro/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
