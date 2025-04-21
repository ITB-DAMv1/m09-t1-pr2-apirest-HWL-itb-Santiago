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
            // Eliminar el token d'autenticació de la sessió
            HttpContext.Session.Remove("AuthToken");
            // Redirigir a la pàgina d'inici de sessió o a una altra pàgina
            return RedirectToPage("/Login");
        }
    }
}
