using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client.Model;

namespace Client.Pages
{
    public class SigninModel : PageModel
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger _logger;

        [BindProperty]
        public RegisterDTO Signin { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public SigninModel(IHttpClientFactory httpClient, ILogger<LoginModel> logging)
        {
            _httpClient = httpClient;
            _logger = logging;
        }


        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var client = _httpClient.CreateClient("ApiGames");
                var response = await client.PostAsJsonAsync("api/Auth/registre", Signin);

                if (response.IsSuccessStatusCode)
                {
                    SuccessMessage = "Usuari registrat correctament. Ara pots iniciar sessió.";
                    return RedirectToPage("/Login");
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

    }

}