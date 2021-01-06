using Domain;

namespace WebApi.Models.Footers
{
    public class GetFooter : Model<Footer, GetFooter>
    {
        public Document DocumentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }

        public GetFooter()
        {

        }

        public GetFooter(Footer footer)
        {
            SetModel(footer);
        }

        public override Footer ToEntity() => new Footer()
        {
            DocumentThatBelongs = this.DocumentThatBelongs,
            StyleClass = this.StyleClass
        };

        protected override GetFooter SetModel(Footer footer)
        {
            DocumentThatBelongs = footer.DocumentThatBelongs;
            StyleClass = footer.StyleClass;

            return this;
        }
    }
}