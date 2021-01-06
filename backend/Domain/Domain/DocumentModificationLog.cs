using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DocumentModificationLog
    {
        public Guid Id { get; set; }
        public Document Document { get; set; }
        public DateTime DateOfModification { get; set; }
    }
}
