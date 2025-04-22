using System.Text;
using System.Text.Json;
using Client.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class SigninAdminModel : PageModel
    {
        private readonly ILogger<SigninAdminModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public RegisterDTO Signin { get; set; } = new RegisterDTO();
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
        public SigninAdminModel(IHttpClientFactory httpClientFactory, ILogger<SigninAdminModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
            try
            {
                var client = _httpClientFactory.CreateClient("ApiGames");
                var response = await client.PostAsJsonAsync("api/Auth/admin/registre", Signin);
                if (response.IsSuccessStatusCode)
                {
                    SuccessMessage = "Usuari registrat correctament. Ara pots iniciar sessió.";
                    return RedirectToPage("/Index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    _logger.LogWarning("Email ja registrat");
                    ErrorMessage = "Email ja registrat. Prova amb un altre.";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    _logger.LogWarning("Error en el registre");
                    ErrorMessage = "Error en el registre. Torna-ho a intentar.";
                }
                else
                {
                    _logger.LogError("Error inesperat durant el registre");
                    ErrorMessage = "Error inesperat. Torna-ho a intentar.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durant el registre");
                ErrorMessage = "Error inesperat. Torna-ho a intentar.";
            }
            return Page();
        }
        public void OnGet()
        {
        }
    }
}
