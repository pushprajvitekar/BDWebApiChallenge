using Domain.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Auth.JwtService
{
    public class JwtTokenService : ITokenService
    {
        internal struct UserInfo
        {
            internal string Role;
            internal int Id;
        }
        readonly Dictionary<string, UserInfo> _users = new Dictionary<string, UserInfo>()
        {
            {"student1",new UserInfo{Id=1, Role=Roles.Student  } },
            {"student2",new UserInfo {Id=2, Role = Roles.Student } },
            {"student3",new UserInfo {Id=3, Role = Roles.Student } },
            {"coursemanager1",new UserInfo {Id=101, Role = Roles.CourseManager } },
            {"coursemanager2",new UserInfo {Id=102, Role = Roles.CourseManager } },
            {"admin",new UserInfo {Id=501, Role = Roles.Admin } },

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
                    new Claim(ClaimTypes.Role, _users[user.Name].Role),
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString( _users[user.Name].Id))
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenModel { Token = tokenHandler.WriteToken(token) };
        }
    }
}
