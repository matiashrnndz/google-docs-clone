using Domain;
using System.Collections.Generic;

namespace Service
{
    public interface ICommentService
    {
        void AddComment(string userEmail, string documentId, Comment comment);
        IEnumerable<Comment> GetComments(string documentId);
    }
}
