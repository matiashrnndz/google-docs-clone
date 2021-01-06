using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceImp
{
    public class ParagraphManagementService : IParagraphManagementService
    {
        internal IParagraphRepository ParagraphRepository { get; set; }
        internal IDocumentRepository DocumentRepository { get; set; }
        internal IStyleClassRepository StyleClassRepository { get; set; }
        internal IContentRepository ContentRepository { get; set; }

        public IEnumerable<Paragraph> GetAllByDocument(Guid documentId)
        {
            if (DocumentRepository.Exists(documentId))
            {
                return ParagraphRepository.GetAllByDocument(documentId);
            }
            else
            {
                throw new MissingDocumentException("The document doesn't exist in the database.");
            }
        }

        public Paragraph Add(Guid documentId, Paragraph paragraph)
        {
            if (DocumentRepository.Exists(documentId))
            {
                Document documentThatBelongs = DocumentRepository.GetById(documentId);
                documentThatBelongs.StyleClass = null;

                paragraph.DocumentThatBelongs = documentThatBelongs;

                paragraph.Id = Guid.NewGuid();

                paragraph.Content = new Content()
                {
                    Id = Guid.NewGuid()
                };

                IEnumerable<Paragraph> existingParagraphs = ParagraphRepository.GetAllByDocument(documentId);
                int newMaxPosition = existingParagraphs.Count();

                paragraph.Position = newMaxPosition;

                if (paragraph.StyleClass != null && !StyleClassRepository.Exists(paragraph.StyleClass.Name))
                {
                    paragraph.StyleClass = null;
                }

                ContentRepository.Add(paragraph.Content);
                ParagraphRepository.Add(paragraph);

                return paragraph;
            }
            else
            {
                throw new MissingDocumentException("This document is not in the database.");
            }
        }

        public void Update(Guid paragraphId, Paragraph newParagraphData)
        {
            if (ParagraphRepository.Exists(paragraphId))
            {
                Paragraph paragraphToUpdate = ParagraphRepository.GetById(paragraphId);
                if (paragraphToUpdate.Position != newParagraphData.Position)
                {
                    IEnumerable<Paragraph> paragraphsInDocument = ParagraphRepository.GetAllByDocument(paragraphToUpdate.DocumentThatBelongs.Id);
                    int newPosition = newParagraphData.Position;
                    int maxPosition = paragraphsInDocument.Count() - 1;
                    if (newPosition > maxPosition || newPosition < 0)
                    {
                        throw new InvalidPositionException("This position is not valid for the document in question.");
                    }
                    else
                    {
                        Paragraph swappedParagraph = paragraphsInDocument.First(p => p.Position == newPosition);
                        swappedParagraph.Position = paragraphToUpdate.Position;
                        ParagraphRepository.Update(swappedParagraph);
                        paragraphToUpdate.Position = newPosition;
                    }

                }
                if (newParagraphData.StyleClass != null && !StyleClassRepository.Exists(newParagraphData.StyleClass.Name))
                {
                    newParagraphData.StyleClass = null;
                }
                paragraphToUpdate.StyleClass = newParagraphData.StyleClass;
                ParagraphRepository.Update(paragraphToUpdate);
            }
            else
            {
                throw new MissingParagraphException("This paragraph is not in the database.");
            }
        }

        public void Delete(Guid paragraphId)
        {
            if (ParagraphRepository.Exists(paragraphId))
            {
                Paragraph paragraphToDelete = ParagraphRepository.GetById(paragraphId);
                IEnumerable<Paragraph> paragraphsInDocument = ParagraphRepository.GetAllByDocument(paragraphToDelete.DocumentThatBelongs.Id);
                foreach (Paragraph paragraphInDocument in paragraphsInDocument)
                {
                    if (paragraphInDocument.Position > paragraphToDelete.Position)
                    {
                        paragraphInDocument.Position--;
                        ParagraphRepository.Update(paragraphInDocument);
                    }
                }
                ParagraphRepository.Delete(paragraphId);
            }
            else
            {
                throw new MissingParagraphException("This paragraph is not in the database");
            }
        }
    }
}
