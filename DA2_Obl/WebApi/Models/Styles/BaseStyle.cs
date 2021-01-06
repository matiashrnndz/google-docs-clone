using Domain;
using System;

namespace WebApi.Models.Styles
{
    public class BaseStyle : Model<Style, BaseStyle>
    {
        public Guid Id { get; set; }
        public Format Format { get; set; }
        public StyleClass StyleClass { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public BaseStyle()
        {

        }

        public BaseStyle(Style style)
        {
            SetModel(style);
        }

        public override Style ToEntity() => new Style()
        {
            Id = this.Id,
            Format = this.Format,
            StyleClass = this.StyleClass,
            Key = this.Key,
            Value = this.Value
        };

        protected override BaseStyle SetModel(Style style)
        {
            Id = style.Id;
            Format = style.Format;
            StyleClass = style.StyleClass;
            Key = style.Key;
            Value = style.Value;

            return this;
        }
    }
}