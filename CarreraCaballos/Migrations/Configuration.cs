using CarreraCaballos.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarreraCaballos.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarreraCaballos.Database.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "CarreraCaballos.Database.ApplicationDbContext";
        }

        protected override void Seed(CarreraCaballos.Database.ApplicationDbContext context)
        {

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            if (userManager.FindByName("admin@admin.com") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "admin@admin.com",
                    UserName = "admin@admin.com",
                    EmailConfirmed = true
                };
                userManager.Create(user, "Admin1234");
                var roleManager =
                    new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                roleManager.Create(new IdentityRole("Administrador"));
                userManager.AddToRole(user.Id, "Administrador");

            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
