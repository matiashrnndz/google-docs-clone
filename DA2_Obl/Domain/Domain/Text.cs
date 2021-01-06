using System;

namespace Domain
{
    public class Text
    {
        public Guid Id { get; set; }
        public StyleClass StyleClass { get; set; }
        public Content ContentThatBelongs { get; set; }
        public string TextContent { get; set; }
        public int Position { get; set; }

        public Text()
        {
            StyleClass = null;
            ContentThatBelongs = null;
            TextContent = "";
            Position = 0;
        }
    }
}
