using Domain;
using System;

namespace WebApi.Models.Headers
{
    public class BaseHeader : Model<Header, BaseHeader>
    {
        public Guid Id { get; set; }
        public Document DocumentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }

        public BaseHeader()
        {

        }

        public BaseHeader(Header header)
        {
            SetModel(header);
        }

        public override Header ToEntity() => new Header()
        {
            Id = this.Id,
            DocumentThatBelongs = this.DocumentThatBelongs,
            StyleClass = this.StyleClass
        };

        protected override BaseHeader SetModel(Header header)
        {
            Id = header.Id;
            DocumentThatBelongs = header.DocumentThatBelongs;
            StyleClass = header.StyleClass;

            return this;
        }
    }
}