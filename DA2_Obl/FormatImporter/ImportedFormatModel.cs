using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace FormatImporter
{
    public class ImportedFormatModel
    {
        public Format ImportedFormat { get; set; }
        public IEnumerable<StyleClass> ImportedStyleClasses { get; set; }
        public IEnumerable<Style> ImportedStyles { get; set; }
    }
}
