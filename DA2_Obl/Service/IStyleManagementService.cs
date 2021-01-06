using Domain;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IStyleManagementService
    {
        IEnumerable<Style> GetAll(string formatName, string styleClassName);
        Style Add(string formatName, string styleClassName, Style style);
        void Delete(Guid styleId);
    }
}