using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceImp
{
    public class DocumentManagementService : IDocumentManagementService
    {
        internal IDocumentRepository DocumentRepository { get; set; }
        internal IUserRepository UserRepository { get; set; }
        internal IStyleClassRepository StyleClassRepository { get; set; }
        internal IFormatRepository FormatRepository { get; set; }
        internal ICodeGenerator CodeGenerator { get; set; }

        public Document Add(string userEmail, Document document)
        {
            if (UserRepository.Exists(userEmail))
            {
                document.Id = Guid.NewGuid();
                document.CreationDate = DateTime.Now;
                document.LastModification = document.CreationDate;
                document.Creator = UserRepository.GetByEmail(userEmail);
                if (document.StyleClass != null && !StyleClassRepository.Exists(document.StyleClass.Name))
                {
                    document.StyleClass = null;
                }

                DocumentRepository.Add(document);

                return document;
            }
            else
            {
                throw new MissingUserException("The user is not on the database.");
            }
        }

        //Decidimos que es responsabilidad del Repositorio borrar en cascada los elementos que dependen del Documento
        public void Delete(Guid documentId)
        {
            if (DocumentRepository.Exists(documentId))
            {
                DocumentRepository.Delete(documentId);
            }
            else
            {
                throw new MissingDocumentException("The document is not on the database.");
            }
        }

        public IEnumerable<Document> GetAllByUser(string userEmail)
        {
            if (UserRepository.Exists(userEmail))
            {
                return DocumentRepository.GetAllByUser(userEmail);
            }
            else
            {
                throw new MissingUserException("The user is not on the database.");
            }
        }

        public IEnumerable<Document> GetAllByUserFilteredAndOrdered(string userEmail, DocumentFilterAndOrder filterAndOrder)
        {
            IEnumerable<Document> documents = DocumentRepository.GetAllByUser(userEmail);
            IEnumerable<Document> filteredDocuments = FilterDocuments(documents, filterAndOrder);
            IEnumerable<Document> orderedAndFilteredDocuments = new List<Document>();

            switch (filterAndOrder.OrderBy)
            {
                case "Id":
                    orderedAndFilteredDocuments = OrderDocumentsAscOrDesc(filterAndOrder.IsDesc, filteredDocuments, x => x.Id);
                    break;
                case "Title":
                    orderedAndFilteredDocuments = OrderDocumentsAscOrDesc(filterAndOrder.IsDesc, filteredDocuments, x => x.Title);
                    break;
                case "CreationDate":
                    orderedAndFilteredDocuments = OrderDocumentsAscOrDesc(filterAndOrder.IsDesc, filteredDocuments, x => x.CreationDate);
                    break;
                case "LastModification":
                    orderedAndFilteredDocuments = OrderDocumentsAscOrDesc(filterAndOrder.IsDesc, filteredDocuments, x => x.LastModification);
                    break;
                default:
                    break;
            }

            return orderedAndFilteredDocuments;
        }

        private IEnumerable<Document> FilterDocuments(IEnumerable<Document> documents, DocumentFilterAndOrder filterAndOrder)
        {
            List<Document> filteredDocuments = new List<Document>();

            foreach (Document document in documents)
            {
                bool ok = true;
                Document filteredData = filterAndOrder.DocumentFilteredData;

                if (filteredData.Id != Guid.Parse("11111111-1111-1111-1111-111111111111"))
                {
                    ok = document.Id == filteredData.Id;
                }
                if (filteredData.Title != "")
                {
                    ok = ok && document.Title == filteredData.Title;
                }
                if (filteredData.CreationDate.Date != DateTime.MaxValue.Date)
                {
                    ok = ok && document.CreationDate.Date == filteredData.CreationDate.Date;
                }
                if (filteredData.LastModification.Date != DateTime.MaxValue.Date)
                {
                    ok = ok && document.LastModification.Date == filteredData.LastModification.Date;
                }

                if (ok)
                {
                    filteredDocuments.Add(document);
                }
            }

            return filteredDocuments;
        }

        private IEnumerable<Document> OrderDocumentsAscOrDesc<Document, TKey>(bool isDesc, IEnumerable<Document> documentsToOrder, Func<Document, TKey> keySelector)
        {
            if (isDesc)
            {
                return documentsToOrder.OrderByDescending(keySelector);
            }
            else
            {
                return documentsToOrder.OrderBy(keySelector);
            }
        }

        public Document GetById(Guid documentId)
        {
            if (DocumentRepository.Exists(documentId))
            {
                return DocumentRepository.GetById(documentId);
            }
            else
            {
                throw new MissingDocumentException("The document is not on the database.");
            }
        }

        public void Update(Guid documentId, Document newDocumentData)
        {
            if (DocumentRepository.Exists(documentId))
            {
                Document documentToUpdate = DocumentRepository.GetById(documentId);
                documentToUpdate.LastModification = DateTime.Now;
                if (newDocumentData.StyleClass != null && !StyleClassRepository.Exists(newDocumentData.StyleClass.Name))
                {
                    newDocumentData.StyleClass = null;
                }
                documentToUpdate.StyleClass = newDocumentData.StyleClass;
                documentToUpdate.Title = newDocumentData.Title;
                DocumentRepository.Update(documentId, documentToUpdate);
            }
            else
            {
                throw new MissingDocumentException("The document is not on the database.");
            }
        }
    }
}
