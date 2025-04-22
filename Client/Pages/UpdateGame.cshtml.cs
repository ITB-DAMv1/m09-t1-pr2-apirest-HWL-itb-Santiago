using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Client.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class UpdateGameModel : PageModel
    {
        private readonly ILogger<UpdateGameModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]
        public GameDTO Game { get; set; } = new GameDTO();
        public UpdateGameModel(IHttpClientFactory httpClientFactory, ILogger<UpdateGameModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token) || Tools.TokenHelper.GetUserRole(token) != "Admin")
                return RedirectToPage("/Login");

            var client = _httpClientFactory.CreateClient("ApiGames");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"/api/Games/{id}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Error carregant el joc amb id " + id);
                return RedirectToPage("/Index");
            }

            var content = await response.Content.ReadAsStringAsync();
            Game = JsonSerializer.Deserialize<GameDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token) || Tools.TokenHelper.GetUserRole(token) != "Admin")
                return RedirectToPage("/Login");

            var client = _httpClientFactory.CreateClient("ApiGames");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var gameDto = new GameDTO
            {
                Id = Game.Id,
                Titulo = Game.Titulo,
                Descripcion = Game.Descripcion,
                Desarrollador = Game.Desarrollador,
                Imagen = Game.Imagen,
                Numero_Votos = Game.Numero_Votos,
                Fecha_Lanzamiento = Game.Fecha_Lanzamiento
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(gameDto), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"api/Games/{Game.Id}", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(gameDto.Id.ToString());
                Console.WriteLine("Error actualitzant el joc");
                _logger.LogError("Error actualitzant el joc.");
                return Page();
            }

            return RedirectToPage("/Index");
        }

    }
}
