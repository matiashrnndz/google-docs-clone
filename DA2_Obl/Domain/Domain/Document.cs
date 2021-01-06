using System;

namespace Domain
{
    public class Document
    {
        public Guid Id { get; set; }
        public User Creator { get; set; }
        public string Title { get; set; }
        public StyleClass StyleClass { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModification { get; set; }

        public Document()
        {
            Creator = null;
            StyleClass = null;
            CreationDate = new DateTime();
            LastModification = CreationDate;
        }
    }
}
