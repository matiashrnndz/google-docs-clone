namespace RepositorySQLServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        Rating = c.Int(nullable: false),
                        Commenter_Email = c.String(maxLength: 128),
                        Document_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Commenter_Email)
                .ForeignKey("dbo.Document", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Commenter_Email)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Administrator = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModification = c.DateTime(nullable: false),
                        Creator_Email = c.String(nullable: false, maxLength: 128),
                        StyleClass_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Creator_Email, cascadeDelete: true)
                .ForeignKey("dbo.StyleClass", t => t.StyleClass_Name)
                .Index(t => t.Creator_Email)
                .Index(t => t.StyleClass_Name);
            
            CreateTable(
                "dbo.StyleClass",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        BasedOn_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.StyleClass", t => t.BasedOn_Name)
                .Index(t => t.BasedOn_Name);
            
            CreateTable(
                "dbo.Content",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DocumentModificationLog",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOfModification = c.DateTime(nullable: false),
                        Document_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Document", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.Footer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content_Id = c.Guid(),
                        DocumentThatBelongs_Id = c.Guid(nullable: false),
                        StyleClass_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Content", t => t.Content_Id)
                .ForeignKey("dbo.Document", t => t.DocumentThatBelongs_Id, cascadeDelete: true)
                .ForeignKey("dbo.StyleClass", t => t.StyleClass_Name)
                .Index(t => t.Content_Id)
                .Index(t => t.DocumentThatBelongs_Id)
                .Index(t => t.StyleClass_Name);
            
            CreateTable(
                "dbo.Format",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.FriendRequest",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                        Receiver_Email = c.String(maxLength: 128),
                        Sender_Email = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.Receiver_Email)
                .ForeignKey("dbo.User", t => t.Sender_Email, cascadeDelete: true)
                .Index(t => t.Receiver_Email)
                .Index(t => t.Sender_Email);
            
            CreateTable(
                "dbo.Header",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content_Id = c.Guid(),
                        DocumentThatBelongs_Id = c.Guid(nullable: false),
                        StyleClass_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Content", t => t.Content_Id)
                .ForeignKey("dbo.Document", t => t.DocumentThatBelongs_Id, cascadeDelete: true)
                .ForeignKey("dbo.StyleClass", t => t.StyleClass_Name)
                .Index(t => t.Content_Id)
                .Index(t => t.DocumentThatBelongs_Id)
                .Index(t => t.StyleClass_Name);
            
            CreateTable(
                "dbo.LoggedEntry",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeOfEntry = c.String(nullable: false),
                        loggedUser = c.String(nullable: false),
                        dateOfRegistration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Paragraph",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Position = c.Int(nullable: false),
                        Content_Id = c.Guid(),
                        DocumentThatBelongs_Id = c.Guid(nullable: false),
                        StyleClass_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Content", t => t.Content_Id)
                .ForeignKey("dbo.Document", t => t.DocumentThatBelongs_Id, cascadeDelete: true)
                .ForeignKey("dbo.StyleClass", t => t.StyleClass_Name)
                .Index(t => t.Content_Id)
                .Index(t => t.DocumentThatBelongs_Id)
                .Index(t => t.StyleClass_Name);
            
            CreateTable(
                "dbo.Session",
                c => new
                    {
                        Token = c.Guid(nullable: false),
                        User_Email = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Token)
                .ForeignKey("dbo.User", t => t.User_Email, cascadeDelete: true)
                .Index(t => t.User_Email);
            
            CreateTable(
                "dbo.Style",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(nullable: false),
                        Value = c.String(nullable: false),
                        Format_Name = c.String(nullable: false, maxLength: 128),
                        StyleClass_Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Format", t => t.Format_Name, cascadeDelete: true)
                .ForeignKey("dbo.StyleClass", t => t.StyleClass_Name, cascadeDelete: true)
                .Index(t => t.Format_Name)
                .Index(t => t.StyleClass_Name);
            
            CreateTable(
                "dbo.Text",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TextContent = c.String(nullable: false),
                        Position = c.Int(nullable: false),
                        ContentThatBelongs_Id = c.Guid(nullable: false),
                        StyleClass_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Content", t => t.ContentThatBelongs_Id, cascadeDelete: true)
                .ForeignKey("dbo.StyleClass", t => t.StyleClass_Name)
                .Index(t => t.ContentThatBelongs_Id)
                .Index(t => t.StyleClass_Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Text", "StyleClass_Name", "dbo.StyleClass");
            DropForeignKey("dbo.Text", "ContentThatBelongs_Id", "dbo.Content");
            DropForeignKey("dbo.Style", "StyleClass_Name", "dbo.StyleClass");
            DropForeignKey("dbo.Style", "Format_Name", "dbo.Format");
            DropForeignKey("dbo.Session", "User_Email", "dbo.User");
            DropForeignKey("dbo.Paragraph", "StyleClass_Name", "dbo.StyleClass");
            DropForeignKey("dbo.Paragraph", "DocumentThatBelongs_Id", "dbo.Document");
            DropForeignKey("dbo.Paragraph", "Content_Id", "dbo.Content");
            DropForeignKey("dbo.Header", "StyleClass_Name", "dbo.StyleClass");
            DropForeignKey("dbo.Header", "DocumentThatBelongs_Id", "dbo.Document");
            DropForeignKey("dbo.Header", "Content_Id", "dbo.Content");
            DropForeignKey("dbo.FriendRequest", "Sender_Email", "dbo.User");
            DropForeignKey("dbo.FriendRequest", "Receiver_Email", "dbo.User");
            DropForeignKey("dbo.Footer", "StyleClass_Name", "dbo.StyleClass");
            DropForeignKey("dbo.Footer", "DocumentThatBelongs_Id", "dbo.Document");
            DropForeignKey("dbo.Footer", "Content_Id", "dbo.Content");
            DropForeignKey("dbo.DocumentModificationLog", "Document_Id", "dbo.Document");
            DropForeignKey("dbo.Comment", "Document_Id", "dbo.Document");
            DropForeignKey("dbo.Document", "StyleClass_Name", "dbo.StyleClass");
            DropForeignKey("dbo.StyleClass", "BasedOn_Name", "dbo.StyleClass");
            DropForeignKey("dbo.Document", "Creator_Email", "dbo.User");
            DropForeignKey("dbo.Comment", "Commenter_Email", "dbo.User");
            DropIndex("dbo.Text", new[] { "StyleClass_Name" });
            DropIndex("dbo.Text", new[] { "ContentThatBelongs_Id" });
            DropIndex("dbo.Style", new[] { "StyleClass_Name" });
            DropIndex("dbo.Style", new[] { "Format_Name" });
            DropIndex("dbo.Session", new[] { "User_Email" });
            DropIndex("dbo.Paragraph", new[] { "StyleClass_Name" });
            DropIndex("dbo.Paragraph", new[] { "DocumentThatBelongs_Id" });
            DropIndex("dbo.Paragraph", new[] { "Content_Id" });
            DropIndex("dbo.Header", new[] { "StyleClass_Name" });
            DropIndex("dbo.Header", new[] { "DocumentThatBelongs_Id" });
            DropIndex("dbo.Header", new[] { "Content_Id" });
            DropIndex("dbo.FriendRequest", new[] { "Sender_Email" });
            DropIndex("dbo.FriendRequest", new[] { "Receiver_Email" });
            DropIndex("dbo.Footer", new[] { "StyleClass_Name" });
            DropIndex("dbo.Footer", new[] { "DocumentThatBelongs_Id" });
            DropIndex("dbo.Footer", new[] { "Content_Id" });
            DropIndex("dbo.DocumentModificationLog", new[] { "Document_Id" });
            DropIndex("dbo.StyleClass", new[] { "BasedOn_Name" });
            DropIndex("dbo.Document", new[] { "StyleClass_Name" });
            DropIndex("dbo.Document", new[] { "Creator_Email" });
            DropIndex("dbo.Comment", new[] { "Document_Id" });
            DropIndex("dbo.Comment", new[] { "Commenter_Email" });
            DropTable("dbo.Text");
            DropTable("dbo.Style");
            DropTable("dbo.Session");
            DropTable("dbo.Paragraph");
            DropTable("dbo.LoggedEntry");
            DropTable("dbo.Header");
            DropTable("dbo.FriendRequest");
            DropTable("dbo.Format");
            DropTable("dbo.Footer");
            DropTable("dbo.DocumentModificationLog");
            DropTable("dbo.Content");
            DropTable("dbo.StyleClass");
            DropTable("dbo.Document");
            DropTable("dbo.User");
            DropTable("dbo.Comment");
        }
    }
}
