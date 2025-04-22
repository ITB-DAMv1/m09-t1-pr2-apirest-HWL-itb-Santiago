using System.Net.Http.Headers;
using Client.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class CreateGameModel : PageModel
    {
        private readonly ILogger<CreateGameModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]
        public GameDTO Game { get; set; } = new GameDTO();
        public void OnGet()
        {
        }
        public CreateGameModel(IHttpClientFactory httpClientFactory, ILogger<CreateGameModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!Tools.TokenHelper.IsTokenSession(token) || Tools.TokenHelper.GetUserRole(token) != "Admin")
                return RedirectToPage("/Login");
            var client = _httpClientFactory.CreateClient("ApiGames");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.PostAsJsonAsync("/api/Games/insert", Game);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                _logger.LogError("Error al crear el joc");
                return Page();
            }
        }
    }
}
