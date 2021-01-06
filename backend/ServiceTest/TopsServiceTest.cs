using Domain;
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
    public class TopsServiceTest
    {
        [TestMethod]
        public void TestGetTop3DocumentsByRatingWithCeroDocuments()
        {
            Mock<ICommentRepository> mockCommentRepository = new Mock<ICommentRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();

            ITopsService topService = new TopsService
            {
                CommentRepository = mockCommentRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
            };

            mockCommentRepository
                .Setup(wl => wl.GetAll())
                .Returns(new List<Comment>());

            IEnumerable<Document> results = topService.GetTop3DocumentsByRating();

            mockCommentRepository.VerifyAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() == 0);
        }

        [TestMethod]
        public void TestGetTop3DocumentsByRatingWithOneDocuments()
        {
            Mock<ICommentRepository> mockCommentRepository = new Mock<ICommentRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();

            ITopsService topService = new TopsService
            {
                CommentRepository = mockCommentRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
            };

            IEnumerable<Comment> fakeComments = GetFakeComment();

            Document fakeDocument0 = GetFakeDocument0();
            Document fakeDocument1 = GetFakeDocument1();

            mockCommentRepository
                .Setup(wl => wl.GetAll())
                .Returns(new List<Comment>() { fakeComments.ElementAt(3) });

            mockDocumentRepository
                .Setup(wl => wl.GetAll())
                .Returns(new List<Document>() { fakeDocument0 });

            mockDocumentRepository
                 .Setup(wl => wl.GetById(fakeDocument0.Id))
                 .Returns(fakeDocument0);

            IEnumerable<Document> expected = new List<Document>()
            {
                fakeDocument0
            };

            IEnumerable<Document> results = topService.GetTop3DocumentsByRating();

            mockCommentRepository.VerifyAll();
            mockDocumentRepository.VerifyAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(expected.SequenceEqual(results));
        }

        [TestMethod]
        public void TestGetTop3DocumentsByRatingWithThreeDocuments()
        {
            Mock<ICommentRepository> mockCommentRepository = new Mock<ICommentRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();

            ITopsService topService = new TopsService
            {
                CommentRepository = mockCommentRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
            };

            IEnumerable<Comment> fakeComments = GetFakeComment();

            Document fakeDocument0 = GetFakeDocument0();
            Document fakeDocument1 = GetFakeDocument1();

            mockCommentRepository
                .Setup(wl => wl.GetAll())
                .Returns(fakeComments);

            mockDocumentRepository
                .Setup(wl => wl.GetAll())
                .Returns(new List<Document>() { fakeDocument0, fakeDocument0, fakeDocument0 });

            mockDocumentRepository
             .Setup(wl => wl.GetById(fakeDocument0.Id))
             .Returns(fakeDocument0);

            IEnumerable<Document> expected = new List<Document>()
            {
                fakeDocument0, fakeDocument0, fakeDocument0
            };

            IEnumerable<Document> results = topService.GetTop3DocumentsByRating();

            mockCommentRepository.VerifyAll();
            mockDocumentRepository.VerifyAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(expected.SequenceEqual(results));
        }

        private User GetFakeUser()
        {
            User user = new User
            {
                Email = "admin@admin.com",
                Administrator = true,
                LastName = "admin",
                Name = "admin",
                UserName = "admin",
                Password = "admin"
            };

            return user;
        }

        private Document GetFakeDocument0()
        {
            Document document = new Document
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Title = "title 0",
                Creator = GetFakeUser(),
                StyleClass = null,
                CreationDate = DateTime.MaxValue,
                LastModification = DateTime.MaxValue
            };

            return document;
        }

        private Document GetFakeDocument1()
        {
            Document document = new Document
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Title = "title 1",
                Creator = GetFakeUser(),
                StyleClass = null,
                CreationDate = DateTime.MaxValue,
                LastModification = DateTime.MaxValue
            };

            return document;
        }

        private IEnumerable<Comment> GetFakeComment()
        {
            List<Comment> fakeList = new List<Comment>
            {
                new Comment
                {
                    Commenter = GetFakeUser(),
                    Document = GetFakeDocument1(),
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    Rating = 3,
                    Text = "bueno"
                },
                new Comment
                {
                   Commenter = GetFakeUser(),
                    Document = GetFakeDocument0(),
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Rating = 4,
                    Text = "Muy bueno"
                },
                new Comment
                {
                    Commenter = GetFakeUser(),
                    Document = GetFakeDocument0(),
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Rating = 5,
                    Text = "Excelente"
                },
                new Comment
                {
                    Commenter = GetFakeUser(),
                    Document = GetFakeDocument0(),
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Rating = 5,
                    Text = "Excelente"
                }

            };

            return fakeList.AsEnumerable();
        }
    }
}
