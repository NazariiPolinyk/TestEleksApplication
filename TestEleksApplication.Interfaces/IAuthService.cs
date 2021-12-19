using System.Threading.Tasks;

namespace TestEleksApplication.Interfaces
{
    public interface IAuthService
    {
        public Task<IUser> Authenticate(string login, string password);
    }
}
