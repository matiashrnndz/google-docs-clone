using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IParagraphRepository
    {
        IEnumerable<Paragraph> GetAllByDocument(Guid documentId);
        Paragraph GetById(Guid paragraphId);
        bool Exists(Guid paragraphId);
        void Add(Paragraph paragraph);
        void Update(Paragraph paragraph);
        void Delete(Guid documentId);
        bool ExistsWithContent(Content content);
        Paragraph GetByContent(Content content);
    }
}
