using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IDocumentCreationByUserGraphService
    {
        IEnumerable<Tuple<string, int>> GetDocumentByUserCreationGraph(DateTime startingDate, DateTime latestDate);
    }
}
