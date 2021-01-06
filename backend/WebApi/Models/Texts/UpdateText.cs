using Domain;

namespace WebApi.Models.Texts
{
    public class UpdateText : Model<Text, UpdateText>
    {
        public StyleClass StyleClass { get; set; }
        public string TextContent { get; set; }
        public int Position { get; set; }

        public UpdateText()
        {

        }

        public UpdateText(Text text)
        {
            SetModel(text);
        }

        public override Text ToEntity() => new Text()
        {
            StyleClass = this.StyleClass,
            TextContent = this.TextContent,
            Position = this.Position
        };

        protected override UpdateText SetModel(Text text)
        {
            StyleClass = text.StyleClass;
            TextContent = text.TextContent;
            Position = text.Position;

            return this;
        }
    }
}