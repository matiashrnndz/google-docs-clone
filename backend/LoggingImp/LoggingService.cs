using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Service;
using Repository;

namespace LoggingImp
{
    public class LoggingService : ILoggingService
    {
        public ILogRepository LogRepository { get; set; }

        public void AddLogForFormatImport(string username)
        {
            LoggedEntry newLog = new LoggedEntry();
            newLog.TypeOfEntry = "Import";
            newLog.Id = new Guid();
            newLog.loggedUser = username;
            newLog.dateOfRegistration = DateTime.Now;
            LogRepository.Add(newLog);
        }

        public void AddLogForLogin(string username)
        {
            LoggedEntry newLog = new LoggedEntry();
            newLog.TypeOfEntry = "Login";
            newLog.Id = new Guid();
            newLog.loggedUser = username;
            newLog.dateOfRegistration = DateTime.Now;
            LogRepository.Add(newLog);
        }

        public IEnumerable<LoggedEntry> getLogs(DateTime startingDate, DateTime endingDate)
        {
            IEnumerable<LoggedEntry> allLogs = LogRepository.getAllLogs();
            List<LoggedEntry> theRightLogs = new List<LoggedEntry>();
            foreach (LoggedEntry selectedLog in allLogs)
            {
                if(startingDate <= selectedLog.dateOfRegistration && selectedLog.dateOfRegistration <= endingDate)
                {
                    theRightLogs.Add(selectedLog);
                }
            }
            return theRightLogs;
        }
    }
}
