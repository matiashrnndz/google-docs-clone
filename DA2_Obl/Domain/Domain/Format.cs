namespace Domain
{
    public class Format
    {
        public string Name { get; set; }

        public Format()
        {
            Name = "";
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}