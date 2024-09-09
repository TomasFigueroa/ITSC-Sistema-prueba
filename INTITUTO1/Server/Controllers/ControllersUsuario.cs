using INTITUTO1.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace INTITUTO1.Server.Controllers
{
    [ApiController]
    [Route("api/Login")]
    public class ControllersUsuario: ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            SesionDTO sesionDTO = new SesionDTO();

            if (login.Correo == "admin@gmail.com" && login.Clave == "admin")
            {
                sesionDTO.Nombre = "admin";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Administrador";
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return StatusCode(StatusCodes.Status200OK, sesionDTO);
        }
    }
}
