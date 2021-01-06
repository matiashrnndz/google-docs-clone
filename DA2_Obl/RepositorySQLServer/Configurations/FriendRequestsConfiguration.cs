using Domain;
using System.Data.Entity.ModelConfiguration;

namespace RepositorySQLServer.Configurations
{
    internal class FriendRequestsConfiguration : EntityTypeConfiguration<FriendRequest>
    {
        public FriendRequestsConfiguration()
        {
            this.HasKey(r => r.Id);

            this.HasRequired(r => r.Sender);

            this.HasOptional(r => r.Receiver);

            this.Property(r => r.Accepted);

            this.ToTable("FriendRequest");
        }
    }
}
