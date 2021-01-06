using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using RepositorySQLServer;
using System;

namespace RepositoryTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        DatabaseContext context;

        IUserRepository UserRepository = UserRepositorySQLServer.GetInstance();
        IDocumentRepository DocumentRepository = DocumentRepositorySQLServer.GetInstance();

        [TestInitialize]
        public void TestInitialize()
        {
            context = new DatabaseContext();

            context.Database.Initialize(true);
        }

        [TestMethod]
        public void TestUserIsAddedAndGottenCorrectlyFromDatabase()
        {
            User user0 = new User
            {
                UserName = "username0",
                Password = "password0",
                Name = "name0",
                LastName = "lastname0",
                Email = "email0",
                Administrator = true
            };

            UserRepository.Add(user0);

            User fromDatabase = UserRepository.GetByEmail(user0.Email);

            Assert.AreEqual(user0.Email, fromDatabase.Email);
            Assert.AreEqual(user0.UserName, fromDatabase.UserName);
            Assert.AreEqual(user0.LastName, fromDatabase.LastName);
            Assert.AreEqual(user0.Name, fromDatabase.Name);
            Assert.AreEqual(user0.LastName, fromDatabase.LastName);
            Assert.AreEqual(user0.Administrator, fromDatabase.Administrator);

            UserRepository.Delete(user0.Email);
        }

        [TestMethod]
        public void TestUserIsUpdatedCorrectlyFromDatabase()
        {
            User user1 = new User
            {
                UserName = "username1",
                Password = "password1",
                Name = "name1",
                LastName = "lastname1",
                Email = "email1",
                Administrator = true
            };

            UserRepository.Add(user1);

            User updatedUser = new User
            {
                UserName = "usernameU",
                Password = "passwordU",
                Name = "nameU",
                LastName = "lastnameU",
                Email = "email1",
                Administrator = true
            };

            UserRepository.Update(updatedUser);

            User fromDatabase = UserRepository.GetByEmail(updatedUser.Email);

            Assert.AreEqual(updatedUser.Email, fromDatabase.Email);
            Assert.AreEqual(updatedUser.UserName, fromDatabase.UserName);
            Assert.AreEqual(updatedUser.LastName, fromDatabase.LastName);
            Assert.AreEqual(updatedUser.Name, fromDatabase.Name);
            Assert.AreEqual(updatedUser.LastName, fromDatabase.LastName);
            Assert.AreEqual(updatedUser.Administrator, fromDatabase.Administrator);

            UserRepository.Delete(user1.Email);
        }

        [TestMethod]
        public void TestUserExistsFromDatabase()
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

            bool exists = UserRepository.Exists(user2.Email);

            Assert.IsTrue(exists);

            UserRepository.Delete(user2.Email);
        }

        [TestMethod]
        public void TestUserDeleteWithOneDocumentFromDatabase()
        {
            User user3 = new User
            {
                UserName = "username3",
                Password = "password3",
                Name = "name3",
                LastName = "lastname3",
                Email = "email3",
                Administrator = true
            };

            Document document = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Creator = user3,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = null
            };

            UserRepository.Add(user3);

            DocumentRepository.Add(document);

            bool existsAfterAddedDocument = DocumentRepository.Exists(document.Id);

            UserRepository.Delete(user3.Email);

            bool existsAfterDeletedUser = DocumentRepository.Exists(document.Id);

            Assert.IsTrue(existsAfterAddedDocument);
            Assert.IsFalse(existsAfterDeletedUser);
        }

        [TestMethod]
        public void TestUserDeleteWithTwoDocumentFromDatabase()
        {
            User user3 = new User
            {
                UserName = "username3",
                Password = "password3",
                Name = "name3",
                LastName = "lastname3",
                Email = "email3",
                Administrator = true
            };

            Document document1 = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Creator = user3,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = null
            };

            Document document2 = new Document
            {
                Id = Guid.NewGuid(),
                Title = "title",
                Creator = user3,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = null
            };

            UserRepository.Add(user3);

            DocumentRepository.Add(document1);
            DocumentRepository.Add(document2);

            bool existsAfterAddedDocument1 = DocumentRepository.Exists(document1.Id);
            bool existsAfterAddedDocument2 = DocumentRepository.Exists(document2.Id);

            UserRepository.Delete(user3.Email);

            bool existsAfterDeletedUser1 = DocumentRepository.Exists(document1.Id);
            bool existsAfterDeletedUser2 = DocumentRepository.Exists(document2.Id);

            Assert.IsTrue(existsAfterAddedDocument1);
            Assert.IsTrue(existsAfterAddedDocument2);
            Assert.IsFalse(existsAfterDeletedUser1);
            Assert.IsFalse(existsAfterDeletedUser2);
        }
    }
}