using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DocumentFilterAndOrder
    {
        public Document DocumentFilteredData { get; set; }
        public string OrderBy { get; set; }
        public bool IsDesc { get; set; }
    }
}
