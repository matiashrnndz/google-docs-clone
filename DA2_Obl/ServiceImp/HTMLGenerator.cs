using Domain;
using Repository;
using Service;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ServiceImp
{
    public class HTMLGenerator : ICodeGenerator
    {
        internal IDocumentRepository DocumentRepository { get; set; }
        internal IHeaderRepository HeaderRepository { get; set; }
        internal IFooterRepository FooterRepository { get; set; }
        internal IParagraphRepository ParagraphRepository { get; set; }
        internal IContentRepository ContentRepository { get; set; }
        internal ITextRepository TextRepository { get; set; }
        internal IStyleClassRepository StyleClassRepository { get; set; }
        internal IFormatRepository FormatRepository { get; set; }
        internal IStyleRepository StyleRepository { get; set; }
        internal StyleHTMLBuilder StyleHTMLBuilder { get; set; }

        public string GenerateHTML(Document document, Format format)
        {
            return ApplyDocumentStyle(document, format);
        }

        private string ApplyDocumentStyle(Document document, Format format)
        {
            IEnumerable<Style> currentStyles = GetStylesWithInheritance(document.StyleClass, format);

            string appliedHtmlCode = "";

            if (!HeaderRepository.ExistsForDocument(document.Id) && !FooterRepository.ExistsForDocument(document.Id))
            {
                appliedHtmlCode = "" + ApplyParagraphStyles(ParagraphRepository.GetAllByDocument(document.Id), format, currentStyles);
            }
            else if (!FooterRepository.ExistsForDocument(document.Id))
            {
                appliedHtmlCode = "" + ApplyHeaderStyle(HeaderRepository.GetByDocument(document.Id), format, currentStyles)
                + "<br>" + ApplyParagraphStyles(ParagraphRepository.GetAllByDocument(document.Id), format, currentStyles);
            }
            else
            {
                appliedHtmlCode = "" + ApplyHeaderStyle(HeaderRepository.GetByDocument(document.Id), format, currentStyles)
                    + "<br>" + ApplyParagraphStyles(ParagraphRepository.GetAllByDocument(document.Id), format, currentStyles)
                    + "<br>" + ApplyFooterStyles(FooterRepository.GetByDocument(document.Id), format, currentStyles);
            }

            return appliedHtmlCode;
        }

        private string ApplyFooterStyles(Footer footer, Format format, IEnumerable<Style> formerStyles)
        {
            IEnumerable<Style> currentStyles = GetStylesWithInheritance(footer.StyleClass, format);
            currentStyles = MergeStyles(currentStyles, formerStyles);

            IEnumerable<Text> textsToApply = TextRepository.GetByContent(footer.Content);
            string appliedHtmlCode = "" + ApplyTextStyles(textsToApply, format, currentStyles);
            return appliedHtmlCode;
        }

        private string ApplyParagraphStyles(IEnumerable<Paragraph> paragraphs, Format format, IEnumerable<Style> formerStyles)
        {
            paragraphs = paragraphs.OrderBy(p => p.Position);
            string appliedHtmlCode = "";
            for (int i = 0; i < paragraphs.Count(); i++)
            {
                Paragraph selectedParagraph = paragraphs.ElementAt(i);
                IEnumerable<Style> currentStyles = GetStylesWithInheritance(selectedParagraph.StyleClass, format);
                currentStyles = MergeStyles(currentStyles, formerStyles);

                IEnumerable<Text> textsToApply = TextRepository.GetByContent(selectedParagraph.Content);
                string textsWithGeneratedHtml = ApplyTextStyles(textsToApply, format, currentStyles);
                
                appliedHtmlCode = appliedHtmlCode + "<br>" + textsWithGeneratedHtml;
            }
            return appliedHtmlCode;
        }

        private string ApplyHeaderStyle(Header header, Format format, IEnumerable<Style> formerStyles)
        {
            IEnumerable<Style> currentStyles = GetStylesWithInheritance(header.StyleClass, format);
            currentStyles = MergeStyles(currentStyles, formerStyles);

            IEnumerable<Text> textsToApply = TextRepository.GetByContent(header.Content);
            string appliedHtmlCode = "" + ApplyTextStyles(textsToApply, format, currentStyles);
            return appliedHtmlCode;
        }

        private IEnumerable<Style> MergeStyles(IEnumerable<Style> currentStyles, IEnumerable<Style> formerStyles)
        {
            List<Style> mergedStyles = new List<Style>();
            foreach(Style currentStyle in currentStyles)
            {
                mergedStyles.Add(currentStyle);
            }
            foreach(Style formerStyle in formerStyles)
            {
                if(currentStyles.Where(x => x.Key == formerStyle.Key).Count() == 0)
                {
                    mergedStyles.Add(formerStyle);
                }
            }
            return mergedStyles.AsEnumerable();
        }

        private string ApplyTextStyles(IEnumerable<Text> texts, Format format, IEnumerable<Style> formerStyles)
        {
            texts = texts.OrderBy(t => t.Position);
            string appliedHtmlCode = "";
            for (int i = 0; i < texts.Count(); i++)
            {
                Text selectedText = texts.ElementAt(i);
                IEnumerable<Style> currentStyles = GetStylesWithInheritance(selectedText.StyleClass, format);
                currentStyles = MergeStyles(currentStyles, formerStyles);
                IEnumerable<string> textHtml = GetHTMLforStyles(currentStyles);
                appliedHtmlCode = appliedHtmlCode + "<br>" + textHtml.ElementAt(0) + selectedText.TextContent + textHtml.ElementAt(1);
            }
            return appliedHtmlCode;
        }

        private IEnumerable<string> GetHTMLforStyles(IEnumerable<Style> styles)
        {
            List<string> ret = new List<string>();

            string leftCode = "";
            string rightCode = "";

            
           StyleHTML styleHtmlCode = StyleHTMLBuilder.ConvertToHTML(styles);
           leftCode = styleHtmlCode.LeftHTMLCode;
           rightCode = styleHtmlCode.RightHTMLCode;
 
            ret.Add(leftCode);
            ret.Add(rightCode);
            return ret;
        }

        private IEnumerable<Style> GetStylesWithInheritance(StyleClass styleClass, Format format)
        {
            List<StyleClass> inheritedClasses = new List<StyleClass>();
            StyleClass selectedClass = styleClass;
            while (selectedClass != null)
            {
                inheritedClasses.Add(selectedClass);
                selectedClass = selectedClass.BasedOn;
            }
            List<Style> inheritedStyles = new List<Style>();
            for (int i = inheritedClasses.Count - 1; i >= 0; i--)
            {
                StyleClass inheritedClass = inheritedClasses.ElementAt(i);
                List<Style> selectedClassStyles = StyleRepository.GetStyles(inheritedClass.Name, format.Name).ToList();
                foreach (Style style in selectedClassStyles)
                {
                    if (inheritedStyles.Exists(s => s.Key.Equals(style.Key)))
                    {
                        inheritedStyles.RemoveAll(s => s.Key.Equals(style.Key));
                    }
                    inheritedStyles.Add(style);
                }
            }
            return inheritedStyles.AsEnumerable();
        }
    }
}
