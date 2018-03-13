using Biblioteca.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.ProgramacionInterna
{
    public class Roles
    {
        public static void CreateRole()
        {
            var db = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var result = roleManager.Create(new IdentityRole("SuperUsuario"));
            var result2 = roleManager.Create(new IdentityRole("Usuario"));
            var result3 = roleManager.Create(new IdentityRole("Bibliotecario"));
        }
    }
}