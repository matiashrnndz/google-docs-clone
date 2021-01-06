using Domain;

namespace Service
{
    public interface ILogInService
    {
        bool ValidateLogIn(User user);
    }
}
