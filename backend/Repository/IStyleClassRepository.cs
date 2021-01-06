using Domain;
using System.Collections.Generic;

namespace Repository
{
    public interface IStyleClassRepository
    {
        IEnumerable<StyleClass> GetAll();
        StyleClass GetByName(string styleClassName);
        bool Exists(string styleClassName);
        void Update(StyleClass styleClass);
        void Add(StyleClass styleClass);
        void Delete(string styleClassName);
    }
}
