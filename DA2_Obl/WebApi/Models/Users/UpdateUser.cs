using Domain;

namespace WebApi.Models.Users
{
    public class UpdateUser : Model<User, UpdateUser>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Administrator { get; set; }

        public UpdateUser()
        {

        }

        public UpdateUser(User user)
        {
            SetModel(user);
        }

        public override User ToEntity() => new User()
        {
            UserName = this.UserName,
            Password = this.Password,
            Name = this.Name,
            LastName = this.LastName,
            Administrator = this.Administrator
        };

        protected override UpdateUser SetModel(User user)
        {
            UserName = user.UserName;
            Password = user.Password;
            Name = user.Name;
            LastName = user.LastName;
            Administrator = user.Administrator;

            return this;
        }

        public override string ToString()
        {
            return "Username : " + UserName + " " +
                "Name : " + Name + " " +
                "Last name : " + LastName;
        }
    }
}