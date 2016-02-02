namespace Sabio.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sabio.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private static void CreateRole(ApplicationDbContext context, string roleName)
        {
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = roleName };

                manager.Create(role);
            }
        }

        protected override void Seed(Sabio.Web.Models.ApplicationDbContext context)
        {
            CreateRole(context, "Admin");
            CreateRole(context, "SuperAdmin");

            CreateRole(context, "Instructor");
            CreateRole(context, "Blogger");




            if (!context.Users.Any(u => u.UserName == "c11@sabiofellows.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "c11@sabiofellows.com",
                    Email = "c11@sabiofellows.com",
                    LockoutEnabled = false
                };

                manager.Create(user, "Sabiopass1!");
                manager.AddToRole(user.Id, "Admin");
                manager.AddToRole(user.Id, "SuperAdmin");


            }


            if (!context.Users.Any(u => u.UserName == "gregorio@sabio.la"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "gregorio@sabio.la",
                    Email = "gregorio@sabio.la",
                    LockoutEnabled = false
                };

                manager.Create(user, "Sabiopass1!");
                manager.AddToRole(user.Id, "Admin");
                manager.AddToRole(user.Id, "SuperAdmin");


            }

            if (!context.Users.Any(u => u.UserName == "john@sabio.la"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "john@sabio.la",
                    Email = "john@sabio.la",
                    LockoutEnabled = false
                };

                manager.Create(user, "Sabiopass1!");
                manager.AddToRole(user.Id, "Admin");
                manager.AddToRole(user.Id, "SuperAdmin");


            }



        }
}
}
