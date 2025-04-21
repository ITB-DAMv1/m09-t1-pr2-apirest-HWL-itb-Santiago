using System;
using System.IO;
using System.Security.Claims;
using Server.Data;
using Server.DTO;
using Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GamesController> _logger;

        public GamesController(ApplicationDbContext context, ILogger<GamesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Consulta todos los juegos en la base de datos
        /// </summary>
        /// returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            var games = await _context.Games
                .Select(g => new GameDTO
                {
                    Id = g.Id,
                    Titulo = g.Titulo,
                    Descripcion = g.Descripcion,
                    Desarrollador = g.Desarrollador,
                    Imagen = g.Imagen,
                    Numero_Votos = g.Numero_Votos,
                    Fecha_Lanzamiento = g.Fecha_Lanzamiento
                })
                .ToListAsync();

            return Ok(games);
        }

        // GET: api/Games/id
        /// <summary>
        /// Consulta juego por id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return NotFound();
            var gameDTO = new GameDTO
            {
                Id = game.Id,
                Titulo = game.Titulo,
                Descripcion = game.Descripcion,
                Desarrollador = game.Desarrollador,
                Imagen = game.Imagen,
                Numero_Votos = game.Numero_Votos,
                Fecha_Lanzamiento = game.Fecha_Lanzamiento
            };
            return Ok(gameDTO);
        }

        // GET: api/Games/search?titulo
        /// <summary>
        /// Consulta juego por titulo   
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGameByTitle(string titulo)
        {
            if (string.IsNullOrEmpty(titulo))
                return BadRequest("Title cannot be null or empty");
            if (titulo.Length < 3)
                return BadRequest("Title must be at least 3 characters long");
            if (titulo.Length > 50)
                return BadRequest("Title must be at most 50 characters long");

            var gameByTitle = await _context.Games.Where(x => x.Titulo.Contains(titulo)).Select(g => new GameDTO
            {
                Titulo = g.Titulo,
                Descripcion = g.Descripcion,
                Desarrollador = g.Desarrollador,
                Imagen = g.Imagen,
                Numero_Votos = g.Numero_Votos,
                Fecha_Lanzamiento = g.Fecha_Lanzamiento
            }).
                OrderBy(x => x.Titulo)
                .Skip(0)
                .Take(10)
                .AsNoTracking()
                .ToListAsync();
            if (gameByTitle == null || gameByTitle.Count == 0)
                return NotFound("No games found with the given title");
            if (gameByTitle == null)
                return NotFound();
            return Ok(gameByTitle);
        }

        // POST: api/Games
        /// <summary>
        /// Crea un nuevo juego - solo admins
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("insert")]
        public async Task<ActionResult<Game>> CreateGame([FromBody] GameDTO game)
        {
            if (game == null)
                return BadRequest("Game cannot be null");
            var gameDTO = new Game
            {
                Titulo = game.Titulo,
                Descripcion = game.Descripcion,
                Desarrollador = game.Desarrollador,
                Imagen = game.Imagen,
                Numero_Votos = game.Numero_Votos,
                Fecha_Lanzamiento = game.Fecha_Lanzamiento
            };
            try
            {
                await _context.Games.AddAsync(gameDTO);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un Juego");
                return BadRequest("Error de insercion");
            }
            return CreatedAtAction(nameof(GetGameById), new { id = gameDTO.Id }, gameDTO);
        }

        // PUT: api/Games/id
        /// <summary>
        /// Actualiza un juego - solo admins
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame([FromRoute] int id, [FromBody] GameDTO gameDto)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return NotFound();

            // Mapear campos del DTO al modelo
            game.Titulo = gameDto.Titulo;
            game.Descripcion = gameDto.Descripcion;
            game.Desarrollador = gameDto.Desarrollador;
            game.Imagen = gameDto.Imagen;
            game.Numero_Votos = gameDto.Numero_Votos;
            game.Fecha_Lanzamiento = gameDto.Fecha_Lanzamiento;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Games/id
        /// <summary>
        /// Elimina un juego - solo admins
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Game>> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                return NotFound();
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Permite votar un juego - solo usuarios autenticados
        /// </summary>
        [Authorize(Roles = "User, Admin")]
        [HttpPost("vote/{id}")]
        public async Task<ActionResult> VoteGame(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("Token no contiene ID d'usuari");

            var user = await _context.ApplicationUsers.FindAsync(userIdClaim);
            if (user == null)
                return NotFound("Usuari no trobat");

            var game = await _context.Games
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
                return NotFound("Joc no trobat");

            if (game.Users.Any(u => u.Id == user.Id))
                return BadRequest("Ja has votat aquest joc");

            game.Numero_Votos += 1;
            user.Games.Add(game);
            _logger.LogInformation($"Usuari amb id {user.Id} ha votat pel joc {game.Id}. Vots actuals: {game.Numero_Votos}");

            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Permite desvotar un juego - solo usuarios autenticados
        /// </summary>
        [Authorize(Roles = "User, Admin")]
        [HttpPost("unvote/{id}")]
        public async Task<ActionResult> UnvoteGame(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("Token no conté ID d'usuari");
            var user = await _context.ApplicationUsers.FindAsync(userIdClaim);
            if (user == null)
                return NotFound("Usuari no trobat");
            var game = await _context.Games
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (game == null)
                return NotFound("Joc no trobat");
            if (!game.Users.Any(u => u.Id == user.Id))
                return BadRequest("No has votat aquest joc");
            game.Numero_Votos -= 1;
            user.Games.Remove(game);
            _logger.LogInformation($"Usuari amb id {user.Id} ha desvotat el joc {game.Id}. Vots actuals: {game.Numero_Votos}");
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
