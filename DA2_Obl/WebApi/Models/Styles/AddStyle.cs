using Domain;

namespace WebApi.Models.Styles
{
    public class AddStyle : Model<Style, AddStyle>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public AddStyle()
        {

        }

        public AddStyle(Style style)
        {
            SetModel(style);
        }

        public override Style ToEntity() => new Style()
        {
            Key = this.Key,
            Value = this.Value
        };

        protected override AddStyle SetModel(Style style)
        {
            Key = style.Key;
            Value = style.Value;

            return this;
        }
    }
}