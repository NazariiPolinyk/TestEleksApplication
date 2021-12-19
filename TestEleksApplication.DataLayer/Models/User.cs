using TestEleksApplication.Interfaces;

namespace TestEleksApplication.DataLayer.Models
{
    public class User : IUser
    {
        public User () { }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
