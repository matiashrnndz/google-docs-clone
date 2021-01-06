using Domain;
using System.Collections.Generic;

namespace Service
{
    public interface IUserManagementService
    {
        IEnumerable<User> GetAll();
        User GetByEmail(string userEmail);
        void Update(string userEmail, User user);
        User Add(User user);
        void Delete(string userEmail);
    }
}
