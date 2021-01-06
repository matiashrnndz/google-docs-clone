using Domain;

namespace WebApi.Models.Comments
{
    public class UpdateDocument : Model<Document, UpdateDocument>
    {
        public string Title { get; set; }
        public StyleClass StyleClass { get; set; }


        public UpdateDocument()
        {

        }

        public UpdateDocument(Document document)
        {
            SetModel(document);
        }

        public override Document ToEntity() => new Document()
        {
            Title = this.Title,
            StyleClass = this.StyleClass

        };

        protected override UpdateDocument SetModel(Document document)
        {
            Title = document.Title;
            StyleClass = document.StyleClass;

            return this;
        }
    }
}