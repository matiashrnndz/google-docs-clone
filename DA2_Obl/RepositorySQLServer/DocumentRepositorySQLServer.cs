using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class DocumentRepositorySQLServer : IDocumentRepository
    {
        private static DocumentRepositorySQLServer instance;

        private DocumentRepositorySQLServer()
        {

        }

        public static DocumentRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new DocumentRepositorySQLServer();
            }

            return instance;
        }

        public IEnumerable<Document> GetAll()
        {
            List<Document> documents;

            using (DatabaseContext c = new DatabaseContext())
            {
                documents = c.Documents
                    .Include(u => u.Creator)
                    .Include(s => s.StyleClass)
                    .ToList();
            }

            return documents.AsEnumerable();
        }

        public IEnumerable<Document> GetAllByUser(string email)
        {
            List<Document> documents;

            using (DatabaseContext c = new DatabaseContext())
            {
                documents = c.Documents
                    .Include(u => u.Creator)
                    .Include(s => s.StyleClass)
                    .Where(x => x.Creator.Email == email)
                    .ToList();
            }

            return documents.AsEnumerable();
        }

        public Document GetById(Guid documentId)
        {
            Document toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Documents
                    .Where(d => d.Id == documentId)
                    .Include(u => u.Creator)
                    .Include(s => s.StyleClass)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public bool Exists(Guid documentId)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Documents
                    .Any(d => d.Id == documentId);
            }

            return exists;
        }

        public void Add(Document toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (toAdd.Creator != null)
                {
                    c.Entry(toAdd.Creator)
                        .State = EntityState.Unchanged;
                }

                if (toAdd.StyleClass != null)
                {
                    toAdd.StyleClass.BasedOn = null;

                    c.Entry(toAdd.StyleClass)
                        .State = EntityState.Unchanged;
                }

                c.Documents.Add(toAdd);

                c.SaveChanges();
            }
        }

        public void Update(Guid documentId, Document document)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Document toUpdate = c.Documents
                    .Where(d => d.Id == documentId)
                    .Include(u => u.Creator)
                    .FirstOrDefault();

                toUpdate.Title = document.Title;
                toUpdate.StyleClass = document.StyleClass;
                toUpdate.LastModification = document.LastModification;

                if (document.StyleClass != null)
                {
                    toUpdate.StyleClass.BasedOn = null;

                    c.Entry(toUpdate.StyleClass)
                        .State = EntityState.Unchanged;
                }

                if (document.Creator != null)
                {
                    c.Entry(toUpdate.Creator)
                        .State = EntityState.Unchanged;
                }

                c.SaveChanges();
            }
        }

        public void Delete(Guid documentId)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                Document toDelete = c.Documents
                    .Where(d => d.Id == documentId)
                    .Include(s => s.StyleClass)
                    .Include(u => u.Creator)
                    .FirstOrDefault();

                if (toDelete.Creator != null)
                {
                    c.Entry(toDelete.Creator).State = EntityState.Unchanged;
                }

                if (toDelete.StyleClass != null)
                {
                    toDelete.StyleClass.BasedOn = null;

                    c.Entry(toDelete.StyleClass).State = EntityState.Unchanged;
                }

                c.Documents.Remove(toDelete);

                c.SaveChanges();
            }
        }
    }
}
