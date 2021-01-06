using Domain;
using RepositorySQLServer.Configurations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RepositorySQLServer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentModificationLog> DocumentModificationLogs { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<StyleClass> StyleClasses { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LoggedEntry> LoggedEntries { get; set; }


        public DatabaseContext() : base("name=DatabaseContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new DocumentConfiguration());
            modelBuilder.Configurations.Add(new DocumentModificationLogConfiguration());
            modelBuilder.Configurations.Add(new ContentConfiguration());
            modelBuilder.Configurations.Add(new FormatConfiguration());
            modelBuilder.Configurations.Add(new HeaderConfiguration());
            modelBuilder.Configurations.Add(new ParagraphConfiguration());
            modelBuilder.Configurations.Add(new FooterConfiguration());
            modelBuilder.Configurations.Add(new StyleClassConfiguration());
            modelBuilder.Configurations.Add(new StyleConfiguration());
            modelBuilder.Configurations.Add(new TextConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new SessionConfiguration());
            modelBuilder.Configurations.Add(new FriendRequestsConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new LoggedEntryConfiguration());
        }
    }
}