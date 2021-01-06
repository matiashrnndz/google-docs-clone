using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(u => u.Email);

            this.Property(u => u.UserName)
                .IsRequired();

            this.Property(u => u.Password)
                .IsRequired();

            this.Property(u => u.Name)
                .IsRequired();

            this.Property(u => u.LastName)
                .IsRequired();

            this.Property(u => u.Administrator)
                .IsRequired();

            this.ToTable("User");
        }
    }
}
