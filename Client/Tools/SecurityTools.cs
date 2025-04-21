using System.Security.Cryptography;
using System.Text;

namespace Client.Tools
{
    public static class SecurityTools
    {
        public static string EncriptPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(password)).ToString();
        }
    }
}