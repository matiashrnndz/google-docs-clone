using Domain;

namespace WebApi.Models.Headers
{
    public class UpdateHeader : Model<Header, UpdateHeader>
    {
        public StyleClass StyleClass { get; set; }

        public UpdateHeader()
        {

        }

        public UpdateHeader(Header header)
        {
            SetModel(header);
        }

        public override Header ToEntity() => new Header()
        {
            StyleClass = this.StyleClass
        };

        protected override UpdateHeader SetModel(Header header)
        {
            StyleClass = header.StyleClass;

            return this;
        }
    }
}