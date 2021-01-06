using System;

namespace Domain
{
    public class StyleClass
    {
        public string Name { get; set; }
        public StyleClass BasedOn { get; set; }

        public StyleClass()
        {
            Name = "";
            BasedOn = null;
        }
        
        public override bool Equals(object obj)
        {
            if (obj != null && GetType() == obj.GetType())
            {
                return this.Name == ((StyleClass)obj).Name;
            }
            else
                return false;
        }

        public override string ToString()
        {
            string ret;
            ret = "Class: " + this.Name;
            if(this.BasedOn != null)
            {
                ret += "| { Based On: " + this.BasedOn.ToString() + " }";
            }

            return ret;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}