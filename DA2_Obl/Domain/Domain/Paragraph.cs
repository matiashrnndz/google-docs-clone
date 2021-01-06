using System;

namespace Domain
{
    public class Paragraph
    {
        public Guid Id { get; set; }
        public Document DocumentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }
        public Content Content { get; set; }
        public int Position { get; set; }

        public Paragraph()
        {
            DocumentThatBelongs = null;
            StyleClass = null;
            Content = null;
            Position = 0;
        }
    }
}
