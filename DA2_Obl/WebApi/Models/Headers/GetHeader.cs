using Domain;

namespace WebApi.Models.Headers
{
    public class GetHeader : Model<Header, GetHeader>
    {
        public Document DocumentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }

        public GetHeader()
        {

        }

        public GetHeader(Header header)
        {
            SetModel(header);
        }

        public override Header ToEntity() => new Header()
        {
            DocumentThatBelongs = this.DocumentThatBelongs,
            StyleClass = this.StyleClass
        };

        protected override GetHeader SetModel(Header header)
        {
            DocumentThatBelongs = header.DocumentThatBelongs;
            StyleClass = header.StyleClass;

            return this;
        }
    }
}