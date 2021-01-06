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
    public class ParagraphManagementTest
    {
        private Mock<IParagraphRepository> mockParagraphRepository;
        private Mock<IDocumentRepository> mockDocumentRepository;
        private Mock<IStyleClassRepository> mockStyleClassRepository;
        private Mock<IContentRepository> mockContentRepository;
        private IParagraphManagementService paragraphLogic;

        public ParagraphManagementTest()
        {
            mockParagraphRepository = new Mock<IParagraphRepository>();
            mockDocumentRepository = new Mock<IDocumentRepository>();
            mockStyleClassRepository = new Mock<IStyleClassRepository>();
            mockContentRepository = new Mock<IContentRepository>();
            paragraphLogic = new ParagraphManagementService()
            {
                ParagraphRepository = mockParagraphRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                StyleClassRepository = mockStyleClassRepository.Object,
                ContentRepository = mockContentRepository.Object
            };
        }

        [TestMethod]
        public void TestGetAllParagraphsByDocumentWorksOnExistingDocument()
        {
            Document fakeDocument = GetFakeDocument();
            IEnumerable<Paragraph> fakeParagraphs = GetFakeParagraphsForDocument(fakeDocument);
            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockParagraphRepository
                .Setup(wl => wl.GetAllByDocument(fakeDocument.Id))
                .Returns(fakeParagraphs);

            IEnumerable<Paragraph> results = paragraphLogic.GetAllByDocument(fakeDocument.Id);

            mockDocumentRepository.VerifyAll();
            mockParagraphRepository.VerifyAll();
            Assert.IsTrue(results.SequenceEqual(fakeParagraphs));
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestGetAllParagraphsByDocumentFailsOnMissingDocument()
        {
            Document fakeDocument = GetFakeDocument();
            IEnumerable<Paragraph> fakeParagraphs = GetFakeParagraphsForDocument(fakeDocument);
            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(false);

            IEnumerable<Paragraph> results = paragraphLogic.GetAllByDocument(fakeDocument.Id);

            mockDocumentRepository.VerifyAll();
        }

        [TestMethod]
        public void TestAddParagraphWorksOnExistingDocument()
        {
            Document fakeDocument = GetFakeDocument();
            IEnumerable<Paragraph> fakeParagraphs = GetFakeParagraphsForDocument(fakeDocument);
            Paragraph fakeNewParagraph = GetFakeParagraph();
            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);
            mockDocumentRepository
                .Setup(wl => wl.GetById(fakeDocument.Id))
                .Returns(fakeDocument);
            mockParagraphRepository
                .Setup(wl => wl.GetAllByDocument(fakeDocument.Id))
                .Returns(fakeParagraphs);
            mockContentRepository
                .Setup(wl => wl.Add(fakeNewParagraph.Content));
            mockParagraphRepository
                .Setup(wl => wl.Add(fakeNewParagraph));

            Paragraph result = paragraphLogic.Add(fakeDocument.Id, fakeNewParagraph);

            mockDocumentRepository.VerifyAll();
            mockParagraphRepository.VerifyAll();
            Assert.AreEqual(result, fakeNewParagraph);
            Assert.AreEqual(result.Position, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingDocumentException))]
        public void TestAddParagraphFailsOnMissingDocument()
        {
            Document fakeDocument = GetFakeDocument();
            IEnumerable<Paragraph> fakeParagraphs = GetFakeParagraphsForDocument(fakeDocument);
            Paragraph fakeNewParagraph = GetFakeParagraph();
            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(false);

            Paragraph result = paragraphLogic.Add(fakeDocument.Id, fakeNewParagraph);

            mockDocumentRepository.VerifyAll();
            mockParagraphRepository.VerifyAll();
        }

        [TestMethod]
        public void TestUpdateParagraphWorksOnExistingParagraphAndValidPosition()
        {
            Document fakeDocument = GetFakeDocument();
            IEnumerable<Paragraph> fakeParagraphs = GetFakeParagraphsForDocument(fakeDocument);
            Paragraph fakeNewParagraph = new Paragraph
            {
                Id = fakeParagraphs.ElementAt(0).Id,
                Content = null,
                DocumentThatBelongs = fakeDocument,
                Position = 2,
                StyleClass = null
            };
            mockParagraphRepository
                .Setup(wl => wl.Exists(fakeNewParagraph.Id))
                .Returns(true);
            mockParagraphRepository
                .Setup(wl => wl.GetById(fakeNewParagraph.Id))
                .Returns(fakeParagraphs.ElementAt(0));
            mockParagraphRepository
                .Setup(wl => wl.GetAllByDocument(fakeDocument.Id))
                .Returns(fakeParagraphs);
            mockParagraphRepository
                .Setup(wl => wl.Update(fakeParagraphs.ElementAt(0)));
            mockParagraphRepository
                .Setup(wl => wl.Update(fakeParagraphs.ElementAt(2)));

            paragraphLogic.Update(fakeNewParagraph.Id, fakeNewParagraph);

            mockParagraphRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingParagraphException))]
        public void TestUpdateParagraphFailsOnMissingParagraph()
        {
            Document fakeDocument = GetFakeDocument();
            IEnumerable<Paragraph> fakeParagraphs = GetFakeParagraphsForDocument(fakeDocument);
            Paragraph fakeNewParagraph = new Paragraph
            {
                Id = fakeParagraphs.ElementAt(0).Id,
                Content = null,
                DocumentThatBelongs = fakeDocument,
                Position = 2,
                StyleClass = null
            };
            mockParagraphRepository
                .Setup(wl => wl.Exists(fakeNewParagraph.Id))
                .Returns(false);

            paragraphLogic.Update(fakeNewParagraph.Id, fakeNewParagraph);

            mockParagraphRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPositionException))]
        public void TestUpdateParagraphFailsOnInvalidPosition()
        {
            Document fakeDocument = GetFakeDocument();
            IEnumerable<Paragraph> fakeParagraphs = GetFakeParagraphsForDocument(fakeDocument);
            Paragraph fakeNewParagraph = new Paragraph
            {
                Id = fakeParagraphs.ElementAt(0).Id,
                Content = null,
                DocumentThatBelongs = fakeDocument,
                Position = 99,
                StyleClass = null
            };
            mockParagraphRepository
                .Setup(wl => wl.Exists(fakeNewParagraph.Id))
                .Returns(true);
            mockParagraphRepository
                .Setup(wl => wl.GetById(fakeNewParagraph.Id))
                .Returns(fakeParagraphs.ElementAt(0));
            mockParagraphRepository
                .Setup(wl => wl.GetAllByDocument(fakeDocument.Id))
                .Returns(fakeParagraphs);

            paragraphLogic.Update(fakeNewParagraph.Id, fakeNewParagraph);

            mockParagraphRepository.VerifyAll();
        }

        [TestMethod]
        public void TestDeleteParagraphWorksOnExistingParagraph()
        {
            Paragraph fakeParagraph = GetFakeParagraph();
            IEnumerable<Paragraph> fakeParagraphs = GetFakeParagraphsForDocument(fakeParagraph.DocumentThatBelongs);

            mockParagraphRepository
                .Setup(wl => wl.Exists(fakeParagraph.Id))
                .Returns(true);
            mockParagraphRepository
                .Setup(wl => wl.GetById(fakeParagraph.Id))
                .Returns(fakeParagraph);
            mockParagraphRepository
                .Setup(wl => wl.GetAllByDocument(fakeParagraph.DocumentThatBelongs.Id))
                .Returns(fakeParagraphs);
            mockParagraphRepository
                .Setup(wl => wl.Delete(fakeParagraph.Id));

            paragraphLogic.Delete(fakeParagraph.Id);

            mockParagraphRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(MissingParagraphException))]
        public void TestDeleteParagraphFailsOnMissingParagraph()
        {
            Document fakeDocument = GetFakeDocument();
            Paragraph fakeParagraph = GetFakeParagraph();

            mockParagraphRepository
                .Setup(wl => wl.Exists(fakeParagraph.Id))
                .Returns(false);

            paragraphLogic.Delete(fakeParagraph.Id);

            mockParagraphRepository.VerifyAll();
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

        private Paragraph GetFakeParagraph()
        {
            return new Paragraph
            {
                Content = new Content(),
                Id = Guid.NewGuid(),
                DocumentThatBelongs = GetFakeDocument(),
                StyleClass = null,
                Position = 0
            };
        }

        private IEnumerable<Paragraph> GetFakeParagraphsForDocument(Document document)
        {
            List<Paragraph> fakeParagraphs = new List<Paragraph>
            {
                new Paragraph
                {
                    Content = new Content(),
                    DocumentThatBelongs = document,
                    StyleClass = null,
                    Id = Guid.NewGuid(),
                    Position = 0
                },
                new Paragraph
                {
                    Content = new Content(),
                    DocumentThatBelongs = document,
                    StyleClass = null,
                    Id = Guid.NewGuid(),
                    Position = 1
                },
                new Paragraph
                {
                    Content = new Content(),
                    DocumentThatBelongs = document,
                    StyleClass = null,
                    Id = Guid.NewGuid(),
                    Position = 2
                }
            };
            return fakeParagraphs.AsEnumerable();
        }
    }
}
