using System.Security.Cryptography;
using System.Text;

namespace Licencia.Criptografia
{
    public static class RsaVerifier
    {
        public static bool VerificarFirma(
            string datos,
            string firmaBase64,
            string publicKeyPem)
        {
            using RSA rsa = RSA.Create();

            rsa.ImportFromPem(publicKeyPem);

            byte[] datosBytes =
                Encoding.UTF8.GetBytes(datos);

            byte[] firma =
                Convert.FromBase64String(
                    firmaBase64);

            return rsa.VerifyData(
                datosBytes,
                firma,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);
        }
    }
}