using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class SessionConfiguration : EntityTypeConfiguration<Session>
    {
        public SessionConfiguration()
        {
            this.HasKey(s => s.Token);

            this.HasRequired(s => s.User);

            this.ToTable("Session");
        }
    }
}
