using Domain;
using Exception;
using Repository;
using Service;
using System.Collections.Generic;

namespace ServiceImp
{
    public class UserManagementService : IUserManagementService
    {
        internal IUserRepository UserRepository { get; set; }

        public IEnumerable<User> GetAll()
        {
            return UserRepository.GetAll();
        }

        public User GetByEmail(string userEmail)
        {
            if (UserRepository.Exists(userEmail))
            {
                return UserRepository.GetByEmail(userEmail);
            }
            else
            {
                throw new MissingUserException("The user with that mail was not found.");
            }
        }

        public User Add(User user)
        {
            if (!UserRepository.Exists(user.Email))
            {
                UserRepository.Add(user);

                return user;
            }
            else
            {
                throw new ExistingUserException("The user already exists on the database.");
            }
        }

        public void Update(string userEmail, User user)
        {
            user.Email = userEmail;

            if (UserRepository.Exists(userEmail))
            {
                UserRepository.Update(user);
            }
            else
            {
                throw new MissingUserException("User is not in the database.");
            }
        }

        public void Delete(string userEmail)
        {
            if (UserRepository.Exists(userEmail))
            {
                UserRepository.Delete(userEmail);
            }
            else
            {
                throw new MissingUserException("The user with that mail was not found.");
            }
        }
    }
}