using Domain;
using Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Service;
using ServiceImp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class DocumentCreationByUserGraphServiceTest
    {
        [TestMethod]
        public void TestGetDocumentCreationByUserGraphReturnsValidData()
        {
            IEnumerable<User> fakeUsers = GetFakeUsers();
            IEnumerable<Document> fakeDocuments = GetFakeDocuments(fakeUsers);
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentCreationByUserGraphService documentCreationByUserGraphLogic = new DocumentCreationByUserGraphService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            mockUserRepository
                .Setup(wl => wl.GetAll())
                .Returns(fakeUsers);
            foreach (User fakeUser in fakeUsers) {
                mockDocumentRepository
                    .Setup(wl => wl.GetAllByUser(fakeUser.Email))
                    .Returns(fakeDocuments.Where(x => x.Creator.Email.Equals(fakeUser.Email)));
            }
       
            IEnumerable<Tuple<string, int>> result1 = documentCreationByUserGraphLogic.GetDocumentByUserCreationGraph(DateTime.Parse("2000-1-1"),DateTime.Now);
            IEnumerable<Tuple<string, int>> result2 = documentCreationByUserGraphLogic.GetDocumentByUserCreationGraph(DateTime.Parse("2015-1-1"), DateTime.Now);
            IEnumerable<Tuple<string, int>> result3 = documentCreationByUserGraphLogic.GetDocumentByUserCreationGraph(DateTime.Parse("1996-1-1"), DateTime.Parse("2012-6-6"));

            mockUserRepository.VerifyAll();
            mockDocumentRepository.VerifyAll();

            Assert.AreEqual(result1.ElementAt(0).Item2,3);
            Assert.AreEqual(result2.ElementAt(0).Item2, 1);
            Assert.AreEqual(result3.ElementAt(0).Item2, 2);
        }

        private IEnumerable<User> GetFakeUsers()
        {
            List<User> fakeUsers = new List<User>
            {
                new User
                {
                    Email = "gerardo@gmail.com",
                    Name = "genardo",
                    LastName = "genardi",
                    UserName = "maestrogirardi",
                    Password = "uncleb0b0",
                    Administrator = true
                },
                new User
                {
                    Email = "benito@gmail.com",
                    Name = "benito",
                    LastName = "mussolini",
                    UserName = "ilduce",
                    Password = "elfaschista",
                    Administrator = true
                }
            };

            return fakeUsers.AsEnumerable();
        }

        private IEnumerable<Document> GetFakeDocuments(IEnumerable<User> users)
        {
            List<Document> fakeDocuments = new List<Document>();

            foreach(User user in users)
            {
                fakeDocuments.Add(new Document
                {
                    Creator = user,
                    CreationDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    StyleClass = null,
                    LastModification = DateTime.Now
                });
                fakeDocuments.Add(new Document
                {
                    Creator = user,
                    CreationDate = DateTime.Parse("2010-11-13"),
                    Id = Guid.NewGuid(),
                    StyleClass = null,
                    LastModification = DateTime.Now
                });
                fakeDocuments.Add(new Document
                {
                    Creator = user,
                    CreationDate = DateTime.Parse("2005-10-5"),
                    Id = Guid.NewGuid(),
                    StyleClass = null,
                    LastModification = DateTime.Now
                });
            }

            return fakeDocuments.AsEnumerable();
        }
    }
}
