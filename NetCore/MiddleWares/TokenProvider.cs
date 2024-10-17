using NetCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
//using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;

namespace NetCore.MiddleWares
{
    public class TokenProvider()
    {
        public static string Create(UserInfo userInfo)
        {
            string secretKey = "Test-Secret-Key-To-Test-Web-Api-Testing-Test";// Configuration.GetSection("Jwt").GetValue<string>("SecretKey")!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email)
                    ]),
                Expires = DateTime.Now.AddMinutes(30),// DateTime.Now.AddMinutes(configuration.GetSection("Jwt").GetValue<int>("ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = "NetCore", // configuration.GetSection("Jwt").GetValue<string>("Issuer")!,
                Audience = "Login" //configuration.GetSection("Jwt").GetValue<string>("Audience")!
            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
 