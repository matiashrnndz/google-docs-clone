using Domain;
using System;

namespace Service
{
    public interface ISessionService
    {
        bool VerifyToken(string token);
        string GetLoggedUsersMail(string token);
        Guid GetToken(User user);
        Session GetSessionByUser(User user);
    }
}