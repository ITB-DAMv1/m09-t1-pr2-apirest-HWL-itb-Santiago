using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Eliminar el token d'autenticaci� de la sessi�
            HttpContext.Session.Remove("AuthToken");
            // Redirigir a la p�gina d'inici de sessi� o a una altra p�gina
            return RedirectToPage("/Login");
        }
    }
}
