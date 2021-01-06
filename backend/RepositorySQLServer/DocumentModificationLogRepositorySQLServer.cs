using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class DocumentModificationLogRepositorySQLServer : IDocumentModificationLogRepository
    {
        private static DocumentModificationLogRepositorySQLServer instance;

        private DocumentModificationLogRepositorySQLServer()
        {

        }

        public static DocumentModificationLogRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new DocumentModificationLogRepositorySQLServer();
            }

            return instance;
        }

        public void Add(DocumentModificationLog toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (toAdd.Document != null)
                {
                    c.Entry(toAdd.Document)
                        .State = EntityState.Unchanged;
                }

                c.DocumentModificationLogs.Add(toAdd);

                c.SaveChanges();
            }
        }

        public IEnumerable<DocumentModificationLog> GetAllByDocument(Guid documentId)
        {
            List<DocumentModificationLog> documents;

            using (DatabaseContext c = new DatabaseContext())
            {
                documents = c.DocumentModificationLogs
                    .Where(p => p.Document.Id == documentId)
                    .Include(a => a.Document)
                    .Include(b => b.Document.Creator)
                    .Include(d => d.Document.StyleClass)
                    .ToList();
            }

            return documents.AsEnumerable();
        }
    }
}