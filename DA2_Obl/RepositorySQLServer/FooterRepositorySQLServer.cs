using Domain;
using Repository;
using System;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class FooterRepositorySQLServer : IFooterRepository
    {
        private static FooterRepositorySQLServer instance;

        private FooterRepositorySQLServer()
        {

        }

        public static FooterRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new FooterRepositorySQLServer();
            }

            return instance;
        }

        public Footer GetById(Guid footerId)
        {
            Footer toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Footers
                    .Where(f => f.Id == footerId)
                    .Include(t => t.Content)
                    .Include(s => s.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public Footer GetByDocument(Guid documentId)
        {
            Footer toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Footers
                    .Where(f => f.DocumentThatBelongs.Id == documentId)
                    .Include(t => t.Content)
                    .Include(s => s.StyleClass)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public bool Exists(Guid footerId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Footers
                    .Any(f => f.Id == footerId);
            }

            return exists;
        }

        public void Add(Footer toAdd)
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

                c.Footers.Add(toAdd);

                c.SaveChanges();
            }
        }

        public void Update(Footer footer)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Footer toUpdate = c.Footers
                    .Where(f => f.Id == footer.Id)
                    .Include(t => t.Content)
                    .Include(d => d.DocumentThatBelongs)
                    .FirstOrDefault();

                toUpdate.StyleClass = footer.StyleClass;

                if (toUpdate.StyleClass != null)
                {
                    toUpdate.StyleClass.BasedOn = null;

                    c.Entry(toUpdate.StyleClass)
                        .State = EntityState.Unchanged;
                }

                c.SaveChanges();
            }
        }

        public void Delete(Guid footerId)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Footer toDelete = c.Footers
                    .Where(f => f.Id == footerId)
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

                c.Footers.Remove(toDelete);

                c.SaveChanges();
            }
        }

        public bool ExistsForDocument(Guid documentId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Footers
                    .Any(f => f.DocumentThatBelongs.Id == documentId);
            }

            return exists;
        }

        public bool ExistsWithContent(Content content)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Footers.Any(p => p.Content.Id == content.Id);
            }

            return exists;
        }

        public Footer GetByContent(Content content)
        {
            Footer toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Footers.Where(p => p.Content.Id == content.Id)
                    .Include(a => a.StyleClass)
                    .Include(b => b.DocumentThatBelongs)
                    .Include(d => d.Content)
                    .FirstOrDefault();
            }

            return toGet;
        }
    }
}
