using System.Security.Cryptography;
using System.Text;

namespace Core.SsoEncryption
{
    public static class AES
    {
        private static string ConvertToBase64(string planText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(planText));
        }
        private static string ConvertFromBase64(string base64String)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        }
        private static RijndaelManaged GetRijndaelManaged(string secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }
        public static string Encrypt(string plainText, string encryptionKey)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                var cipher = GetRijndaelManaged(encryptionKey);
                return  Convert.ToBase64String(cipher.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
