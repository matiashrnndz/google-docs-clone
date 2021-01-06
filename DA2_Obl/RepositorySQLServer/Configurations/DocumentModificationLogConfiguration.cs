using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class DocumentModificationLogConfiguration : EntityTypeConfiguration<DocumentModificationLog>
    {
        public DocumentModificationLogConfiguration()
        {
            this.HasKey(d => d.Id);

            this.HasRequired(d => d.Document);

            this.Property(d => d.DateOfModification)
                .IsRequired();

            this.ToTable("DocumentModificationLog");
        }
    }
}
