using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IParagraphManagementService
    {
        IEnumerable<Paragraph> GetAllByDocument(Guid documentId);
        Paragraph Add(Guid documentId, Paragraph paragraph);
        void Delete(Guid paragraphId);
        void Update(Guid paragraphId, Paragraph paragraph);
    }
}
