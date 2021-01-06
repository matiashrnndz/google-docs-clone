using Repository;
using Service;
using Domain;
using Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImp
{
    public class DocumentModificationLogService : IDocumentModificationLogService
    {
        internal IDocumentRepository DocumentRepository { get; set; }
        internal IDocumentModificationLogRepository DocumentModificationLogRepository { get; set; }
        internal IHeaderRepository HeaderRepository { get; set; }
        internal IFooterRepository FooterRepository { get; set; }
        internal IParagraphRepository ParagraphRepository { get; set; }
        internal ITextRepository TextRepository { get; set; }
        internal IContentRepository ContentRepository { get; set; }

        public void LogModificationToDocument(Guid documentId)
        {
            Document modifiedDocument = DocumentRepository.GetById(documentId);
            DateTime modificationDate = DateTime.Now;
            DocumentModificationLog modificationLog = new DocumentModificationLog
            {
                DateOfModification = modificationDate,
                Document = modifiedDocument,
                Id = Guid.NewGuid()
            };
            modifiedDocument.LastModification = modificationDate;
            DocumentRepository.Update(modifiedDocument.Id, modifiedDocument);
            DocumentModificationLogRepository.Add(modificationLog);
        }

        public void LogModificationToHeader(Guid headerId)
        {
            if (HeaderRepository.Exists(headerId))
            {
                Header headerOfModdedDocument = HeaderRepository.GetById(headerId);
                LogModificationToDocument(headerOfModdedDocument.DocumentThatBelongs.Id);
            } else
            {
                throw new MissingHeaderException("This header is not in the database.");
            }
        }

        public void LogModificationToFooter(Guid footerId)
        {
            if (FooterRepository.Exists(footerId))
            {
                Footer footerOfModdedDocument = FooterRepository.GetById(footerId);
                LogModificationToDocument(footerOfModdedDocument.DocumentThatBelongs.Id);
            }else
            {
                throw new MissingFooterException("This footer is not in the database.");
            }
        }

        public void LogModificationToParagraph(Guid paragraphId)
        {
            if (ParagraphRepository.Exists(paragraphId))
            {
                Paragraph paragraphOfModdedDocument = ParagraphRepository.GetById(paragraphId);
                LogModificationToDocument(paragraphOfModdedDocument.DocumentThatBelongs.Id);
            } else
            {
                throw new MissingParagraphException("This paragraph is not in the database.");
            }
        }

        public void LogModificationToText(Guid textId)
        {
            if (TextRepository.Exists(textId))
            {
                Text textOfModdedDocument = TextRepository.GetById(textId);
                Content contentOfModdedDocument = textOfModdedDocument.ContentThatBelongs;
                if (HeaderRepository.ExistsWithContent(contentOfModdedDocument))
                {
                    Header headerOfModdedDocument = HeaderRepository.GetByContent(contentOfModdedDocument);
                    LogModificationToDocument(headerOfModdedDocument.DocumentThatBelongs.Id);
                }
                else if (ParagraphRepository.ExistsWithContent(contentOfModdedDocument))
                {
                    Paragraph paragraphOfModdedDocument = ParagraphRepository.GetByContent(contentOfModdedDocument);
                    LogModificationToDocument(paragraphOfModdedDocument.DocumentThatBelongs.Id);
                }
                else if (FooterRepository.ExistsWithContent(contentOfModdedDocument))
                {
                    Footer footerOfModdedDocument = FooterRepository.GetByContent(contentOfModdedDocument);
                    LogModificationToDocument(footerOfModdedDocument.DocumentThatBelongs.Id);
                }
            } else
            {
                throw new MissingTextException("This text is not in the database.");
            }
        }

    }
}
