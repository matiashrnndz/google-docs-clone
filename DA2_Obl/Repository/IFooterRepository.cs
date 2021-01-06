using Domain;
using System;

namespace Repository
{
    public interface IFooterRepository
    {
        Footer GetByDocument(Guid documentId);
        Footer GetById(Guid footerId);
        bool Exists(Guid footerId);
        void Add(Footer footer);
        void Update(Footer footer);
        void Delete(Guid footerId);
        bool ExistsForDocument(Guid documentId);
        bool ExistsWithContent(Content content);
        Footer GetByContent(Content content);
    }
}
