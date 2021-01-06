using Domain;

namespace WebApi.Models.Users
{
    public class GetUser : Model<User, GetUser>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Administrator { get; set; }

        public GetUser()
        {
            
        }

        public GetUser(User user)
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

        protected override GetUser SetModel(User user)
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