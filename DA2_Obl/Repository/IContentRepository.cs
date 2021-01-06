using Domain;
using System;

namespace Repository
{
    public interface IContentRepository
    {
        Content GetById(Guid id);
        Content GetByHeader(Header header);
        Content GetByParagraph(Paragraph paragraph);
        Content GetByFooter(Footer footer);
        Content GetByText(Text text);
        void Add(Content content);
        bool Exists(Guid contentId);
        void Delete(Content content);
    }
}
