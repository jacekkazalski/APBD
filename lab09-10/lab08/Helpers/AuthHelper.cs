using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lab08.Helpers
{
    public static class AuthHelper
    {
        public static JwtSecurityToken GenerateToken(/*Obiekt usera z bazy danych, */ string secret)
        {
            // claimsy wypelniamy operujac na obiekcie reprezentujacym
            // uzytkownika z bazy danych

            Claim[] userClaims =
            {
                //normalnie wartosci bierzemy z przekazanego obiektu user
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "Daniel"),
                new Claim(ClaimTypes.Surname,"Jabłoński"),
                new Claim(ClaimTypes.Email,"jd@pja.edu.pl"),
                new Claim(ClaimTypes.Role,"admin"),
                new Claim(ClaimTypes.Role,"lecturer")
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5002",
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );

            return token;
        }
    }
}
