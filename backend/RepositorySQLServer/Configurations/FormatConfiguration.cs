using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class FormatConfiguration : EntityTypeConfiguration<Format>
    {
        public FormatConfiguration()
        {
            this.HasKey(f => f.Name);

            this.ToTable("Format");
        }
    }
}
