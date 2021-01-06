using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface ITextRepository
    {
        Text GetById(Guid textId);
        IEnumerable<Text> GetByContent(Content content);
        void Add(Text text);
        void Update(Text text);
        void Delete(Guid textId);
        bool Exists(Guid textId);
    }
}
