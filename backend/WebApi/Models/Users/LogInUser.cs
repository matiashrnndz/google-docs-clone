using Domain;

namespace WebApi.Models.Users
{
    public class LogInUser : Model<User, LogInUser>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LogInUser()
        {

        }

        public LogInUser(User user)
        {
            SetModel(user);
        }

        public override User ToEntity() => new User()
        {
            Email = this.Email,
            Password = this.Password,
        };

        protected override LogInUser SetModel(User user)
        {
            Email = user.Email;
            Password = user.Password;

            return this;
        }
    }
}