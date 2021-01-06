using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDocumentModificationLogRepository
    {
        IEnumerable<DocumentModificationLog> GetAllByDocument(Guid documentId);
        void Add(DocumentModificationLog modificationLog);
    }
}
