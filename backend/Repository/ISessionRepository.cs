using Domain;
using System;

namespace Repository
{
    public interface ISessionRepository
    {
        void Add(Session session);
        bool ExistsByUser(string email);
        Session GetByUser(string email);
        bool ExistsByToken(Guid token);
        string GetEmailByToken(Guid token);
    }
}
