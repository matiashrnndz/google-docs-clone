using Domain;

namespace WebApi.Models.Paragraphs
{
    public class UpdateParagraph : Model<Paragraph, UpdateParagraph>
    {
        public StyleClass StyleClass { get; set; }
        public int Position { get; set; }

        public UpdateParagraph()
        {

        }

        public UpdateParagraph(Paragraph paragraph)
        {
            SetModel(paragraph);
        }

        public override Paragraph ToEntity() => new Paragraph()
        {
            StyleClass = this.StyleClass,
            Position = this.Position
        };

        protected override UpdateParagraph SetModel(Paragraph paragraph)
        {
            StyleClass = paragraph.StyleClass;
            Position = paragraph.Position;

            return this;
        }
    }
}