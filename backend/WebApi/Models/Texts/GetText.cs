using Domain;

namespace WebApi.Models.Texts
{
    public class GetText : Model<Text, GetText>
    {
        public Content ContentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }
        public string TextContent { get; set; }

        public GetText()
        {

        }

        public GetText(Text text)
        {
            SetModel(text);
        }

        public override Text ToEntity() => new Text()
        {
            ContentThatBelongs = this.ContentThatBelongs,
            StyleClass = this.StyleClass,
            TextContent = this.TextContent
        };

        protected override GetText SetModel(Text text)
        {
            ContentThatBelongs = text.ContentThatBelongs;
            StyleClass = text.StyleClass;
            TextContent = text.TextContent;

            return this;
        }
    }
}