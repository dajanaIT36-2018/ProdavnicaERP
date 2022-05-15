using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProdavnicaERP.Data;
using ProdavnicaERP.Entities;
using ProdavnicaERP.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProdavnicaERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private IKorisnikRepository korisnikRepository;
        private ITipKorisnikaRepository tipKorisnikaRepository;

        public LoginController(IConfiguration config, IKorisnikRepository korisnikRepository, ITipKorisnikaRepository tipKorisnikaRepository)
        {
            _config = config;
            this.korisnikRepository = korisnikRepository;
            this.tipKorisnikaRepository = tipKorisnikaRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }
        private string Generate(Korisnik user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            if (user.TipKorisnika.Tip == "admin")
            {
                user.Rola = "Admin";

            }
            else 
            {
                user.Rola = "Kupac";
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ImeKorisnik),
                new Claim(ClaimTypes.Email, user.EMailKorisnika),
                new Claim(ClaimTypes.Role, user.Rola)
               
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Korisnik Authenticate(UserLogin userLogin)
        {
            var currentUser = korisnikRepository.GetKorisnikByUserNameAndPassword(userLogin.KorisnickoIme, userLogin.Lozinka);
            currentUser.TipKorisnika= tipKorisnikaRepository.GetTipKorisnikaById(currentUser.TipKorisnikaId);



            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
