using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLocalMemory
{
    public class SessionRepositoryLocalMemory : ISessionRepository
    {
        private static SessionRepositoryLocalMemory instance;

        private List<Session> activeSessions;

        private SessionRepositoryLocalMemory()
        {
            activeSessions = new List<Session>();

            Session debugSession = new Session
            {
                Email = "admin@admin.com",
                Token = Guid.Empty
            };

            activeSessions.Add(debugSession);
        }

        public static SessionRepositoryLocalMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new SessionRepositoryLocalMemory();
            }

            return instance;
        }

        public void Add(Session session)
        {
            activeSessions.Add(session);
        }

        public bool ExistsByUser(string email)
        {
            return activeSessions.Any(s => s.Email == email);
        }

        public IEnumerable<Session> GetActiveSessions()
        {
            return activeSessions.AsEnumerable();
        }

        public Session GetByUser(string email)
        {
            return activeSessions.Where(s => s.Email == email).FirstOrDefault();
        }
    }
}
