using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class DocumentConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            this.HasKey(d => d.Id);

            this.Property(d => d.Title);

            this.HasRequired(d => d.Creator);

            this.HasOptional(d => d.StyleClass);

            this.Property(d => d.CreationDate);

            this.Property(d => d.LastModification);

            this.ToTable("Document");
        }
    }
}
