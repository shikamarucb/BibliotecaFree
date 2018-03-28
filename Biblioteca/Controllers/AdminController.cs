using Biblioteca.Models;
using Biblioteca.Servicios;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Controllers
{
    [Authorize(Roles = "SuperUsuario")]
    public class AdminController : Controller
    {
        #region temporal
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

        private ApplicationDbContext db = new ApplicationDbContext();

        #region GetAdmin
        public ActionResult GetAdmin()
        {
            var rolid = db.Roles.Where(x => x.Name == "SuperUsuario").Select(y => y.Id).FirstOrDefault();

            var usuarios = db.Users.Where(x => x.Roles.Any(y => y.RoleId == rolid))
                .Select(usuario => new 
                {
                    Id=usuario.Id,
                    Dni = usuario.Dni,
                    Name = usuario.Name,
                    LastName = usuario.LastName,
                    UserName = usuario.UserName,
                    PartnerCode = usuario.PartnerCode,
                    Address = usuario.Address,
                    PhoneNumber = usuario.PhoneNumber

                }).AsEnumerable()
                .Select(z => new ApplicationUser
                {
                    Id=z.Id,
                    Dni = z.Dni,
                    Name = z.Name,
                    LastName = z.LastName,
                    UserName = z.UserName,
                    PartnerCode = z.PartnerCode,
                    Address = z.Address,
                    PhoneNumber = z.PhoneNumber
                }).ToList();
            

            return View(usuarios);
        }
        #endregion

        #region CreateAdmin
        public ActionResult CreateAdmin(string result)
        {
            ViewBag.msj = result;
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
                return RedirectToAction("CreateAdmin", new { result = "nk}*v%7t?P764hgPgqqvq:T,-:{Hc" });
            }
            else
            {
                AddErrors(identityResult);
            }
            return View(model);
        }
        #endregion

        #region EditAdmin
        public ActionResult EditAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            user.PasswordHash = "";
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult EditAdmin(ApplicationUser model)
        {
            //DOS FORMAS DE HACERLO, LA QUE ESTA SIN COMENTAR, Y LA QUE ESTA CON COMENTARIOS

            var user = UserManager.FindById(model.Id);
            var pass = UserManager.PasswordHasher.HashPassword(model.PasswordHash);
            user.PasswordHash = pass;
            UserManager.Update(user);

            //db.Users.Attach(model);
            //model.PasswordHash = UserManager.PasswordHasher.HashPassword(model.PasswordHash);
            //db.SaveChanges();

            return View();
        }
        #endregion

        #region CreateLibrarian
        public ActionResult CreateLibrarian()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateLibrarian(RegisterViewModel model)
        {
            var identityResult = await new RegistroPersonas().Registrar(model, ModelState, 2);
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