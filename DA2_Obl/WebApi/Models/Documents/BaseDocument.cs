using Domain;
using System;

namespace WebApi.Models.Comments
{
    public class BaseDocument : Model<Document, BaseDocument>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public User Creator { get; set; }
        public StyleClass StyleClass { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModification { get; set; }

        public BaseDocument()
        {

        }

        public BaseDocument(Document document)
        {
            SetModel(document);
        }

        public override Document ToEntity() => new Document()
        {
            Id = this.Id,
            Title = this.Title,
            Creator = this.Creator,
            StyleClass = this.StyleClass,
            CreationDate = this.CreationDate,
            LastModification = this.LastModification

        };

        protected override BaseDocument SetModel(Document document)
        {
            Id = document.Id;
            Title = document.Title;
            Creator = document.Creator;

            if (document.StyleClass == null)
            {
                StyleClass = new StyleClass();
            }
            else
            {
                StyleClass = document.StyleClass;
            }

            if (document.CreationDate == null)
            {
                CreationDate = new DateTime();
            }
            else
            {
                CreationDate = document.CreationDate;
            }

            if (document.LastModification == null)
            {
                LastModification = new DateTime();
            }
            else
            {
                LastModification = document.LastModification;
            }

            return this;
        }
    }
}