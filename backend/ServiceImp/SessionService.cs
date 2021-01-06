using Domain;
using Repository;
using Service;
using System;
using Exceptions = System.Exception;

namespace ServiceImp
{
    public class SessionService : ISessionService
    {
        internal ISessionRepository SessionRepository { get; set; }

        public bool VerifyToken(string token)
        {
            Guid guid;

            try
            {
                guid = Guid.Parse(token);
            }
            catch (Exceptions e)
            {
                throw new InvalidOperationException(e.Message);
            }

            return SessionRepository.ExistsByToken(guid);
        }

        //Pre: VerifyToken(token) returns true
        public string GetLoggedUsersMail(string token)
        {
            Guid guid;

            try
            {
                guid = Guid.Parse(token);
            }
            catch (Exceptions e)
            {
                throw new InvalidOperationException(e.Message);
            }

            return SessionRepository.GetEmailByToken(guid);
        }

        public Guid GetToken(User user)
        {
            Session session;

            if (!SessionRepository.ExistsByUser(user.Email))
            {
                session = new Session
                {
                    Token = Guid.NewGuid(),
                    User = user
                };

                SessionRepository.Add(session);
            }
            else
            {
                session = SessionRepository.GetByUser(user.Email);
            }

            return session.Token;
        }

        //Pre: VerifyToken(token) returns true
        public Session GetSessionByUser(User user)
        {
            Session session = SessionRepository.GetByUser(user.Email);

            return session;
        }
    }
}