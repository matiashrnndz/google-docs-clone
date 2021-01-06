using System;

namespace Domain
{
    public class LoggedEntry
    {
        public Guid Id { get; set; }
        public string TypeOfEntry { get; set; }
        public string LoggedUser { get; set; }
        public DateTime DateOfRegistration { get; set; }

        public override string ToString()
        {
            string ret;

            ret = "User " + this.LoggedUser + "has registered the following " + this.TypeOfEntry + " action. Date: " + DateOfRegistration.ToString(); 

            return ret;
        }
    }
}
