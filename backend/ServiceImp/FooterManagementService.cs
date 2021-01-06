using Domain;
using Exception;
using Repository;
using Service;
using System;

namespace ServiceImp
{
    public class FooterManagementService : IFooterManagementService
    {
        internal IFooterRepository FooterRepository { get; set; }
        internal IDocumentRepository DocumentRepository { get; set; }
        internal IContentRepository ContentRepository { get; set; }
        internal IStyleClassRepository StyleClassRepository { get; set; }

        public Footer GetByDocument(Guid documentId)
        {
            if (DocumentRepository.Exists(documentId))
            {
                if (FooterRepository.ExistsForDocument(documentId))
                {
                    return FooterRepository.GetByDocument(documentId);
                }
                else
                {
                    throw new MissingFooterException("This footer is not in the database");
                }
            }
            else
            {
                throw new MissingDocumentException("This document is not on the database.");
            }

        }

        public void Update(Guid footerId, Footer newFooterData)
        {
            if (FooterRepository.Exists(footerId))
            {
                Footer footerToUpdate = FooterRepository.GetById(footerId);
                if (newFooterData.StyleClass != null && !StyleClassRepository.Exists(newFooterData.StyleClass.Name))
                {
                    newFooterData.StyleClass = null;
                }
                footerToUpdate.StyleClass = newFooterData.StyleClass;
                FooterRepository.Update(footerToUpdate);
            }
            else
            {
                throw new MissingFooterException("The footer is not in the database.");
            }
        }

        public Footer Add(Guid documentId, Footer footer)
        {
            if (DocumentRepository.Exists(documentId))
            {
                if (!FooterRepository.ExistsForDocument(documentId))
                {
                    Document documentThatBelongs = DocumentRepository.GetById(documentId);
                    documentThatBelongs.StyleClass = null;

                    footer.DocumentThatBelongs = documentThatBelongs;

                    footer.Id = Guid.NewGuid();

                    footer.Content = new Content()
                    {
                        Id = Guid.NewGuid()
                    };

                    if (footer.StyleClass != null && !StyleClassRepository.Exists(footer.StyleClass.Name))
                    {
                        footer.StyleClass = null;
                    }
                    ContentRepository.Add(footer.Content);
                    FooterRepository.Add(footer);

                    return footer;
                }
                else
                {
                    throw new ExistingFooterException("This document already has a footer.");
                }
            }
            else
            {
                throw new MissingDocumentException("This document is not in the database.");
            }
        }

        public void Delete(Guid footerId)
        {
            if (FooterRepository.Exists(footerId))
            {
                FooterRepository.Delete(footerId);
            }
            else
            {
                throw new MissingFooterException("This footer is not in the database.");
            }
        }
    }
}
