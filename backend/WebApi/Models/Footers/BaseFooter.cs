using Domain;
using System;

namespace WebApi.Models.Footers
{
    public class BaseFooter : Model<Footer, BaseFooter>
    {
        public Guid Id { get; set; }
        public Document DocumentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }

        public BaseFooter()
        {

        }

        public BaseFooter(Footer footer)
        {
            SetModel(footer);
        }

        public override Footer ToEntity() => new Footer()
        {
            Id = this.Id,
            DocumentThatBelongs = this.DocumentThatBelongs,
            StyleClass = this.StyleClass
        };

        protected override BaseFooter SetModel(Footer footer)
        {
            Id = footer.Id;
            DocumentThatBelongs = footer.DocumentThatBelongs;
            StyleClass = footer.StyleClass;

            return this;
        }
    }
}