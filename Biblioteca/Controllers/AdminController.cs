using Biblioteca.Models;
using Biblioteca.Servicios;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    public class AdminController : Controller
    {

        #region CreateAdmin
        public ActionResult CreateAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdmin(RegisterViewModel model)
        {
            var identityResult = await new RegistroPersonas().Registrar(model, ModelState, 1);
            if (identityResult == null)
            {
                return View(model);
            }
            else if (identityResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                AddErrors(identityResult);
            }
            return View(model);
        } 
        #endregion

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}