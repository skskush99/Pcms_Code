using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Security
{
    public class EncryptionUtility
    {
        const string key = "cryptogr";
        public static string Encrypt(string Source, string encryptionKey = null)
        {
            try
            {
                byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
                if (string.IsNullOrWhiteSpace(encryptionKey))
                    encryptionKey = key;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider();

                byte[] bytKey = System.Text.Encoding.UTF8.GetBytes(encryptionKey.Substring(0, 8));

                //     byte[] bytKey = GetLegalKey(key, mobjCryptoService);
                mobjCryptoService.Key = bytKey;
                mobjCryptoService.IV = bytKey;

                ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();

                byte[] bytOut = ms.GetBuffer();

                int i = 0;

                for (i = 0; i < bytOut.Length; i++)

                    if (bytOut[i] == 0)

                        if (bytOut[i + 1] != null)

                            if (bytOut[i + 1] == 0)

                                if (bytOut[i + 2] != null)

                                    if (bytOut[i + 2] == 0)

                                        break;

                if (i % 2 == 1)

                    i++;

                return System.Convert.ToBase64String(bytOut, 0, i);
                //return Encrypt1(Source);

            }

            catch (Exception)
            {
                throw;
            }
        }
        public static string Decrypt(string Source, string decryptionKey = null)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(decryptionKey))
                    decryptionKey = key;
                byte[] bytIn = System.Convert.FromBase64String(Source.Replace(' ', '+'));

                DESCryptoServiceProvider mobjCryptoService = new DESCryptoServiceProvider();

                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

                ms.Position = 0;

                byte[] bytKey = System.Text.Encoding.UTF8.GetBytes(decryptionKey.Substring(0, 8));
                mobjCryptoService.Key = bytKey;
                mobjCryptoService.IV = bytKey;

                ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

                System.IO.StreamReader sr = new System.IO.StreamReader(cs);
                return sr.ReadToEnd();
                //return Decrypt1(Source);
            }
            catch (Exception)
            {
                return "q=" + Source;
            }
        }
        public static string GenerateHashKey(string sessionId)
        {
            StringBuilder myStr = new StringBuilder();
            //myStr.Append(HttpContext.Current.Request.Browser.Browser);
            //myStr.Append(HttpContext.Current.Request.Browser.Platform);
            //myStr.Append(HttpContext.Current.Request.Browser.MajorVersion);
            //myStr.Append(HttpContext.Current.Request.Browser.MinorVersion);

            //myStr.Append(HttpContext.Current.Request.LogonUserIdentity.User.Value);
            SHA1 sha = new SHA1CryptoServiceProvider();
            //byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value + myStr.ToString()));
            //return Convert.ToBase64String(hashdata);
            return Encrypt(sessionId + myStr.ToString());
        }
        public static string GenerateMD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public static string GenerateAuthKey(HttpRequest request, string loginLogId)
        {
            StringBuilder myStr = new StringBuilder();
            //if (request != null)
            //{
            //    myStr.Append(request.Headers["User-Agent"].ToString().ToLower());
            //}
            return Encrypt(loginLogId + myStr.ToString());
        }
        public static string GenerateAuthId(Int64 useId, Int64 loginLogId, Int64 actualUserId, Int16 userTypeId = 0)
        {
            return Encrypt(useId.ToString() + "~" + loginLogId.ToString() + "~" + actualUserId.ToString() + "~" + userTypeId.ToString());
        }



        // Encryption Decryption by specific key .
        private static byte[] byteKey;
        private static SymmetricAlgorithm secretKey;

        public static void SetKey(string myKey)
        {
            using (SHA1Managed sha = new SHA1Managed())
            {
                byteKey = Encoding.UTF8.GetBytes(myKey);
                byteKey = sha.ComputeHash(byteKey);
                //key = new ArraySegment<byte>(key, 0, 16).ToArray();
                byteKey = byteKey.Take(16).ToArray();
                secretKey = new RijndaelManaged
                {
                    KeySize = 128,
                    Key = byteKey,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };
            }
        }


        public static string EncryptByKey(string strToEncrypt, string secret)
        {
            try
            {
                SetKey(secret);
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = byteKey;
                    aesAlg.Mode = CipherMode.ECB;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    byte[] encryptedBytes;

                    using (var msEncrypt = new System.IO.MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(strToEncrypt);
                            }
                            encryptedBytes = msEncrypt.ToArray();
                        }
                    }

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while encrypting: " + e.ToString());
            }
            return null;
        }

        public static string DecryptByKey(string strToDecrypt, string secret)
        {
            try
            {
                SetKey(secret);
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = byteKey;
                    aesAlg.Mode = CipherMode.ECB;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    byte[] encryptedBytes = Convert.FromBase64String(strToDecrypt);
                    string plaintext = null;

                    using (var msDecrypt = new System.IO.MemoryStream(encryptedBytes))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return plaintext;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while decrypting: " + e.ToString());
            }
            return null;
        }
    }
}
