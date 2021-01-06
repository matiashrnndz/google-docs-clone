using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class StyleClassConfiguration : EntityTypeConfiguration<StyleClass>
    {
        public StyleClassConfiguration()
        {
            this.HasKey(s => s.Name);

            this.HasOptional(s => s.BasedOn);

            this.ToTable("StyleClass");
        }
    }
}
