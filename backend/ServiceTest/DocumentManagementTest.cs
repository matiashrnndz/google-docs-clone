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
    public class DocumentManagementTest
    {
        [TestMethod]
        public void TestGetDocumentsByUserWorksOnExistingUser()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            IEnumerable<Document> fakeDocuments = GetFakeDocuments();
            string userMail = "elmarcianito100porcientorealnofake@gmail.com";

            mockUserRepository
                .Setup(wl => wl.Exists(userMail))
                .Returns(true);
            mockDocumentRepository
                .Setup(wl => wl.GetAllByUser(userMail))
                .Returns(fakeDocuments);

            IEnumerable<Document> results = documentLogic.GetAllByUser(userMail);

            mockDocumentRepository.VerifyAll();
            Assert.IsNotNull(results);
            Assert.IsTrue(fakeDocuments.SequenceEqual(results));
        }

        [TestMethod]
        [ExpectedException(typeof(MissingUserException))]
        public void TestGetDocumentsByUserThrowsExceptionOnMissingUser()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            IEnumerable<Document> fakeDocuments = GetFakeDocuments();
            string userMail = "elmarcianito100porcientorealnofake@gmail.com";

            mockUserRepository
                .Setup(wl => wl.Exists(userMail))
                .Returns(false);

            IEnumerable<Document> results = documentLogic.GetAllByUser(userMail);

            mockDocumentRepository.VerifyAll();
        }

        [TestMethod]
        public void TestGetDocumentByIdWorksOnExistingDocument()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Guid fakeId = Guid.NewGuid();
            Document fakeDocument = new Document
            {
                Id = fakeId,
                Creator = GetFakeUser(),
                StyleClass = new StyleClass()
            };

            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeId))
                .Returns(true);
            mockDocumentRepository
                .Setup(wl => wl.GetById(fakeId))
                .Returns(fakeDocument);

            Document result = documentLogic.GetById(fakeId);

            mockDocumentRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeDocument, result);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestGetDocumentByIdFailsOnMissingDocument()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Guid fakeId = Guid.NewGuid();
            Document fakeDocument = new Document
            {
                Id = fakeId,
                Creator = GetFakeUser(),
                StyleClass = new StyleClass()
            };

            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeId))
                .Returns(false);

            Document result = documentLogic.GetById(fakeId);

            mockDocumentRepository.VerifyAll();
        }

        [TestMethod]
        public void TestAddDocumentWorksOnExistingUser()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = new Document
            {
                Creator = GetFakeUser(),
                StyleClass = new StyleClass()
            };
            string userMail = "elmarcianito100porcientorealnofake@gmail.com";

            mockUserRepository
                .Setup(wl => wl.Exists(userMail))
                .Returns(true);
            mockDocumentRepository
                .Setup(wl => wl.Add(fakeDocument));
            mockStyleClassRepository
                .Setup(wl => wl.Exists(fakeDocument.StyleClass.Name))
                .Returns(true);

            Document result = documentLogic.Add(userMail, fakeDocument);

            mockDocumentRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.IsTrue(DateTime.Now > result.CreationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingUserException))]
        public void TestAddDocumentFailsOnMissingUser()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Document fakeDocument = new Document
            {
                Creator = GetFakeUser(),
                StyleClass = new StyleClass()
            };
            string userMail = "elmarcianito100porcientorealnofake@gmail.com";

            mockUserRepository
                .Setup(wl => wl.Exists(userMail))
                .Returns(false);

            Document result = documentLogic.Add(userMail, fakeDocument);

            mockDocumentRepository.VerifyAll();           
        }

        [TestMethod]
        public void TestUpdateDocumentWorksOnExistingDocument()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Guid fakeId = Guid.NewGuid();
            Document fakeDocument = new Document
            {
                Id = fakeId,
                Creator = GetFakeUser(),
                CreationDate = DateTime.Now.AddDays(-1),
                StyleClass = new StyleClass()
            };

            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeId))
                .Returns(true);
            mockDocumentRepository
                .Setup(wl => wl.Update(fakeId, fakeDocument));
            mockStyleClassRepository
              .Setup(wl => wl.Exists(fakeDocument.StyleClass.Name))
              .Returns(true);
            mockDocumentRepository
                .Setup(wl => wl.GetById(fakeId))
                .Returns(fakeDocument);

            documentLogic.Update(fakeId, fakeDocument);

            mockDocumentRepository.VerifyAll();
            Assert.AreNotEqual(fakeDocument.CreationDate, fakeDocument.LastModification);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestUpdateDocumentFailsOnMissingDocument()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Guid fakeId = Guid.NewGuid();
            Document fakeDocument = new Document
            {
                Id = fakeId,
                Creator = GetFakeUser(),
                CreationDate = DateTime.Now.AddDays(-1),
                StyleClass = new StyleClass()
            };

            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeId))
                .Returns(false);
            

            documentLogic.Update(fakeId, fakeDocument);

            mockDocumentRepository.VerifyAll();
            
        }

        [TestMethod]
        public void TestDeleteDocumentWorksOnExistingDocument()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Guid fakeId = Guid.NewGuid();
            Document fakeDocument = new Document
            {
                Id = fakeId,
                Creator = GetFakeUser(),
                CreationDate = DateTime.Now.AddDays(-1),
                StyleClass = new StyleClass()
            };

            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeId))
                .Returns(true);
            mockDocumentRepository
                .Setup(wl => wl.Delete(fakeId));

            documentLogic.Delete(fakeId);

            mockDocumentRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestDeleteDocumentFailsOnMissingDocument()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IDocumentManagementService documentLogic = new DocumentManagementService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Guid fakeId = Guid.NewGuid();
            Document fakeDocument = new Document
            {
                Id = fakeId,
                Creator = GetFakeUser(),
                CreationDate = DateTime.Now.AddDays(-1),
                StyleClass = new StyleClass()
            };

            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeId))
                .Returns(false);

            documentLogic.Delete(fakeId);

            mockDocumentRepository.VerifyAll();
        }

        private IEnumerable<Document> GetFakeDocuments()
        {
            List<Document> fakeDocuments = new List<Document>
            {
                new Document
                {
                    Creator = GetFakeUser(),
                    Id = Guid.NewGuid(),
                    StyleClass = new StyleClass()
    },
                new Document
                {
                    Creator = GetFakeUser(),
                    Id = Guid.NewGuid(),
                    StyleClass = new StyleClass()
},
                new Document
                {
                    Creator = GetFakeUser(),
                    Id = Guid.NewGuid(),
                    StyleClass = new StyleClass()
                }
            };
            return fakeDocuments.AsEnumerable();
        }

        private User GetFakeUser()
        {
            return new User
            {
                Email = "elmarcianito100porcientorealnofake@gmail.com",
                Name = "El Hermano",
                LastName = "De Jiren",
                UserName = "vivalacumbia21",
                Password = "doniapelos",
                Administrator = true
            };
        }
    }
}

