using System.Data.Entity;
using CarreraCaballos.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarreraCaballos.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Apuesta> Apuestas { get; set; }
        public DbSet<Caballo> Caballos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Posicion> Posiciones { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}