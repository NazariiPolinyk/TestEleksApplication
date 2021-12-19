using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System;
using TestEleksApplication.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TestEleksApplication.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace TestEleksApplication.Services.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private readonly TestEleksDbContext _context;
        private readonly TokenOptions _tokenOptions;

        public AuthService(TestEleksDbContext context, IOptions<TokenOptions> tokenOptions)
        {
            _context = context;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<IUser> Authenticate(string login, string password)
        {
            var account = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (account == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenOptions.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.Login),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            account.Token = tokenHandler.WriteToken(token);

            return account;
        }
    }
}
