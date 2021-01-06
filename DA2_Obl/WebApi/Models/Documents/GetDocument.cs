using Domain;
using System;

namespace WebApi.Models.Comments
{
    public class GetDocument : Model<Document, GetDocument>
    {
        public User Creator { get; set; }
        public string Title { get; set; }
        public StyleClass StyleClass { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModification { get; set; }

        public GetDocument()
        {

        }

        public GetDocument(Document document)
        {
            SetModel(document);
        }

        public override Document ToEntity() => new Document()
        {
            Creator = this.Creator,
            Title = this.Title,
            StyleClass = this.StyleClass,
            CreationDate = this.CreationDate,
            LastModification = this.LastModification

        };

        protected override GetDocument SetModel(Document document)
        {
            Creator = document.Creator;
            Title = document.Title;
            StyleClass = document.StyleClass;
            CreationDate = document.CreationDate;
            LastModification = LastModification;

            return this;
        }
    }
}