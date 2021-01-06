using Domain;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace RepositorySQLServer
{
    public class UserRepositorySQLServer : IUserRepository
    {
        private static UserRepositorySQLServer instance;

        private UserRepositorySQLServer()
        {

        }

        public static UserRepositorySQLServer GetInstance()
        {
            if (instance == null)
            {
                instance = new UserRepositorySQLServer();
            }

            return instance;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users;

            using (DatabaseContext c = new DatabaseContext())
            {
                users = c.Users
                    .ToList();
            }

            return users.AsEnumerable();
        }

        public User GetByEmail(string email)
        {
            User toGet;

            using (DatabaseContext c = new DatabaseContext())
            {
                toGet = c.Users
                    .Where(u => u.Email == email)
                    .FirstOrDefault();
            }

            return toGet;
        }

        public bool Exists(string email)
        {
            bool exists;

            using (DatabaseContext c = new DatabaseContext())
            {
                exists = c.Users
                    .Any(u => u.Email == email);
            }

            return exists;
        }

        public void Update(User user)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                User toUpdate = c.Users
                     .Where(u => u.Email == user.Email)
                     .FirstOrDefault();

                toUpdate.UserName = user.UserName;
                toUpdate.Password = user.Password;
                toUpdate.Name = user.Name;
                toUpdate.LastName = user.LastName;

                c.SaveChanges();
            }
        }

        public void Add(User toAdd)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                c.Users.Add(toAdd);

                c.SaveChanges();
            }
        }

        public void Delete(string email)
        {
            using (DatabaseContext c = new DatabaseContext())
            {
                User toDelete = c.Users
                     .Where(u => u.Email == email)
                     .FirstOrDefault();

                c.Users.Remove(toDelete);

                c.SaveChanges();
            }
        }
    }
}
