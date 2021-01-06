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
    public class CommentServiceTest
    {
        [TestMethod]
        public void TestGetCommentsCallsRepositoryGetAll()
        {
            Mock<ICommentRepository> mockCommentRepository = new Mock<ICommentRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

            ICommentService commentService = new CommentService
            {
                CommentRepository = mockCommentRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                UserRepository = mockUserRepository.Object
            };

            IEnumerable<Comment> fakeComments = GetFakeComment();
            Document fakeDocument = GetFakeDocument();

            mockCommentRepository
                .Setup(wl => wl.GetComments(fakeDocument.Id))
                .Returns(fakeComments);

            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);

            IEnumerable<Comment> results = commentService.GetComments(fakeDocument.Id.ToString());

            mockCommentRepository.VerifyAll();
            mockDocumentRepository.VerifyAll();

            Assert.IsNotNull(results);
            Assert.IsTrue(fakeComments.SequenceEqual(results));
        }

        [TestMethod]
        public void TestAddCommentCallsRepositoryAddComment()
        {
            Mock<ICommentRepository> mockCommentRepository = new Mock<ICommentRepository>();
            Mock<IDocumentRepository> mockDocumentRepository = new Mock<IDocumentRepository>();
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>();

            ICommentService commentService = new CommentService
            {
                CommentRepository = mockCommentRepository.Object,
                DocumentRepository = mockDocumentRepository.Object,
                UserRepository = mockUserRepository.Object
            };

            IEnumerable<Comment> fakeComments = GetFakeComment();
            Comment fakeComment = fakeComments.ElementAt(1);
            Document fakeDocument = GetFakeDocument();
            User fakeUser = GetFakeUser();

            mockCommentRepository
                .Setup(wl => wl.AddComment(fakeComment));

            mockDocumentRepository
                .Setup(wl => wl.Exists(fakeDocument.Id))
                .Returns(true);

            mockDocumentRepository
                .Setup(wl => wl.GetById(fakeDocument.Id))
                .Returns(fakeDocument);

            mockUserRepository
                .Setup(wl => wl.Exists(fakeUser.Email))
                .Returns(true);

            mockUserRepository
                .Setup(wl => wl.GetByEmail(fakeUser.Email))
                .Returns(fakeUser);

            commentService.AddComment(fakeUser.Email, fakeDocument.Id.ToString(), fakeComment);

            mockCommentRepository.VerifyAll();
            mockDocumentRepository.VerifyAll();
            mockUserRepository.VerifyAll();

            Assert.IsTrue(true);
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

        private Document GetFakeDocument()
        {
            Document document = new Document
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                Title = "title",
                Creator = GetFakeUser(),
                StyleClass = null,
                CreationDate = DateTime.Now,
                LastModification = DateTime.Now
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
                    Document = GetFakeDocument(),
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    Rating = 3,
                    Text = "bueno"
                },
                new Comment
                {
                   Commenter = GetFakeUser(),
                    Document = GetFakeDocument(),
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Rating = 3,
                    Text = "Muy bueno"
                },
                new Comment
                {
                    Commenter = GetFakeUser(),
                    Document = GetFakeDocument(),
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Rating = 5,
                    Text = "Excelente"
                }
            };

            return fakeList.AsEnumerable();
        }
    }
}
