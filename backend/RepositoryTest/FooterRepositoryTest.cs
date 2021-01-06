using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using RepositorySQLServer;
using System;

namespace RepositoryTest
{
    [TestClass]
    public class FooterRepositoryTest
    {
        DatabaseContext context = new DatabaseContext();

        IUserRepository UserRepository = UserRepositorySQLServer.GetInstance();
        IDocumentRepository DocumentRepository = DocumentRepositorySQLServer.GetInstance();
        IFooterRepository FooterRepository = FooterRepositorySQLServer.GetInstance();
        IContentRepository ContentRepository = ContentRepositorySQLServer.GetInstance();
        IStyleClassRepository StyleClassRepository = StyleClassRepositorySQLServer.GetInstance();

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestFooterIsUpdatedCorrectlyFromDatabase()
        {
            User user = new User
            {
                UserName = "username",
                Password = "password",
                Name = "name",
                LastName = "lastname",
                Email = Guid.NewGuid().ToString(),
                Administrator = true
            };

            UserRepository.Add(user);

            StyleClass StyleClassF1 = new StyleClass
            {
                Name = "StyleClassF1",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClassF1);

            Document document = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title1",
                Creator = user,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = StyleClassF1
            };

            DocumentRepository.Add(document);

            Content content = new Content
            {
                Id = Guid.NewGuid(),
            };

            ContentRepository.Add(content);

            Footer footer = new Footer
            {
                Id = Guid.NewGuid(),
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = StyleClassF1
            };

            FooterRepository.Add(footer);

            Footer updatedFooter = new Footer
            {
                Id = footer.Id,
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = null
            };

            FooterRepository.Update(updatedFooter);

            Footer fromDatabase = FooterRepository.GetById(updatedFooter.Id);

            StyleClassRepository.Delete(StyleClassF1.Name);
            DocumentRepository.Delete(document.Id);
            UserRepository.Delete(user.Email);
            ContentRepository.Delete(content);

            Assert.AreEqual(updatedFooter.Id, fromDatabase.Id);
            Assert.AreEqual(updatedFooter.Content.Id, fromDatabase.Content.Id);
            Assert.IsNull(updatedFooter.StyleClass);
            Assert.AreEqual(updatedFooter.DocumentThatBelongs.Id, fromDatabase.DocumentThatBelongs.Id);
        }

        [TestMethod]
        public void TestFooterExistsFromDatabase()
        {
            User user = new User
            {
                UserName = "username",
                Password = "password",
                Name = "name",
                LastName = "lastname",
                Email = Guid.NewGuid().ToString(),
                Administrator = true
            };

            UserRepository.Add(user);

            Document document = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title1",
                Creator = user,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = null
            };

            DocumentRepository.Add(document);

            Content content = new Content
            {
                Id = Guid.NewGuid(),
            };

            ContentRepository.Add(content);

            Footer footer = new Footer
            {
                Id = Guid.NewGuid(),
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = null
            };

            FooterRepository.Add(footer);

            bool exists = FooterRepository.Exists(footer.Id);

            DocumentRepository.Delete(document.Id);
            UserRepository.Delete(user.Email);
            ContentRepository.Delete(content);

            Assert.IsTrue(exists);
        }
    }
}