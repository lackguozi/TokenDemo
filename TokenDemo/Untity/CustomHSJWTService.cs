using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TokenDemo.Untity
{
    public class CustomHSJWTService : ICustomJWTService
    {
        private readonly JWTTokenOptions _JwtTokenOption;
        public CustomHSJWTService(IOptionsMonitor<JWTTokenOptions>jwtTokenOptions)
        {
            _JwtTokenOption = jwtTokenOptions.CurrentValue;
        }

        public string GetJWTToken(string name, string pwd)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "001"),
                new Claim(ClaimTypes.Name, name),
                new Claim("TestName", name),
                new Claim("JWT", "Fuck u "),
                new Claim("Love","c#"),
                new Claim("whhfhf", "wandale"),
                new Claim("Demo", "test"),

            };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtTokenOption.SecurityKey));
            SigningCredentials creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(issuer: _JwtTokenOption.Issuer,
                audience: _JwtTokenOption.Audience, claims: claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: creds);
            string rtoken = new JwtSecurityTokenHandler().WriteToken(token);
            return rtoken;
        }
    }
}
