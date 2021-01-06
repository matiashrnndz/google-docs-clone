using Domain;

namespace WebApi.Models.Footers
{
    public class AddFooter : Model<Footer, AddFooter>
    {
        public StyleClass StyleClass { get; set; }

        public AddFooter()
        {

        }

        public AddFooter(Footer footer)
        {
            SetModel(footer);
        }

        public override Footer ToEntity() => new Footer()
        {
            StyleClass = this.StyleClass
        };

        protected override AddFooter SetModel(Footer footer)
        {
            StyleClass = footer.StyleClass;

            return this;
        }
    }
}