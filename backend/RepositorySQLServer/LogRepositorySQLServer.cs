using Domain;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace RepositorySQLServer
{
    public class LogRepositorySQLServer : ILogRepository
    {
        private static LogRepositorySQLServer instance;

        private LogRepositorySQLServer()
        {

        }

        public static LogRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new LogRepositorySQLServer();
            }

            return instance;
        }

        public IEnumerable<LoggedEntry> getAllLogs()
        {
            List<LoggedEntry> loggedEntries;

            using (DatabaseContext c = new DatabaseContext())
            {
                loggedEntries = c.LoggedEntries
                    .ToList();
            }

            return loggedEntries.AsEnumerable();
        }

        public void Add(LoggedEntry loggedEntry)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                c.LoggedEntries.Add(loggedEntry);

                c.SaveChanges();
            }
        }
    }
}
