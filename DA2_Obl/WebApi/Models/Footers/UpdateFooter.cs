using Domain;

namespace WebApi.Models.Footers
{
    public class UpdateFooter : Model<Footer, UpdateFooter>
    {
        public StyleClass StyleClass { get; set; }

        public UpdateFooter()
        {

        }

        public UpdateFooter(Footer footer)
        {
            SetModel(footer);
        }

        public override Footer ToEntity() => new Footer()
        {
            StyleClass = this.StyleClass
        };

        protected override UpdateFooter SetModel(Footer footer)
        {
            StyleClass = footer.StyleClass;

            return this;
        }
    }
}