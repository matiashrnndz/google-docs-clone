using Domain;
using System;

namespace WebApi.Models.Paragraphs
{
    public class BaseParagraph : Model<Paragraph, BaseParagraph>
    {
        public Guid Id { get; set; }
        public Document DocumentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }
        public int Position { get; set; }

        public BaseParagraph()
        {

        }

        public BaseParagraph(Paragraph paragraph)
        {
            SetModel(paragraph);
        }

        public override Paragraph ToEntity() => new Paragraph()
        {
            Id = this.Id,
            DocumentThatBelongs = this.DocumentThatBelongs,
            StyleClass = this.StyleClass,
            Position = this.Position
        };

        protected override BaseParagraph SetModel(Paragraph paragraph)
        {
            Id = paragraph.Id;
            DocumentThatBelongs = paragraph.DocumentThatBelongs;
            StyleClass = paragraph.StyleClass;
            Position = paragraph.Position;

            return this;
        }
    }
}