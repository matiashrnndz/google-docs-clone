using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.Text)
                .IsRequired();

            this.Property(c => c.Rating)
                .IsRequired();

            this.HasOptional(c => c.Commenter);

            this.HasRequired(c => c.Document);

            this.ToTable("Comment");
        }
    }
}