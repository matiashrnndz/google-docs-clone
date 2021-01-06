using Domain;
using Exception;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceImp
{
    public class TextManagementService : ITextManagementService
    {
        internal ITextRepository TextRepository { get; set; }
        internal IContentRepository ContentRepository { get; set; }
        internal IHeaderRepository HeaderRepository { get; set; }
        internal IParagraphRepository ParagraphRepository { get; set; }
        internal IFooterRepository FooterRepository { get; set; }
        internal IStyleClassRepository StyleClassRepository { get; set; }

        public Text GetById(Guid textId)
        {
            if (TextRepository.Exists(textId))
            {
                return TextRepository.GetById(textId);
            }
            else
            {
                throw new MissingTextException("This text is not in the database.");
            }
        }

        public Text GetByHeader(Guid headerId)
        {
            if (HeaderRepository.Exists(headerId))
            {
                Header headerForText = HeaderRepository.GetById(headerId);
                Content contentOfHeader = headerForText.Content;
                IEnumerable<Text> headerTexts = TextRepository.GetByContent(contentOfHeader);
                if (headerTexts.Count() != 0)
                {
                    return headerTexts.First();
                }
                else
                {
                    throw new MissingTextException("This text is not in the database");
                }
            }
            else
            {
                throw new MissingHeaderException("This header does not exist in the database");
            }

        }

        public IEnumerable<Text> GetAllByParagraph(Guid paragraphId)
        {
            if (ParagraphRepository.Exists(paragraphId))
            {
                Paragraph paragraphForText = ParagraphRepository.GetById(paragraphId);
                Content contentFromParagraph = paragraphForText.Content;
                IEnumerable<Text> paragraphTexts = TextRepository.GetByContent(contentFromParagraph);
                return paragraphTexts;
            }
            else
            {
                throw new MissingParagraphException("The paragraph is not in the database.");
            }
        }

        public Text GetByFooter(Guid footerId)
        {
            if (FooterRepository.Exists(footerId))
            {
                Footer footerForText = FooterRepository.GetById(footerId);
                Content contentOfFooter = footerForText.Content;
                IEnumerable<Text> footerTexts = TextRepository.GetByContent(contentOfFooter);
                if (footerTexts.Count() != 0)
                {
                    return footerTexts.First();
                }
                else
                {
                    throw new MissingTextException("This text is not in the database");
                }
            }
            else
            {
                throw new MissingFooterException("This footer does not exist in the database");
            }

        }

        public void Update(Guid textId, Text newTextData)
        {
            if (TextRepository.Exists(textId))
            {
                Text textToUpdate = TextRepository.GetById(textId);
                if (textToUpdate.Position != newTextData.Position)
                {
                    IEnumerable<Text> textsInContent = TextRepository.GetByContent(textToUpdate.ContentThatBelongs);
                    int newPosition = newTextData.Position;
                    int maxPosition = textsInContent.Count() - 1;
                    if (newPosition > maxPosition || newPosition < 0)
                    {
                        throw new InvalidPositionException("This position is not valid for the content in question.");
                    }
                    else
                    {
                        Text swappedText = textsInContent.First(p => p.Position == newPosition);
                        swappedText.Position = textToUpdate.Position;
                        TextRepository.Update(swappedText);
                        textToUpdate.Position = newPosition;
                    }

                }
                if (newTextData.StyleClass != null && !StyleClassRepository.Exists(newTextData.StyleClass.Name))
                {
                    newTextData.StyleClass = null;
                }
                textToUpdate.StyleClass = newTextData.StyleClass;
                textToUpdate.TextContent = newTextData.TextContent;
                TextRepository.Update(textToUpdate);
            }
            else
            {
                throw new MissingParagraphException("This paragraph is not in the database.");
            }
        }

        public Text AddToHeader(Guid headerId, Text text)
        {
            if (HeaderRepository.Exists(headerId))
            {
                Header header = HeaderRepository.GetById(headerId);
                header.StyleClass = null;

                Header headerForText = header;

                Content contentOfHeader = headerForText.Content;

                IEnumerable<Text> headerTexts = TextRepository.GetByContent(contentOfHeader);

                if (headerTexts.Count() == 0)
                {
                    text.Id = Guid.NewGuid();
                    text.Position = 0;
                    text.ContentThatBelongs = contentOfHeader;
                    TextRepository.Add(text);

                    return text;
                }
                else
                {
                    throw new ExistingTextException("There is an existing text in the selected header.");
                }
            }
            else
            {
                throw new MissingHeaderException("This header does not exist in the database");
            }
        }
        public Text AddToParagraph(Guid paragraphId, Text text)
        {
            if (ParagraphRepository.Exists(paragraphId))
            {
                Paragraph paragraph = ParagraphRepository.GetById(paragraphId);
                paragraph.StyleClass = null;

                Paragraph paragraphForText = paragraph;

                Content contentOfParagraph = paragraphForText.Content;
                IEnumerable<Text> paragraphTexts = TextRepository.GetByContent(contentOfParagraph);
                text.Id = Guid.NewGuid();
                text.Position = paragraphTexts.Count();
                text.ContentThatBelongs = contentOfParagraph;
                TextRepository.Add(text);

                return text;

            }
            else
            {
                throw new MissingParagraphException("This paragraph does not exist in the database");
            }
        }
        public Text AddToFooter(Guid footerId, Text text)
        {
            if (FooterRepository.Exists(footerId))
            {
                Footer footer = FooterRepository.GetById(footerId);
                footer.StyleClass = null;

                Footer footerForText = footer;

                Content contentOfFooter = footerForText.Content;
                IEnumerable<Text> footerTexts = TextRepository.GetByContent(contentOfFooter);
                if (footerTexts.Count() == 0)
                {
                    text.Id = Guid.NewGuid();
                    text.Position = 0;
                    text.ContentThatBelongs = contentOfFooter;
                    TextRepository.Add(text);

                    return text;
                }
                else
                {
                    throw new ExistingTextException("There is an existing text in the selected footer.");
                }
            }
            else
            {
                throw new MissingFooterException("This footer does not exist in the database");
            }
        }

        public void Delete(Guid textId)
        {
            if (TextRepository.Exists(textId))
            {
                Text textToDelete = TextRepository.GetById(textId);
                IEnumerable<Text> textsInContent = TextRepository.GetByContent(textToDelete.ContentThatBelongs);
                foreach (Text textInContent in textsInContent)
                {
                    if(textInContent.Position > textToDelete.Position)
                    {
                        textInContent.Position--;
                        TextRepository.Update(textInContent);
                    }
                }
                TextRepository.Delete(textId);
            }
            else
            {
                throw new MissingTextException("This text is not in the database");
            }
        }
    }
}
