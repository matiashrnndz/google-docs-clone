using Domain;

namespace WebApi.Models.Texts
{
    public class AddText : Model<Text, AddText>
    {
        public StyleClass StyleClass { get; set; }
        public string TextContent { get; set; }

        public AddText()
        {

        }

        public AddText(Text text)
        {
            SetModel(text);
        }

        public override Text ToEntity() => new Text()
        {
            StyleClass = this.StyleClass,
            TextContent = this.TextContent,
        };

        protected override AddText SetModel(Text text)
        {
            StyleClass = text.StyleClass;
            TextContent = text.TextContent;

            return this;
        }
    }

}