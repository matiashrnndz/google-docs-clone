using Domain;
using System;

namespace WebApi.Models.Texts
{
    public class BaseText : Model<Text, BaseText>
    {
        public Guid Id { get; set; }
        public Content ContentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }
        public string TextContent { get; set; }
        public int Position { get; set; }

        public BaseText()
        {

        }

        public BaseText(Text text)
        {
            SetModel(text);
        }

        public override Text ToEntity() => new Text()
        {
            Id = this.Id,
            ContentThatBelongs = this.ContentThatBelongs,
            StyleClass = this.StyleClass,
            TextContent = this.TextContent,
            Position = this.Position
        };

        protected override BaseText SetModel(Text text)
        {
            Id = text.Id;
            ContentThatBelongs = text.ContentThatBelongs;
            StyleClass = text.StyleClass;
            TextContent = text.TextContent;
            Position = text.Position;

            return this;
        }
    }
}