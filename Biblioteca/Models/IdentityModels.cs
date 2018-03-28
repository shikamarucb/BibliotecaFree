using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Biblioteca.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser
    {
        [ScriptIgnore]
        public virtual List<Prestamo> Prestamos { get; set; }

        [Display(Name="Documento de Identidad")]
        public long Dni { get; set; }

        [Display(Name="Nombre")]
        public string Name { get; set; }

        [Display(Name="Apellido")]
        public string LastName { get; set; }

        [Display(Name ="Direccion de Residencia")]
        public string Address { get; set; }

        [Index(IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Código de Socio")]
        public int PartnerCode { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Name", Name));
            return userIdentity;
        }
    }



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Volumen> Volumenes { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        public ApplicationDbContext()
            : base("BibliotecaCS", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }

    [MetadataType(typeof(IdentityUserHelper))]
    public partial class ApplicationUser { }

    public class IdentityUserHelper
    {
        [Display(Name = "Correo Electrónico")]
        public virtual string UserName { get; set; }

        [Display(Name = "Número de Teléfono")]
        public virtual string PhoneNumber { get; set; }

        [Display(Name ="Contraseña")]
        [Required]
        [DataType(DataType.Password)]
        public virtual string PasswordHash { get; set; }
    }
}