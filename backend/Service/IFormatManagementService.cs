using Domain;
using System.Collections.Generic;

namespace Service
{
    public interface IFormatManagementService
    {
        IEnumerable<Format> GetAll();
        Format GetByName(string formatName);
        Format Add(Format format);
        void Delete(string formatName);
    }
}
