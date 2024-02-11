using Domain.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Auth
{
    public class JwtTokenService : ITokenService
    {
        readonly Dictionary<string, string> _users = new Dictionary<string, string>()
        {
            {"student1",Roles.Student },
            {"student2",Roles.Student },
            {"student3",Roles.Student },
            {"coursemanager1",Roles.CourseManager },
            {"coursemanager2",Roles.CourseManager },
            {"admin",Roles.Admin },

        };
        private readonly string secretKey;

        public JwtTokenService(string secretKey)
        {
            this.secretKey = secretKey;
        }
        public TokenModel GetToken(UserModel user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            if (!_users.Any(u => u.Key == user.Name))
            {
                throw new UnauthorizedAccessException("invalid user name");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenKey = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Name) ,
                    new Claim(ClaimTypes.Role, _users[user.Name])
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenModel { Token = tokenHandler.WriteToken(token) };
        }
    }
}
