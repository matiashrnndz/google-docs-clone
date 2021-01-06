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
    public class FooterManagementTest
    {
        [TestMethod]
        public void TestGetByDocumentReturnsFooterOnExistingFooterAndDocument()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockFooterRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(true);
            mockFooterRepository
                .Setup(wl => wl.GetByDocument(fakeDocument.Id))
                .Returns(fakeFooter);

            Footer result = footerLogic.GetByDocument(fakeDocument.Id);

            mockDocumentRepository.VerifyAll();
            mockFooterRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, fakeFooter);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestGetByDocumentFailsOnMissingDocument()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();

            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);



            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(false);
            Footer result = footerLogic.GetByDocument(fakeDocument.Id);

            mockDocumentRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFooterException))]
        public void TestGetByDocumentFailsOnMissingFooter()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockFooterRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(false);

            Footer result = footerLogic.GetByDocument(fakeDocument.Id);

            mockDocumentRepository.VerifyAll();
            mockFooterRepository.VerifyAll();
        }

        [TestMethod]
        public void TestAddFooterWorksOnMissingFooterAndExistingDocument()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockFooterRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(false);
            mockFooterRepository
                .Setup(wl => wl.Add(fakeFooter));
            mockDocumentRepository
                .Setup(wl => wl.GetById(fakeDocument.Id))
                .Returns(fakeDocument);

            Footer result = footerLogic.Add(fakeDocument.Id, fakeFooter);

            mockDocumentRepository.VerifyAll();
            mockFooterRepository.VerifyAll();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, fakeFooter);
        }

        [TestMethod]
        [ExpectedException(typeof(ExistingFooterException))]
        public void TestAddFooterFailsOnExistingDocumentAndFooter()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockFooterRepository
                .Setup(wl => wl.ExistsForDocument(fakeDocument.Id))
                .Returns(true);

            Footer result = footerLogic.Add(fakeDocument.Id, fakeFooter);

            mockDocumentRepository.VerifyAll();
            mockFooterRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestAddFooterFailsOnMissingDocument()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);


            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(false);

            Footer result = footerLogic.Add(fakeDocument.Id, fakeFooter);

            mockDocumentRepository.VerifyAll();
        }

        [TestMethod]
        public void TestUpdateFooterWorksOnExistingFooter()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);

            mockFooterRepository
                .Setup(wl => wl.Exists(fakeFooter.Id))
                .Returns(true);
            mockFooterRepository
                .Setup(wl => wl.Update(fakeFooter));
            mockFooterRepository
                .Setup(wl => wl.GetById(fakeFooter.Id))
                .Returns(fakeFooter);

            footerLogic.Update(fakeFooter.Id, fakeFooter);
            mockFooterRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFooterException))]
        public void TestUpdateFooterFailsOnMissingFooter()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);

            mockFooterRepository
                .Setup(wl => wl.Exists(fakeFooter.Id))
                .Returns(false);

            footerLogic.Update(fakeFooter.Id, fakeFooter);


            mockFooterRepository.VerifyAll();

        }

        [TestMethod]
        public void TestDeleteFooterWorksOnExistingFooter()
        {
            Mock<IFooterRepository> mockFooterRepository = new Mock<IFooterRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IContentRepository> mockContentRepository = new Mock<IContentRepository>();
            Mock<IStyleClassRepository> mockStyleClassRepository = new Mock<IStyleClassRepository>();
            IFooterManagementService footerLogic = new FooterManagementService
            {
                FooterRepository = mockFooterRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                ContentRepository = mockContentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object
            };
            Document fakeDocument = GetFakeDocument();
            Footer fakeFooter = GetFakeFooterForDocument(fakeDocument);

            mockFooterRepository
                .Setup(wl => wl.Exists(fakeFooter.Id))
                .Returns(true);
            mockFooterRepository
                .Setup(wl => wl.Delete(fakeFooter.Id));

            footerLogic.Delete(fakeFooter.Id);
            mockFooterRepository.VerifyAll();
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

        private Footer GetFakeFooterForDocument(Document document)
        {
            return new Footer
            {
                DocumentThatBelongs = document,
                Id = Guid.NewGuid(),
                Content = new Content(),
                StyleClass = null
            };
        }
    }
}
