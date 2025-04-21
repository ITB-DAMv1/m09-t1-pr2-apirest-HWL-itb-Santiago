using Server.Controllers;
using Server.Data;
using Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Server.DTO;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsersController> _logger;
        public UsersController(ApplicationDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Consulta el perfil del usuario con sus atributos
        /// </summary>
        [Authorize]
        [HttpGet("perfil")]
        public async Task<ActionResult<UserDTO>> GetPerfil()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("No es pot obtenir l'identificador de l'usuari");

            var user = await _context.Users
                .Include(u => u.Games)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound("Usuari no trobat");

            var userDto = new UserDTO
            {
                Nombre = user.UserName,
                Email = user.Email,
                Games = user.Games.Select(g => new GameDTO
                {
                    Id = g.Id,
                    Titulo = g.Titulo,
                    Descripcion = g.Descripcion,
                    Numero_Votos = g.Numero_Votos
                }).ToList()
            };

            return Ok(userDto);
        }

    }
}
