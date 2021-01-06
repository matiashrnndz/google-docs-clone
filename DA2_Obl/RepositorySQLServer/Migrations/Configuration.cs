namespace RepositorySQLServer.Migrations
{
    using Domain;
    using System;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<RepositorySQLServer.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RepositorySQLServer.DatabaseContext context)
        {
            User admin = new User
            {
                Email = "admin@admin.com",
                Password = "admin",
                Name = "admin",
                LastName = "Admin",
                UserName = "Admin",
                Administrator = true
            };

            context.Users.AddOrUpdate(admin);

            Session adminSession = new Session
            {
                Token = Guid.Empty,
                User = admin,
            };

            context.Sessions.AddOrUpdate(adminSession);

            context.SaveChanges();
        }
    }
}
