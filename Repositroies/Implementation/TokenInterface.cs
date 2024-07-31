using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using myproject.Repositroies.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace myproject.Repositroies.Implimentation
{
    public class TokenInterface : ITokenInterface
    {
        private readonly IConfiguration configuration;

        public TokenInterface(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public  string createJwtToken(IdentityUser user, List<string> roles)
        {

            var claim = new List<Claim>
            { 
                new Claim(ClaimTypes.Email, user.Email),
            };

            claim.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role, role)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken
            (
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims:claim,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials

            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
