using Domain;
using System.Collections.Generic;

namespace Repository
{
    public interface IFormatRepository
    {
        IEnumerable<Format> GetAll();
        Format GetByName(string formatName);
        bool Exists(string formatName);
        void Add(Format format);
        void Delete(string formatName);
    }
}
