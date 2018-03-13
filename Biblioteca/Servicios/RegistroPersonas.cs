using Biblioteca.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Biblioteca.Servicios
{
    public class RegistroPersonas
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public RegistroPersonas()
        {

        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
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
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        public async Task<IdentityResult> Registrar(RegisterViewModel model, ModelStateDictionary ModelState,
            int rol)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    LastName = model.LastName,
                    Dni = model.Dni,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //Se asigna rol al usuario
                    switch (rol)
                    {
                        case 1:
                            UserManager.AddToRole(user.Id, "SuperUsuario");
                            break;
                        case 2:
                            UserManager.AddToRole(user.Id, "Bibliotecario");
                            break;
                        default:
                            UserManager.AddToRole(user.Id, "Usuario");
                            break;
                    }
                    
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                }
                return result;
            }
            return null;
        }


    }
}