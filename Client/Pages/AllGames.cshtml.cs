using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Client.Model;

namespace Client.Pages
{
    public class AllGamesModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]
        public List<GameDTO> Games { get; set; } = new List<GameDTO>();

        public AllGamesModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient("ApiGames");

            try
            {
                var response = await client.GetAsync("api/Games");
                if (response == null || !response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error de carrega de dades de la llista Films");
                }
                else
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Games = JsonSerializer.Deserialize<List<GameDTO>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    Games = Games.OrderByDescending(x => x.Numero_Votos).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            //Agafem el token
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token)) return RedirectToPage("/Login");

            //Obliguem al HttpClient a enviar el token en el Header:
            var client = _httpClientFactory.CreateClient("ApiGames");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"api/Games/{id}");

            //En aquest cas comprobem si la resposta es BadRequest
            if (response == null || !(response.StatusCode == HttpStatusCode.BadRequest))
            {
                _logger.LogError("No s'ha eliminat l'element. BadRequest response");
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostVoteAsync(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token)) return RedirectToPage("/Login");

            var client = _httpClientFactory.CreateClient("ApiGames");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"api/Games/vote/{id}", null);

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogError("Error en la votació. Codi: " + response?.StatusCode);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUnVoteAsync(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token)) return RedirectToPage("/Login");

            var client = _httpClientFactory.CreateClient("ApiGames");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"api/Games/unvote/{id}", null);

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogError("Error en la votació. Codi: " + response?.StatusCode);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdate(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token)) return RedirectToPage("/Login");
            var client = _httpClientFactory.CreateClient("ApiGames");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return RedirectToPage("/UpdateGame", new { id });
        }
    }
}