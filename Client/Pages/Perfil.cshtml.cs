using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Client.Model;

public class PerfilModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PerfilModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public UserDTO? Usuario { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var token = HttpContext.Session.GetString("AuthToken");
        if (string.IsNullOrEmpty(token)) return RedirectToPage("/Login");

        var client = _httpClientFactory.CreateClient("ApiGames");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.GetAsync("api/Users/perfil");

        if (!response.IsSuccessStatusCode)
        {
            // Manejo de error
            return RedirectToPage("/Error");
        }

        Usuario = await response.Content.ReadFromJsonAsync<UserDTO>();

        return Page();
    }
}

