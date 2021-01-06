using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatImporter
{
    public interface IFormatImporterLogic
    {
        bool isCompatibleFile(string filepath);
        ImportedFormatModel getFormat(string filepath);
    }
}
