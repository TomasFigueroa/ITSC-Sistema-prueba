using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Login")]
    public class ControllersUsuario : ControllerBase
    {
        private readonly IConfiguration _config;

        public ControllersUsuario(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {

            if (login.Correo == "admin@gmail.com" && login.Clave == "admin")
            {
                //Generar token JWT
                login.Token = GenerarTokenJWT(login.Correo);

                return Ok(login);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Usuario o contraseña incorrectos");
            }
        }

        private string GenerarTokenJWT(string correo)
        {
            //definicion de los claims del token
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, correo),
            };

            //clave secreta del token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //config del token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet]
        [Route("GenerateToken")]
        public IActionResult GenerateToken()
        {
            var claims = new[]
            {
              new Claim(ClaimTypes.Email, "test@gmail.com"),
              new Claim(ClaimTypes.Role, "TestUser")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EstaEsUnaClaveSecretaDe32Caracteres1234"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "JwtAPI",
                audience: "TuAplicacion",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}

