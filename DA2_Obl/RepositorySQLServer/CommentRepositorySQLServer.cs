using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class CommentRepositorySQLServer : ICommentRepository
    {
        private static CommentRepositorySQLServer instance;

        private CommentRepositorySQLServer()
        {

        }

        public static CommentRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new CommentRepositorySQLServer();
            }

            return instance;
        }

        public void AddComment(Comment toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (toAdd.Commenter != null)
                {
                    c.Entry(toAdd.Commenter)
                        .State = EntityState.Unchanged;
                }

                if (toAdd.Document != null)
                {
                    c.Entry(toAdd.Document)
                        .State = EntityState.Unchanged;
                }

                c.Comments.Add(toAdd);

                c.SaveChanges();
            }
        }

        public IEnumerable<Comment> GetAll()
        {
            List<Comment> comments;

            using (DatabaseContext c = new DatabaseContext())
            {
                comments = c.Comments
                    .Include(u => u.Commenter)
                    .Include(s => s.Document)
                    .ToList();
            }

            return comments.AsEnumerable();
        }

        public IEnumerable<Comment> GetComments(Guid documentId)
        {
            List<Comment> comments;

            using (DatabaseContext c = new DatabaseContext())
            {
                comments = c.Comments
                    .Include(u => u.Commenter)
                    .Include(s => s.Document)
                    .Where(a => a.Document.Id == documentId)
                    .ToList();
            }

            return comments.AsEnumerable();
        }
    }
}
