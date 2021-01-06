using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using RepositorySQLServer;
using System;

namespace RepositoryTest
{
    [TestClass]
    public class DocumentRepositoryTest
    {
        DatabaseContext context = new DatabaseContext();

        IUserRepository UserRepository = UserRepositorySQLServer.GetInstance();
        IDocumentRepository DocumentRepository = DocumentRepositorySQLServer.GetInstance();
        IHeaderRepository HeaderRepository = HeaderRepositorySQLServer.GetInstance();
        IParagraphRepository ParagraphRepository = ParagraphRepositorySQL.GetInstance();
        IFooterRepository FooterRepository = FooterRepositorySQLServer.GetInstance();
        IStyleClassRepository StyleClassRepository = StyleClassRepositorySQLServer.GetInstance();

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestDocumentIsUpdatedCorrectlyFromDatabase()
        {
            User user = new User
            {
                UserName = "username",
                Password = "password",
                Name = "name",
                LastName = "lastname",
                Email = "email",
                Administrator = true
            };

            UserRepository.Add(user);

            StyleClass StyleClass1 = new StyleClass
            {
                Name = "StyleClass1",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClass1);

            StyleClass StyleClass2 = new StyleClass
            {
                Name = "StyleClass2",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClass2);

            Guid documentId = Guid.NewGuid();

            Document document = new Document
            {
                Id = documentId,
                Title = "title1",
                Creator = user,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = StyleClass1
            };

            DocumentRepository.Add(document);

            Document updatedDocument = new Document
            {
                Id = documentId,
                Title = "UpdatedTitle",
                Creator = user,
                CreationDate = document.CreationDate,
                LastModification = DateTime.Now,
                StyleClass = StyleClass2
            };

            DocumentRepository.Update(documentId, updatedDocument);

            Document fromDatabase = DocumentRepository.GetById(documentId);

            StyleClassRepository.Delete(StyleClass1.Name);
            StyleClassRepository.Delete(StyleClass2.Name);
            DocumentRepository.Delete(documentId);
            UserRepository.Delete(user.Email);

            Assert.AreEqual(updatedDocument.Id, fromDatabase.Id);
            Assert.AreEqual(updatedDocument.Title, fromDatabase.Title);
            Assert.AreEqual(updatedDocument.Creator.Email, fromDatabase.Creator.Email);
            Assert.AreEqual(updatedDocument.StyleClass.Name, fromDatabase.StyleClass.Name);
        }

        [TestMethod]
        public void TestDocumentExistsFromDatabase()
        {
            User user2 = new User
            {
                UserName = "username2",
                Password = "password2",
                Name = "name2",
                LastName = "lastname2",
                Email = "email2",
                Administrator = true
            };

            UserRepository.Add(user2);

            Document document = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title1",
                Creator = user2,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = null
            };

            DocumentRepository.Add(document);

            bool exists = DocumentRepository.Exists(document.Id);

            Assert.IsTrue(exists);

            UserRepository.Delete(user2.Email);
        }

        [TestMethod]
        public void TestDocumentDeleteWithOneHeaderOneFooterOneParagraphFromDatabase()
        {
            User user4 = new User
            {
                UserName = "username3",
                Password = "password3",
                Name = "name4",
                LastName = "lastname3",
                Email = "email3",
                Administrator = true
            };

            UserRepository.Add(user4);

            Document document = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Creator = user4,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = null
            };

            DocumentRepository.Add(document);

            Header header = new Header
            {
                Id = Guid.NewGuid(),
                Content = null,
                DocumentThatBelongs = document,
                StyleClass = null
            };

            HeaderRepository.Add(header);

            Footer footer = new Footer
            {
                Id = Guid.NewGuid(),
                Content = null,
                DocumentThatBelongs = document,
                StyleClass = null
            };

            FooterRepository.Add(footer);

            Paragraph paragraph1 = new Paragraph
            {
                Id = Guid.NewGuid(),
                Content = null,
                DocumentThatBelongs = document,
                StyleClass = null,
                Position = 0
            };

            ParagraphRepository.Add(paragraph1);

            Paragraph paragraph2 = new Paragraph
            {
                Id = Guid.NewGuid(),
                Content = null,
                DocumentThatBelongs = document,
                StyleClass = null,
                Position = 1
            };

            ParagraphRepository.Add(paragraph2);

            DocumentRepository.Delete(document.Id);
            UserRepository.Delete(user4.Email);

            bool headerExistsAfterDelete = HeaderRepository.Exists(header.Id);
            bool paragraph1ExistsAfterDelete = ParagraphRepository.Exists(paragraph1.Id);
            bool paragraph2ExistsAfterDelete = ParagraphRepository.Exists(paragraph2.Id);
            bool footerExistsAfterDelete = FooterRepository.Exists(footer.Id);

            Assert.IsFalse(headerExistsAfterDelete);
            Assert.IsFalse(paragraph1ExistsAfterDelete);
            Assert.IsFalse(paragraph2ExistsAfterDelete);
            Assert.IsFalse(footerExistsAfterDelete);
        }
    }
}