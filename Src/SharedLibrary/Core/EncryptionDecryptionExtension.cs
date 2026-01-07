using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class EncryptionDecryptionExtension
    {

        public static string Decrypt(string encryptedText, string _secretKey)
        {
            try
            {
                byte[] fullCipher = Convert.FromBase64String(encryptedText);
                byte[] key = Encoding.UTF8.GetBytes(_secretKey); // 16 bytes for AES-128
                byte[] iv = Encoding.UTF8.GetBytes(_secretKey); // 16 bytes for IV

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(fullCipher, 0, fullCipher.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decryption error: " + ex.Message);
                throw;
            }
        }

        public static string Encrypt(string plainText, string _secretKey)
        {
            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] key = Encoding.UTF8.GetBytes(_secretKey); // 16 bytes for AES-128
                byte[] iv = Encoding.UTF8.GetBytes(_secretKey); // 16 bytes for IV

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = iv;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    {
                        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Encryption error: " + ex.Message);
                throw;
            }
        }
    }
}
