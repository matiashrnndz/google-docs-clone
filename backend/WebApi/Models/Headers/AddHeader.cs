using Domain;

namespace WebApi.Models.Headers
{
    public class AddHeader : Model<Header, AddHeader>
    {
        public StyleClass StyleClass { get; set; }

        public AddHeader()
        {

        }

        public AddHeader(Header header)
        {
            SetModel(header);
        }

        public override Header ToEntity() => new Header()
        {
            StyleClass = this.StyleClass
        };

        protected override AddHeader SetModel(Header header)
        {
            StyleClass = header.StyleClass;

            return this;
        }
    }
}