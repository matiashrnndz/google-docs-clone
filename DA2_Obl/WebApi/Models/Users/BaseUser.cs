using Domain;

namespace WebApi.Models.Users
{
    public class BaseUser : Model<User, BaseUser>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Administrator { get; set; }

        public BaseUser()
        {

        }

        public BaseUser(User user)
        {
            SetModel(user);
        }

        public override User ToEntity() => new User()
        {
            Email = this.Email,
            UserName = this.UserName,
            Password = this.Password,
            Name = this.Name,
            LastName = this.LastName,
            Administrator = this.Administrator
        };

        protected override BaseUser SetModel(User user)
        {
            Email = user.Email;
            UserName = user.UserName;
            Password = "****";
            Name = user.Name;
            LastName = user.LastName;
            Administrator = user.Administrator;

            return this;
        }
    }
}