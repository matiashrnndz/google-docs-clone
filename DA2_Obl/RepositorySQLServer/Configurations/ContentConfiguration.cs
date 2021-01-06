using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class ContentConfiguration : EntityTypeConfiguration<Content>
    {
        public ContentConfiguration()
        {
            this.HasKey(c => c.Id);

            this.ToTable("Content");
        }
    }
}
