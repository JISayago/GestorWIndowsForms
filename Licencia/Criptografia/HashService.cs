using System.Security.Cryptography;
using System.Text;

namespace Licencia.Criptografia
{
    public static class HashService
    {
        public static byte[] CalcularHash(byte[] datos)
        {
            using var sha = SHA256.Create();
            return sha.ComputeHash(datos);
        }

        public static byte[] CalcularHash(string texto)
        {
            return CalcularHash(Encoding.UTF8.GetBytes(texto));
        }

        public static string CalcularHashBase64(string texto)
        {
            return Convert.ToBase64String(CalcularHash(texto));
        }
    }
}