using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface ITextManagementService
    {
        Text GetById(Guid textId);
        Text GetByHeader(Guid headerId);
        IEnumerable<Text> GetAllByParagraph(Guid paragraphId);
        Text GetByFooter(Guid footerId);
        void Update(Guid textId, Text newTextData);
        void Delete(Guid textId);
        Text AddToFooter(Guid footerId, Text text);
        Text AddToHeader(Guid headerId, Text text);
        Text AddToParagraph(Guid paragraphId, Text text);
    }
}
