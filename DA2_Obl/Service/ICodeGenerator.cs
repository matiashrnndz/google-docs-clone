using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Service
{
    public interface ICodeGenerator
    {
        string GenerateHTML(Document document, Format format);
    }
}
