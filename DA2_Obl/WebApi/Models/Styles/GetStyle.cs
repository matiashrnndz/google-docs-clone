using Domain;

namespace WebApi.Models.Styles
{
    public class GetStyle : Model<Style, GetStyle>
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public GetStyle()
        {

        }

        public GetStyle(Style style)
        {
            SetModel(style);
        }

        public override Style ToEntity() => new Style()
        {
            Key = this.Key,
            Value = this.Value
        };

        protected override GetStyle SetModel(Style style)
        {
            Key = style.Key;
            Value = style.Value;

            return this;
        }
    }
}