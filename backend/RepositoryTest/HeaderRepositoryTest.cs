using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using RepositorySQLServer;
using System;

namespace RepositoryTest
{
    [TestClass]
    public class HeaderRepositoryTest
    {
        DatabaseContext context = new DatabaseContext();

        IUserRepository UserRepository = UserRepositorySQLServer.GetInstance();
        IDocumentRepository DocumentRepository = DocumentRepositorySQLServer.GetInstance();
        IHeaderRepository HeaderRepository = HeaderRepositorySQLServer.GetInstance();
        IContentRepository ContentRepository = ContentRepositorySQLServer.GetInstance();
        IStyleClassRepository StyleClassRepository = StyleClassRepositorySQLServer.GetInstance();

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestHeaderIsUpdatedCorrectlyFromDatabase()
        {
            User userH1 = new User
            {
                UserName = "usernameH1",
                Password = "passwordH1",
                Name = "nameH1",
                LastName = "lastnameH1",
                Email = "emailH1",
                Administrator = true
            };

            UserRepository.Add(userH1);

            StyleClass StyleClassH1 = new StyleClass
            {
                Name = "StyleClassH1",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClassH1);

            StyleClass StyleClassH2 = new StyleClass
            {
                Name = "StyleClassH2",
                BasedOn = null
            };

            StyleClassRepository.Add(StyleClassH2);

            Document document = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title1",
                Creator = userH1,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = StyleClassH1
            };

            DocumentRepository.Add(document);

            Content content = new Content
            {
                Id = Guid.NewGuid(),
            };

            ContentRepository.Add(content);

            Header header = new Header
            {
                Id = Guid.NewGuid(),
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = StyleClassH1
            };

            HeaderRepository.Add(header);

            Header updatedHeader = new Header
            {
                Id = header.Id,
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = StyleClassH2
            };

            HeaderRepository.Update(updatedHeader);

            Header fromDatabase = HeaderRepository.GetById(updatedHeader.Id);

            StyleClassRepository.Delete(StyleClassH1.Name);
            StyleClassRepository.Delete(StyleClassH2.Name);
            DocumentRepository.Delete(document.Id);
            UserRepository.Delete(userH1.Email);
            ContentRepository.Delete(content);

            Assert.AreEqual(updatedHeader.Id, fromDatabase.Id);
            Assert.AreEqual(updatedHeader.Content.Id, fromDatabase.Content.Id);
            Assert.AreEqual(updatedHeader.StyleClass.Name, fromDatabase.StyleClass.Name);
            Assert.AreEqual(updatedHeader.DocumentThatBelongs.Id, fromDatabase.DocumentThatBelongs.Id);
        }

        [TestMethod]
        public void TestHeaderExistsFromDatabase()
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

            Header header = new Header
            {
                Id = Guid.NewGuid(),
                Content = content,
                DocumentThatBelongs = document,
                StyleClass = null
            };

            HeaderRepository.Add(header);

            bool exists = HeaderRepository.Exists(header.Id);

            DocumentRepository.Delete(document.Id);
            UserRepository.Delete(user.Email);
            ContentRepository.Delete(content);

            Assert.IsTrue(exists);
        }
    }
}