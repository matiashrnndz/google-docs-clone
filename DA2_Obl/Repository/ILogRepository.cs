using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public interface ILogRepository
    {
        void Add(LoggedEntry loggedEntry);
        IEnumerable<LoggedEntry> getAllLogs();
    }
}
