using Domain;

namespace WebApi.Models.Paragraphs
{
    public class GetParagraph : Model<Paragraph, GetParagraph>
    {
        public Document DocumentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }
        public int Position { get; set; }

        public GetParagraph()
        {

        }

        public GetParagraph(Paragraph paragraph)
        {
            SetModel(paragraph);
        }

        public override Paragraph ToEntity() => new Paragraph()
        {
            DocumentThatBelongs = this.DocumentThatBelongs,
            StyleClass = this.StyleClass,
            Position = this.Position
        };

        protected override GetParagraph SetModel(Paragraph paragraph)
        {
            DocumentThatBelongs = paragraph.DocumentThatBelongs;
            StyleClass = paragraph.StyleClass;
            Position = paragraph.Position;

            return this;
        }
    }
}