using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Service
{
    public interface ILoggingService
    {
        void AddLogForLogin(string username);
        void AddLogForFormatImport(string username);
        IEnumerable<LoggedEntry> getLogs(DateTime startingDate, DateTime endingDate);
    }
}
