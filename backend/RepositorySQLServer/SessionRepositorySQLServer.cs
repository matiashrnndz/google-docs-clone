using Domain;
using Repository;
using System;
using System.Data.Entity;
using System.Linq;

namespace RepositorySQLServer
{
    public class SessionRepositorySQLServer : ISessionRepository
    {
        private static SessionRepositorySQLServer instance;

        private SessionRepositorySQLServer()
        {

        }

        public static SessionRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new SessionRepositorySQLServer();
            }

            return instance;
        }

        public void Add(Session session)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                if (c.Users.Any(u => u.Email == session.User.Email))
                {
                    c.Entry(session.User)
                        .State = EntityState.Unchanged;
                }

                c.Sessions.Add(session);

                c.SaveChanges();
            }
        }

        public bool ExistsByUser(string email)
        {
            bool exists = false;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Sessions.Any(u => u.User.Email == email);
            }

            return exists;
        }

        public Session GetByUser(string email)
        {
            Session session = null;

            using (DatabaseContext c = new DatabaseContext())
            {
                session = c.Sessions.Where(s => s.User.Email == email)
                    .Include(u => u.User)
                    .FirstOrDefault();
            }

            return session;
        }

        public bool ExistsByToken(Guid token)
        {
            bool exists = false;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Sessions.Any(s => s.Token == token);
            }

            return exists;
        }

        public string GetEmailByToken(Guid token)
        {
            string email = "";

            using (DatabaseContext c = new DatabaseContext())
            {
                Session session = c.Sessions.Where(s => s.Token == token)
                    .Include(u => u.User)
                    .FirstOrDefault();

                email = session.User.Email;
            }

            return email;
        }
    }
}
