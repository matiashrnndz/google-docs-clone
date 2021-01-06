using Domain;

namespace WebApi.Models.Users
{
    public class AddUser : Model<User, AddUser>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Administrator { get; set; }

        public AddUser()
        {

        }

        public AddUser(User user)
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

        protected override AddUser SetModel(User user)
        {
            Email = user.Email;
            UserName = user.UserName;
            Password = user.Password;
            Name = user.Name;
            LastName = user.LastName;
            Administrator = user.Administrator;

            return this;
        }
    }
}