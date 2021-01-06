using Domain;
using System.Collections.Generic;

namespace Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        void Add(User user);
        bool Exists(string email);
        void Update(User user);
        User GetByEmail(string userEmail);
        void Delete(string userEmail);
    }
}
