using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IDocumentModificationLogService
    {
        void LogModificationToDocument(Guid documentId);
        void LogModificationToHeader(Guid headerId);
        void LogModificationToFooter(Guid footerId);
        void LogModificationToParagraph(Guid paragraphId);
        void LogModificationToText(Guid textId);
    }
}
