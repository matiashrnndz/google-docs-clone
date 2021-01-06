using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IDocumentManagementService
    {
        IEnumerable<Document> GetAllByUser(string userEmail);
        Document GetById(Guid documentId);
        void Update(Guid documentId, Document document);
        Document Add(string userEmail, Document document);
        void Delete(Guid documentId);
        IEnumerable<Document> GetAllByUserFilteredAndOrdered(string userEmail, DocumentFilterAndOrder filterAndOrder);
    }
}
