using Domain;
using Exception;
using Repository;
using Service;
using System;

namespace ServiceImp
{
    public class HeaderManagementService : IHeaderManagementService
    {
        internal IHeaderRepository HeaderRepository { get; set; }
        internal IDocumentRepository DocumentRepository { get; set; }
        internal IContentRepository ContentRepository { get; set; }
        internal IStyleClassRepository StyleClassRepository { get; set; }

        public Header GetByDocument(Guid documentId)
        {
            if (DocumentRepository.Exists(documentId))
            {
                if (HeaderRepository.ExistsForDocument(documentId))
                {
                    return HeaderRepository.GetByDocument(documentId);
                }
                else
                {
                    throw new MissingHeaderException("This header is not in the database");
                }
            }
            else
            {
                throw new MissingDocumentException("This document is not on the database.");
            }

        }

        public void Update(Guid headerId, Header newHeaderData)
        {
            if (HeaderRepository.Exists(headerId))
            {
                Header headerToUpdate = HeaderRepository.GetById(headerId);
                if (newHeaderData.StyleClass != null && !StyleClassRepository.Exists(newHeaderData.StyleClass.Name))
                {
                    newHeaderData.StyleClass = null;
                }
                headerToUpdate.StyleClass = newHeaderData.StyleClass;
                HeaderRepository.Update(headerToUpdate);
            }
            else
            {
                throw new MissingHeaderException("The header is not in the database.");
            }
        }

        public Header Add(Guid documentId, Header header)
        {
            if (DocumentRepository.Exists(documentId))
            {
                if (!HeaderRepository.ExistsForDocument(documentId))
                {
                    Document documentThatBelongs = DocumentRepository.GetById(documentId);
                    documentThatBelongs.StyleClass = null;

                    header.DocumentThatBelongs = documentThatBelongs;

                    header.Id = Guid.NewGuid();

                    header.Content = new Content()
                    {
                        Id = Guid.NewGuid()
                    };

                    if (header.StyleClass != null && !StyleClassRepository.Exists(header.StyleClass.Name))
                    {
                        header.StyleClass = null;
                    }

                    ContentRepository.Add(header.Content);
                    HeaderRepository.Add(header);

                    return header;
                }
                else
                {
                    throw new ExistingHeaderException("This document already has a header.");
                }
            }
            else
            {
                throw new MissingDocumentException("This document is not in the database.");
            }
        }

        public void Delete(Guid headerId)
        {
            if (HeaderRepository.Exists(headerId))
            {
                HeaderRepository.Delete(headerId);
            }
            else
            {
                throw new MissingHeaderException("This header is not in the database.");
            }
        }
    }
}
