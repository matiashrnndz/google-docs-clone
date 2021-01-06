using System;

namespace Domain
{
    public class Footer
    {
        public Guid Id { get; set; }
        public Document DocumentThatBelongs { get; set; }
        public StyleClass StyleClass { get; set; }
        public Content Content { get; set; }

        public Footer()
        {
            DocumentThatBelongs = null;
            StyleClass = null;
            Content = null;
        }
    }
}
