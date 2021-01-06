using Domain;
using Repository;
using Service;

namespace ServiceImp
{
    public class LogInService : ILogInService
    {
        internal IUserRepository UserRepository { get; set; }

        public bool ValidateLogIn(User user)
        {
            User fromDatabase = UserRepository.GetByEmail(user.Email);

            return fromDatabase != null && user.Password == fromDatabase.Password;
        }
    }
}
