using Domain;

namespace WebApi.Models.Comments
{
    public class AddDocument : Model<Document, AddDocument>
    {
        public string Title { get; set; }
        public StyleClass StyleClass { get; set; }

        public AddDocument()
        {

        }

        public AddDocument(Document document)
        {
            SetModel(document);
        }

        public override Document ToEntity() => new Document()
        {
            StyleClass = this.StyleClass,
            Title = this.Title

        };

        protected override AddDocument SetModel(Document document)
        {
            StyleClass = document.StyleClass;
            Title = document.Title;

            return this;
        }
    }
}