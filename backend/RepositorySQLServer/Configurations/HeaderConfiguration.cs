using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class HeaderConfiguration : EntityTypeConfiguration<Header>
    {
        public HeaderConfiguration()
        {
            this.HasKey(p => p.Id);

            this.HasRequired(p => p.DocumentThatBelongs);

            this.HasOptional(p => p.Content);

            this.HasOptional(p => p.StyleClass);

            this.ToTable("Header");
        }
    }
}
