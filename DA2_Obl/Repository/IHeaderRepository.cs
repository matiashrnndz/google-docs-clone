using Domain;
using System;

namespace Repository
{
    public interface IHeaderRepository
    {
        Header GetByDocument(Guid documentId);
        Header GetById(Guid headerId);
        bool ExistsForDocument(Guid documentId);
        void Add(Header header);
        void Update(Header header);
        void Delete(Guid headerId);
        bool Exists(Guid id);
        Header GetByContent(Content content);
        bool ExistsWithContent(Content content);
    }
}
