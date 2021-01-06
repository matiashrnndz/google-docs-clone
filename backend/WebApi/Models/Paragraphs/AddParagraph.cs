using Domain;

namespace WebApi.Models.Paragraphs
{
    public class AddParagraph : Model<Paragraph, AddParagraph>
    {
        public StyleClass StyleClass { get; set; }

        public AddParagraph()
        {

        }

        public AddParagraph(Paragraph paragraph)
        {
            SetModel(paragraph);
        }

        public override Paragraph ToEntity() => new Paragraph()
        {
            StyleClass = this.StyleClass,
        };

        protected override AddParagraph SetModel(Paragraph paragraph)
        {
            StyleClass = paragraph.StyleClass;

            return this;
        }
    }
}