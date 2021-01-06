using Domain;
using System.Collections.Generic;
using System;

namespace Repository
{
    public interface IStyleRepository
    {
        IEnumerable<Style> GetStyles(string styleClassName, string formatName);
        bool Exists(StyleClass styleClass, Format format);
        void Add(Style style);
        void Delete(Style style);
        Style GetById(Guid styleId);
        bool ExistsById(Guid styleId);
    }
}
