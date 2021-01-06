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
    public class DocumentModificationByUserGraphServiceTest
    {
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IDocumentRepository> mockDocumentRepository;
        private Mock<IDocumentModificationLogRepository> mockLoggingRepository;
        IDocumentModificationByUserGraphService modificationGraphLogic;

        public DocumentModificationByUserGraphServiceTest()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockDocumentRepository = new Mock<IDocumentRepository>();
            mockLoggingRepository = new Mock<IDocumentModificationLogRepository>();
            modificationGraphLogic = new DocumentModificationByUserGraphService()
            {
                UserRepository = mockUserRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                DocumentModificationLogRepository = mockLoggingRepository.Object
            };
        }

        [TestMethod]
        public void TestGetModificationsPerUserPerDayReturnsValidGraph()
        {
            User fakeUser = GetFakeUser();
            List<Document> fakeDocuments = new List<Document>();
            fakeDocuments.Add(GetFakeDocument(fakeUser));
            fakeDocuments.Add(GetFakeDocument(fakeUser));
            IEnumerable<Document> trueFakeDocuments = fakeDocuments.AsEnumerable();
            IEnumerable<DocumentModificationLog> fakeModifications = GetFakeModifications(trueFakeDocuments.ElementAt(0));
            IEnumerable<DocumentModificationLog> fakeModifications2 = new List<DocumentModificationLog>().AsEnumerable();

            mockDocumentRepository
                .Setup(wl => wl.GetAllByUser(fakeUser.Email))
                .Returns(trueFakeDocuments);
            mockLoggingRepository
                .Setup(wl => wl.GetAllByDocument(trueFakeDocuments.ElementAt(0).Id))
                .Returns(fakeModifications);
            mockLoggingRepository
                .Setup(wl => wl.GetAllByDocument(trueFakeDocuments.ElementAt(1).Id))
                .Returns(fakeModifications2);

            IEnumerable<Tuple<DateTime, int>> result1 = modificationGraphLogic.GetModificationsPerUserPerDay(fakeUser, DateTime.Now.AddDays(-40), DateTime.Now.AddDays(20));

            Assert.IsNotNull(result1);
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

        private Document GetFakeDocument(User fakeUser)
        {
            return new Document
            {
                Id = Guid.NewGuid(),
                Creator = fakeUser,
                LastModification = DateTime.Now,
                CreationDate = DateTime.Now,
                Title = "Algo",
                StyleClass = new StyleClass()
            };
        }

        private IEnumerable<DocumentModificationLog> GetFakeModifications(Document fakeDocument)
        {

            return new List<DocumentModificationLog>
            {
                new DocumentModificationLog
                {
                    DateOfModification = DateTime.Now,
                    Document = fakeDocument,
                    Id = Guid.NewGuid()
                },
                new DocumentModificationLog
                {
                    DateOfModification = DateTime.Now.AddDays(-10),
                    Document = fakeDocument,
                    Id = Guid.NewGuid()
                },
                new DocumentModificationLog
                {
                    DateOfModification = DateTime.Now.AddDays(-20),
                    Document = fakeDocument,
                    Id = Guid.NewGuid()
                },
                new DocumentModificationLog
                {
                    DateOfModification = DateTime.Now.AddDays(10),
                    Document = fakeDocument,
                    Id = Guid.NewGuid()
                }
            }.AsEnumerable(); 
        }
    }
}
