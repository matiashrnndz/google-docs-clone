using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class FooterConfiguration : EntityTypeConfiguration<Footer>
    {
        public FooterConfiguration()
        {
            this.HasKey(p => p.Id);

            this.HasRequired(p => p.DocumentThatBelongs);

            this.HasOptional(p => p.StyleClass);

            this.HasOptional(p => p.Content);

            this.ToTable("Footer");
        }
    }
}
