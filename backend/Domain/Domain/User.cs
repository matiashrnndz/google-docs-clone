using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Administrator { get; set; }

        public User()
        {
            Email = "";
            Name = "";
            LastName = "";
            UserName = "";
            Password = "";
            Administrator = false;
        }
    }
}