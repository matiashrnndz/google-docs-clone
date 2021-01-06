using Domain;
using System.Collections.Generic;

namespace Service
{
    public interface IStyleClassManagementService
    {
        IEnumerable<StyleClass> GetAll();
        StyleClass GetByName(string styleClassName);
        bool Exists(string styleClassName);
        StyleClass Add(StyleClass styleClass);
        void Delete(string styleClassName);
        void Update(string styleClass_name, StyleClass styleClass);
    }
}
