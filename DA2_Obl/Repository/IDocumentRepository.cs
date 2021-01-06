using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IDocumentRepository
    {
        IEnumerable<Document> GetAll();
        IEnumerable<Document> GetAllByUser(string userEmail);
        bool Exists(Guid documentId);
        Document GetById(Guid documentId);
        void Add(Document document);
        void Update(Guid documentId, Document document);
        void Delete(Guid documentId);
    }
}
