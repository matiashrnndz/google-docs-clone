using System;

namespace Domain
{
    public class Header
    {
        public Guid Id { get; set; }
        public Document DocumentThatBelongs { get; set; }
        public Content Content { get; set; }
        public StyleClass StyleClass { get; set; }

        public Header()
        {
            DocumentThatBelongs = null;
            StyleClass = null;
            Content = null;
        }
    }
}
