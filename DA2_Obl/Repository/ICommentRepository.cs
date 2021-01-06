using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        IEnumerable<Comment> GetComments(Guid documentId);
        IEnumerable<Comment> GetAll();
    }
}
