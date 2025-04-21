using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client.Model;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Client.Pages
{
    public class GameDetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger _logger;
        public GameDetailsModel(IHttpClientFactory httpClient, ILogger<GameDetailsModel> logging)
        {
            _httpClient = httpClient;
            _logger = logging;
        }
        [BindProperty]
        public GameDTO Game { get; set; } = new GameDTO();

        public string? ErrorMessage { get; set; }
        public async Task OnGet(int id)
        {
            try
            {
                var client = _httpClient.CreateClient("ApiGames");
                var response = await client.GetAsync($"api/Games/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Game = JsonSerializer.Deserialize<GameDTO>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    ErrorMessage = "Error al carregar les dades del joc.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durant la càrrega de detalls del joc");
                ErrorMessage = "Error inesperat. Torna-ho a intentar.";
            }
        }

        public async Task<IActionResult> OnPostVoteAsync(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token)) return RedirectToPage("/Login");

            var client = _httpClient.CreateClient("ApiGames");
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

            var client = _httpClient.CreateClient("ApiGames");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"api/Games/unvote/{id}", null);

            if (response == null || !response.IsSuccessStatusCode)
            {
                _logger.LogError("Error en la votació. Codi: " + response?.StatusCode);
            }

            return RedirectToPage();
        }
    }
}
