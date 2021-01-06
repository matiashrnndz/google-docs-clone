using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class ParagraphConfiguration : EntityTypeConfiguration<Paragraph>
    {
        public ParagraphConfiguration()
        {
            this.HasKey(p => p.Id);

            this.HasRequired(p => p.DocumentThatBelongs);

            this.HasOptional(p => p.StyleClass);

            this.HasOptional(p => p.Content);

            this.Property(p => p.Position)
                .IsRequired();

            this.ToTable("Paragraph");
        }
    }
}
