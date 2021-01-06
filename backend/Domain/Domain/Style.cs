using System;

namespace Domain
{
    public class Style
    {
        public Guid Id { get; set; }
        public Format Format { get; set; }
        public StyleClass StyleClass { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public Style()
        {
            Key = "";
            Value = "";
            Format = null;
            StyleClass = null;
        }

        public override string ToString()
        {
            string ret;

            ret = "Key: " + this.Key;

            if(this.Value != "")
            {
                ret += " Value: " + this.Value;
            }

            return ret;
        }
    }
}