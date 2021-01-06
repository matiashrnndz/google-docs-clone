using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class LoggedEntryConfiguration : EntityTypeConfiguration<LoggedEntry>
    {
        public LoggedEntryConfiguration()
        {
            this.HasKey(le => le.Id);

            this.Property(le => le.LoggedUser)
                .IsRequired();

            this.Property(le => le.TypeOfEntry)
                .IsRequired();

            this.Property(le => le.DateOfRegistration)
                .IsRequired();

            this.ToTable("LoggedEntry");
        }
    }
}
