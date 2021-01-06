using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using RepositorySQLServer;
using System;

namespace RepositoryTest
{
    [TestClass]
    public class ParagraphRepositoryTest
    {
        DatabaseContext context = new DatabaseContext();

        IUserRepository UserRepository = UserRepositorySQLServer.GetInstance();
        IDocumentRepository DocumentRepository = DocumentRepositorySQLServer.GetInstance();
        IParagraphRepository ParagraphRepository = ParagraphRepositorySQL.GetInstance();
        IContentRepository ContentRepository = ContentRepositorySQLServer.GetInstance();
        IStyleClassRepository StyleClassRepository = StyleClassRepositorySQLServer.GetInstance();

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestParagraphIsUpdatedCorrectlyFromDatabase()
        {
            User userP1 = new User
            {
                UserName = "usernameP1",
                Password = "passwordP1",
                Name = "nameP1",
                LastName = "lastnameP1",
                Email = "emailP1",
                Administrator = true
            };

            UserRepository.Add(userP1);

            StyleClass StyleClassP1 = new StyleClass
            {
                Name = "StyleClassP1",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClassP1);

            StyleClass StyleClassP2 = new StyleClass
            {
                Name = "StyleClassP2",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClassP2);

            Document document = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title1",
                Creator = userP1,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = StyleClassP1
            };

            DocumentRepository.Add(document);

            Content content = new Content
            {
                Id = Guid.NewGuid(),
            };

            ContentRepository.Add(content);

            Paragraph paragraph1 = new Paragraph
            {
                Id = Guid.NewGuid(),
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = StyleClassP1,
                Position = 0
            };

            ParagraphRepository.Add(paragraph1);

            Paragraph updatedParagraph = new Paragraph
            {
                Id = paragraph1.Id,
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = StyleClassP2,
                Position = 1
            };

            ParagraphRepository.Update(updatedParagraph);

            Paragraph fromDatabase = ParagraphRepository.GetById(updatedParagraph.Id);

            StyleClassRepository.Delete(StyleClassP1.Name);
            StyleClassRepository.Delete(StyleClassP2.Name);
            DocumentRepository.Delete(document.Id);
            UserRepository.Delete(userP1.Email);
            ContentRepository.Delete(content);

            Assert.AreEqual(updatedParagraph.Id, fromDatabase.Id);
            Assert.AreEqual(updatedParagraph.Content.Id, fromDatabase.Content.Id);
            Assert.AreEqual(updatedParagraph.StyleClass.Name, fromDatabase.StyleClass.Name);
            Assert.AreEqual(updatedParagraph.DocumentThatBelongs.Id, fromDatabase.DocumentThatBelongs.Id);
            Assert.AreEqual(updatedParagraph.Position, fromDatabase.Position);
        }

        [TestMethod]
        public void TestParagraphsDontExistsAfterDeleteDocumentFromDatabase()
        {
            User userP1 = new User
            {
                UserName = "usernameP1",
                Password = "passwordP1",
                Name = "nameP1",
                LastName = "lastnameP1",
                Email = "emailP1",
                Administrator = true
            };

            UserRepository.Add(userP1);

            StyleClass StyleClassP1 = new StyleClass
            {
                Name = "StyleClassP1",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClassP1);

            StyleClass StyleClassP2 = new StyleClass
            {
                Name = "StyleClassP2",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClassP2);

            Document document = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title1",
                Creator = userP1,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = StyleClassP1
            };

            DocumentRepository.Add(document);

            Content content = new Content
            {
                Id = Guid.NewGuid(),
            };

            ContentRepository.Add(content);

            Paragraph paragraph1 = new Paragraph
            {
                Id = Guid.NewGuid(),
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = StyleClassP1,
                Position = 0
            };

            ParagraphRepository.Add(paragraph1);

            Paragraph paragraph2 = new Paragraph
            {
                Id = Guid.NewGuid(),
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = StyleClassP2,
                Position = 1
            };

            ParagraphRepository.Add(paragraph2);

            bool existsBeforeDeleteDocument1 = ParagraphRepository.Exists(paragraph1.Id);
            bool existsBeforeDeleteDocument2 = ParagraphRepository.Exists(paragraph2.Id);

            DocumentRepository.Delete(document.Id);

            bool existsAfterDeleteDocument1 = ParagraphRepository.Exists(paragraph1.Id);
            bool existsAftereDeleteDocument2 = ParagraphRepository.Exists(paragraph2.Id);

            StyleClassRepository.Delete(StyleClassP1.Name);
            StyleClassRepository.Delete(StyleClassP2.Name);
            UserRepository.Delete(userP1.Email);
            ContentRepository.Delete(content);

            Assert.IsTrue(existsBeforeDeleteDocument1);
            Assert.IsTrue(existsBeforeDeleteDocument2);
            Assert.IsFalse(existsAfterDeleteDocument1);
            Assert.IsFalse(existsAftereDeleteDocument2);
        }
    }
}