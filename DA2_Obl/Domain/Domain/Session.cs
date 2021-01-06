using System;

namespace Domain
{
    public class Session
    {
        public Guid Token { get; set; }
        public User User { get; set; }

        public Session()
        {
            User = null;
            Token = Guid.Empty;
        }
    }
}
