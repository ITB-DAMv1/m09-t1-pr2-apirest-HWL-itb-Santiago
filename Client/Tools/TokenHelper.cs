using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.Tools
{
    public static class TokenHelper
    {
        public static bool IsTokenSession(string token)
        {
            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }
        /// <summary>
        /// Validar si el token ha expirat o no
        /// Requereix de la instal·lacio de la llibreria System.IdentityModel.Tokens.Jwt
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var expiration = jwt.ValidTo;
            return expiration < DateTime.UtcNow;
        }

        public static string GetUserId(string? token)
        {
            if (string.IsNullOrEmpty(token)) return "Anonymous";
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "nameid" || c.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId ?? "Unknown";
        }
        public static string GetUserName(string? token)
        {
            if (string.IsNullOrEmpty(token)) return "Anonymous";

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var username = jwt.Claims.FirstOrDefault(c => c.Type == "unique_name" || c.Type == "name" || c.Type == "sub" || c.Type == ClaimTypes.Name)?.Value;

            return username ?? "Unknown";
        }

        //Get User Rol
        public static string GetUserRole(string? token)
        {
            if (string.IsNullOrEmpty(token)) return "Anonymous";
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var role = jwt.Claims.FirstOrDefault(c => c.Type == "role" || c.Type == "roles" || c.Type == ClaimTypes.Role)?.Value;
            return role ?? "Unknown";
        }
    }
}
