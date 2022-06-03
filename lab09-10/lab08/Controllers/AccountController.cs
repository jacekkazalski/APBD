using lab08.DTO;
using lab08.Entities;
using lab08.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace lab08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            /*z body przyjmujemy email i haslo
            * w nazie sprawdzamy czy istnieje uzytkownik
            * potem sprawdzamy czy haslo sie zgadza
            * 
            * jezeli sie nie zgadza to 400 badrequest
            * jezeli sie zgadza to generujemy acces token + refresh token
            */


            //weryfikacja hasla - zalozmy ze jestesmy w serwisie
            User user = new User();
            var verifyPassword = new PasswordHasher<User>().VerifyHashedPassword(
                user,
                "hash",
                "haslo");

            if(verifyPassword == PasswordVerificationResult.Failed)
            {
                //zwracamy 400 badrequest
            }

            string token = new JwtSecurityTokenHandler().WriteToken(AuthHelper.GenerateToken(/*user obiekt,*/_configuration["Secret"]));
            Guid refreshToken = Guid.NewGuid(); // <- powinien zostac dodany do danego usera (do kolumny) wraz z data wygasniecia

            return Ok(new
            {
                token,
                refreshToken
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            /*przyjmujemy z body zadania refresh token
             * sprawdzamy w bd czy mamy zapisany taki token przy ktoryms uzytkowniku
             * oraz czy nie wygasl
             * 
             * jezeli token wygasl zwracamy 400 badrequest
             * jezeeli nie wygasl generujemy ponownie pare access token + refresh token
             * nowym refresh tokenem nadpisujemy stara wartosc i ustawiamy na nowo czas
             */
            return Ok();
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            //zalozmy ze jestesmy w service
            User user = new User();

            string hashedPassword = new PasswordHasher<User>().HashPassword(user, registerDTO.Password);
            return Ok(hashedPassword);
        }
    }
}
