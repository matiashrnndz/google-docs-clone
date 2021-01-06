using Domain;
using Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using Service;
using ServiceImp;
using System;


namespace ServiceTest
{
    [TestClass]
    public class HeaderManagementTest
    {
        [TestMethod]
        public void TestGetByDocumentReturnsHeaderOnExistingHeaderAndDocument()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockHeaderRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(true);
            mockHeaderRepository
                .Setup(wl => wl.GetByDocument(fakeDocument.Id))
                .Returns(fakeHeader);

            Header result = headerLogic.GetByDocument(fakeDocument.Id);

            mockDocumentRepository.VerifyAll();
            mockHeaderRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, fakeHeader);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestGetByDocumentFailsOnMissingDocument()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();

            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);



            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(false);
            Header result = headerLogic.GetByDocument(fakeDocument.Id);

            mockDocumentRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingHeaderException))]
        public void TestGetByDocumentFailsOnMissingHeader()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockHeaderRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(false);

            Header result = headerLogic.GetByDocument(fakeDocument.Id);

            mockDocumentRepository.VerifyAll();
            mockHeaderRepository.VerifyAll();
        }

        [TestMethod]
        public void TestAddHeaderWorksOnMissingHeaderAndExistingDocument()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockHeaderRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(false);
            mockHeaderRepository
                .Setup(wl => wl.Add(fakeHeader));
            mockDocumentRepository
                .Setup(wl => wl.GetById(fakeDocument.Id))
                .Returns(fakeDocument);

            Header result = headerLogic.Add(fakeDocument.Id, fakeHeader);

            mockDocumentRepository.VerifyAll();
            mockHeaderRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, fakeHeader);
        }

        [TestMethod]
        [ExpectedException(typeof(ExistingHeaderException))]
        public void TestAddHeaderFailsOnExistingDocumentAndHeader()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockHeaderRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(true);

            Header result = headerLogic.Add(fakeDocument.Id, fakeHeader);

            mockDocumentRepository.VerifyAll();
            mockHeaderRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestAddHeaderFailsOnMissingDocument()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(false);

            Header result = headerLogic.Add(fakeDocument.Id, fakeHeader);

            mockDocumentRepository.VerifyAll();
        }

        [TestMethod]
        public void TestUpdateHeaderWorksOnExistingHeader()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);

            mockHeaderRepository
                .Setup(wl => wl.Exists(fakeHeader.Id))
                .Returns(true);
            mockHeaderRepository
                .Setup(wl => wl.Update(fakeHeader));
            mockHeaderRepository
                .Setup(wl => wl.GetById(fakeHeader.Id))
                .Returns(fakeHeader);

            headerLogic.Update(fakeHeader.Id, fakeHeader);
            mockHeaderRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingHeaderException))]
        public void TestUpdateHeaderFailsOnMissingHeader()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);

            mockHeaderRepository
                .Setup(wl => wl.Exists(fakeHeader.Id))
                .Returns(false);

            headerLogic.Update(fakeHeader.Id, fakeHeader);


            mockHeaderRepository.VerifyAll();

        }

        [TestMethod]
        public void TestDeleteHeaderWorksOnExistingHeader()
        {
            Mock<IHeaderRepository> mockHeaderRepository = new Mock<IHeaderRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IHeaderManagementService headerLogic = new HeaderManagementService
            {
                HeaderRepository = mockHeaderRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Header fakeHeader = GetFakeHeaderForDocument(fakeDocument);

            mockHeaderRepository
                .Setup(wl => wl.Exists(fakeHeader.Id))
                .Returns(true);
            mockHeaderRepository
                .Setup(wl => wl.Delete(fakeHeader.Id));

            headerLogic.Delete(fakeHeader.Id);
            mockHeaderRepository.VerifyAll();
        }

        private Document GetFakeDocument()
        {
            return new Document
            {
                Creator = null,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now,
                StyleClass = null
            };
        }

        private Header GetFakeHeaderForDocument(Document document)
        {
            return new Header
            {
                DocumentThatBelongs = document,
                Id = Guid.NewGuid(),
                Content = new Content(),
                StyleClass = null
            };
        }
    }
}
