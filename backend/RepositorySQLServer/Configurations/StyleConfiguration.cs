using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class StyleConfiguration : EntityTypeConfiguration<Style>
    {
        public StyleConfiguration()
        {
            this.HasKey(s => s.Id);

            this.HasRequired(s => s.Format);

            this.HasRequired(s => s.StyleClass);

            this.Property(s => s.Key)
                .IsRequired();

            this.Property(s => s.Value)
                .IsRequired();

            this.ToTable("Style");
        }
    }
}
