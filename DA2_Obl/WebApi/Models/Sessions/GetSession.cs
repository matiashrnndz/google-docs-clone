using Domain;
using System;

namespace WebApi.Models.Sessions
{
    public class GetSession : Model<Session, GetSession>
    {
        public Guid Token { get; set; }
        public User User { get; set; }

        public GetSession()
        {

        }

        public GetSession(Session session)
        {
            SetModel(session);
        }

        public override Session ToEntity() => new Session()
        {
            Token = this.Token,
            User = this.User
        };

        protected override GetSession SetModel(Session session)
        {
            Token = session.Token;
            User = session.User;

            return this;
        }
    }
}