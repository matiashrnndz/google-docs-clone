using Domain;
using Repository;
using System;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class HeaderRepositorySQLServer : IHeaderRepository
    {
        private static HeaderRepositorySQLServer instance;

        private HeaderRepositorySQLServer()
        {

        }

        public static HeaderRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new HeaderRepositorySQLServer();
            }

            return instance;
        }

        public Header GetById(Guid headerId)
        {
            Header toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Headers
                    .Where(f => f.Id == headerId)
                    .Include(t => t.Content)
                    .Include(s => s.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public Header GetByDocument(Guid documentId)
        {
            Header toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Headers
                    .Where(f => f.DocumentThatBelongs.Id == documentId)
                    .Include(t => t.Content)
                    .Include(s => s.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public bool Exists(Guid headerId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Headers
                    .Any(f => f.Id == headerId);
            }

            return exists;
        }

        public void Add(Header toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (toAdd.DocumentThatBelongs != null)
                {
                    c.Entry(toAdd.DocumentThatBelongs)
                        .State = EntityState.Unchanged;
                }

                if (toAdd.StyleClass != null)
                {
                    toAdd.StyleClass.BasedOn = null;

                    c.Entry(toAdd.StyleClass)
                        .State = EntityState.Unchanged;
                }

                if (toAdd.Content != null)
                {
                    c.Entry(toAdd.Content)
                        .State = EntityState.Unchanged;
                }

                c.Headers.Add(toAdd);

                c.SaveChanges();
            }
        }

        public void Update(Header header)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Header toUpdate = c.Headers
                    .Where(f => f.Id == header.Id)
                    .Include(t => t.Content)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();

                toUpdate.StyleClass = header.StyleClass;

                if (toUpdate.StyleClass != null)
                {
                    toUpdate.StyleClass.BasedOn = null;

                    c.Entry(toUpdate.StyleClass)
                        .State = EntityState.Unchanged;
                }

                c.SaveChanges();
            }
        }

        public void Delete(Guid headerId)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Header toDelete = c.Headers
                    .Where(h => h.Id == headerId)
                    .Include(t => t.Content)
                    .Include(s => s.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();

                if (toDelete.DocumentThatBelongs != null)
                {
                    c.Entry(toDelete.DocumentThatBelongs)
                        .State = EntityState.Unchanged;
                }

                if (toDelete.StyleClass != null)
                {
                    toDelete.StyleClass.BasedOn = null;

                    c.Entry(toDelete.StyleClass)
                        .State = EntityState.Unchanged;
                }

                if (toDelete.Content != null)
                {
                    c.Entry(toDelete.Content)
                        .State = EntityState.Unchanged;
                }

                c.Headers.Remove(toDelete);

                c.SaveChanges();
            }
        }

        public bool ExistsForDocument(Guid documentId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Headers
                    .Any(p => p.DocumentThatBelongs.Id == documentId);
            }

            return exists;
        }

        public bool ExistsWithContent(Content content)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Headers.Any(p => p.Content.Id == content.Id);
            }

            return exists;
        }

        public Header GetByContent(Content content)
        {
            Header toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Headers.Where(p => p.Content.Id == content.Id)
                    .Include(a => a.Content)
                    .Include(b => b.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();
            }

            return toGet;
        }
    }
}
