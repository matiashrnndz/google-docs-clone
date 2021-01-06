using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;

namespace ServiceImp
{
    public class CommentService : ICommentService
    {
        internal IDocumentRepository DocumentRepository { get; set; }
        internal ICommentRepository CommentRepository { get; set; }
        internal IUserRepository UserRepository { get; set; }

        public void AddComment(string userEmail, string documentId, Comment comment)
        {
            Guid docId = Guid.Parse(documentId);

            ValidateUser(userEmail);
            ValidateDocument(docId);
            ValidateRating(comment.Rating);

            User user = UserRepository.GetByEmail(userEmail);
            Document document = DocumentRepository.GetById(docId);

            comment.Id = Guid.NewGuid();
            comment.Commenter = user;
            comment.Document = document;

            CommentRepository.AddComment(comment);
        }

        public IEnumerable<Comment> GetComments(string documentId)
        {
            Guid docId = Guid.Parse(documentId);

            ValidateDocument(docId);

            IEnumerable<Comment> comments = CommentRepository.GetComments(docId);

            return comments;
        }

        private void ValidateRating(int rating)
        {
            if (rating < 0 || rating > 5)
            {
                throw new InvalidRatingException("The rating is not valid.");
            }
        }

        private void ValidateUser(string userEmail)
        {
            if (!UserRepository.Exists(userEmail))
            {
                throw new MissingUserException("The user does not exist.");
            }
        }

        private void ValidateDocument(Guid documentId)
        {
            if (!DocumentRepository.Exists(documentId))
            {
                throw new MissingDocumentException("The documment does not exist.");
            }
        }
    }
}
