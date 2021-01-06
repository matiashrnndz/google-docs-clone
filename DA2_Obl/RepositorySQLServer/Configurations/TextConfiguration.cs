using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class TextConfiguration : EntityTypeConfiguration<Text>
    {
        public TextConfiguration()
        {
            this.HasKey(t => t.Id);

            this.HasOptional(t => t.StyleClass);

            this.HasRequired(t => t.ContentThatBelongs);

            this.Property(t => t.TextContent)
                .IsRequired();

            this.Property(t => t.Position)
                .IsRequired();

            this.ToTable("Text");
        }
    }
}
