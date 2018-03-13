using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Biblioteca.ProgramacionInterna;

namespace Biblioteca.Controllers
{
    public class DBaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DBase
        public ActionResult Index()
        {
            try
            {
                Autor aut = new Autor();
                for (int i = 0; i < 1000000; i++)
                {
                    using(MD5 md5Hash = MD5.Create())
                    {
                        string hash = GetMd5Hash(md5Hash, Guid.NewGuid().ToString());
                        aut.Nombre = hash;
                        aut.Nacimiento = new DateTime(1990, 1, 1);
                        db.Autores.Add(aut);
                        db.SaveChanges();
                    };
                    
                }
            }catch(Exception e)
            {

            }
            return View();
        }

        public ActionResult CreateRole()
        {
            Roles.CreateRole();
            return RedirectToAction("Index","Home");
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}