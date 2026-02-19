using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Report.Repository.Global
{
    public class Cryptology
    {
        #region EncryptDecrypt Number
        public string EncryptNumber(string input)
        {
            //return Server.UrlEncode(Encrypt3(input));
            return TripleDESEncrypt(input);
        }
        public string EncryptID(int input)
        {
            //return Server.UrlEncode(Encrypt3(input));
            return TripleDESEncryptID(input);
        }

        public string EncryptNumber(string input, int i)
        {
            //return Encrypt3(input);
            return TripleDESEncrypt(input);
        }
        public string DecryptNumber(string input)
        {
            //return Decrypt3(Server.UrlDecode(input));
            return TripleDESDecrypt(input);
        }

        public int DecryptID(string input)
        {
            //return Decrypt3(Server.UrlDecode(input));
            return TripleDESDecryptID(input);
        }
        #endregion

        #region TripleDESEncryptDecrypt

        string tbKey = "a1b2c3d4e5";

        private string TripleDESEncrypt(string tbMessage)
        {
            try
            {
                using (TripleDES threedes = new TripleDESCryptoServiceProvider())
                {
                    threedes.Key = StringToByte(tbKey, 24); // convert to 24 characters - 192 bits
                    threedes.IV = StringToByte("12345678");
                    byte[] key = threedes.Key;
                    byte[] IV = threedes.IV;

                    ICryptoTransform encryptor = threedes.CreateEncryptor(key, IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                        // Write all data to the crypto stream and flush it.
                        csEncrypt.Write(StringToByte(tbMessage), 0, StringToByte(tbMessage).Length);
                        csEncrypt.FlushFinalBlock();

                        // Get the encrypted array of bytes.
                        byte[] encrypted = msEncrypt.ToArray();

                        return ByteToString(encrypted);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        private string TripleDESEncryptID(int Message)
        {
            string tbMessage = Message.ToString();
            try
            {

                using (TripleDES threedes = new TripleDESCryptoServiceProvider())
                {
                    threedes.Key = StringToByte(tbKey, 24); // convert to 24 characters - 192 bits
                    threedes.IV = StringToByte("12345678");
                    byte[] key = threedes.Key;
                    byte[] IV = threedes.IV;

                    ICryptoTransform encryptor = threedes.CreateEncryptor(key, IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                        // Write all data to the crypto stream and flush it.
                        csEncrypt.Write(StringToByte(tbMessage), 0, StringToByte(tbMessage).Length);
                        csEncrypt.FlushFinalBlock();

                        // Get the encrypted array of bytes.
                        byte[] encrypted = msEncrypt.ToArray();

                        return ByteToString(encrypted);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        private string TripleDESDecrypt(string encryptedMessage)
        {
            try
            {
                using (TripleDES threedes = new TripleDESCryptoServiceProvider())
                {
                    threedes.Key = StringToByte(tbKey, 24); // convert to 24 characters - 192 bits
                    threedes.IV = StringToByte("12345678");
                    byte[] key = threedes.Key;
                    byte[] IV = threedes.IV;

                    ICryptoTransform decryptor = threedes.CreateDecryptor(key, IV);

                    // Now decrypt the previously encrypted message using the decryptor
                    byte[] encrypted = StringToByteDecimal(encryptedMessage);
                    using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                    {
                        CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                        return ByteToString(csDecrypt);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        private int TripleDESDecryptID(string encryptedMessage)
        {
            int decraptvalue = 0;
            try
            {
                if (encryptedMessage != "0")
                {
                    using (TripleDES threedes = new TripleDESCryptoServiceProvider())
                    {
                        threedes.Key = StringToByte(tbKey, 24); // convert to 24 characters - 192 bits
                        threedes.IV = StringToByte("12345678");
                        byte[] key = threedes.Key;
                        byte[] IV = threedes.IV;

                        ICryptoTransform decryptor = threedes.CreateDecryptor(key, IV);

                        // Now decrypt the previously encrypted message using the decryptor
                        byte[] encrypted = StringToByteDecimal(encryptedMessage);
                        using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                        {
                            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                            decraptvalue = int.Parse(ByteToString(csDecrypt));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               // EventLog.WriteError(ex.ToString());
            }
            return decraptvalue;
        }

        public static byte[] StringToByteDecimal(string StringToConvert)
        {
            int j = 0;
            string dec = "";
            byte[] ByteArray = new byte[StringToConvert.Length / 2];
            for (int i = 0; i < StringToConvert.Length; i = i + 2)
            {
                dec = HexToDec(StringToConvert.Substring(i, 2));
                ByteArray[j] = Convert.ToByte(dec);
                j++;
            }
            return ByteArray;
        }

        public static byte[] StringToByte(string StringToConvert)
        {

            char[] CharArray = StringToConvert.ToCharArray();
            byte[] ByteArray = new byte[CharArray.Length];
            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
            }
            return ByteArray;
        }
        public static byte[] StringToByte(string StringToConvert, int length)
        {

            char[] CharArray = StringToConvert.ToCharArray();
            byte[] ByteArray = new byte[length];
            for (int i = 0; i < CharArray.Length; i++)
            {
                ByteArray[i] = Convert.ToByte(CharArray[i]);
            }
            return ByteArray;
        }
        public static string ByteToString(CryptoStream buff)
        {
            string sbinary = "";
            int b = 0;
            do
            {
                b = buff.ReadByte();
                if (b != -1) sbinary += ((char)b);

            } while (b != -1);
            return (sbinary);
        }
        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        private static string HexToDec(string hexNum)
        {
            return Convert.ToString(int.Parse(hexNum, System.Globalization.NumberStyles.HexNumber));
        }
        #endregion

        #region EncryptDecrypt2
        // The key used for generating the encrypted string
        //   private const string cryptoKey = "EncryptDecryptCM";

        //// The Initialization Vector for the DES encryption routine
        // private readonly byte[] IV = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };

        public string Encrypt2(string input)
        {
            return encrypt2Detail(input);
        }

        public string encrypt2Detail(string input)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(input);
            using (TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider())
                {
                    des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                    des.IV = IV;
                    return Convert.ToBase64String(
                        des.CreateEncryptor().TransformFinalBlock(
                            buffer,
                            0,
                            buffer.Length
                        )
                    );
                }
            }
        }


        public string Decrypt2(string input)
        {
            return decrypt2Detail(input);
        }

        public string decrypt2Detail(string input)
        {
            byte[] buffer = Convert.FromBase64String(input);
            using (TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider())
                {
                    des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                    des.IV = IV;
                    return Encoding.ASCII.GetString(
                        des.CreateDecryptor().TransformFinalBlock(
                            buffer,
                            0,
                            buffer.Length
                        )
                    );
                }
            }
        }

        #endregion

        #region EncryptDecrypt3
        public string Encrypt3(string strText)
        {
            return Encrypt3Detail(strText, "&%#@?,:*");
        }

        public string Decrypt3(string strText)
        {
            return Decrypt3Detail(strText, "&%#@?,:*");
        }

        private string Encrypt3Detail(string strText, string strEncrKey)
        {
            byte[] byKey;
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string Decrypt3Detail(string strText, string sDecrKey)
        {
            byte[] byKey;
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };

            byte[] inputByteArray;
            // inputByteArray.Length = strText.Length;

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    inputByteArray = Convert.FromBase64String(strText);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                        return encoding.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region EncryptDecrypt4

        public string encrypt4(string strText)
        {
            return Encrypt4Detail(strText, "&%#@?,:*");
        }

        public string decrypt4(string str)
        {
            return Decrypt4Detail(str, "&%#@?,:*");
        }

        private string Encrypt4Detail(string strText, string strEncrypt)
        {
            byte[] byKey = new byte[20];
            byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(strText);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write);

                        cs.Write(inputArray, 0, inputArray.Length);
                        cs.FlushFinalBlock();

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Decrypt4Detail(string strText, string strEncrypt)
        {
            byte[] bKey = new byte[20];
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            try
            {
                bKey = System.Text.Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    Byte[] inputByteArray = inputByteArray = Convert.FromBase64String(strText);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);

                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                        return encoding.GetString(ms.ToArray());
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region EncryptDecrypt PasswordSalt

        public string PasswordSaltEnc(string input)
        {
            return (encrypt4(Encrypt3(Encrypt2(input))));
        }
        public string PasswordSaltDec(string input)
        {
            return (Decrypt2(Decrypt3(decrypt4(input))));
        }

        #endregion

        #region EncryptionDecryptionQuerystring

        #region EncryptQueryString
        public string EncryptQueryString(string input)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
            string result = Convert.ToBase64String(b);
            return EncryptedString(System.Web.HttpUtility.UrlDecode(result));
        }
        #endregion

        #region DecryptQueryString
        public string DecryptQueryString(string input)
        {
            byte[] b = Convert.FromBase64String(DecryptedString(input));
            return System.Text.ASCIIEncoding.ASCII.GetString(b);
        }
        #endregion

        #endregion

        #region EncryptionDecryptionQuerystring1

        #region DecryptedString
        public string DecryptedString(string input)
        {
            return decrypt(input);
        }

        public string decrypt(string encryptedQueryString)
        {
            byte[] buffer = Convert.FromBase64String(encryptedQueryString);
            using (TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider())
                {
                    des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                    des.IV = IV;
                    return Encoding.ASCII.GetString(
                        des.CreateDecryptor().TransformFinalBlock(
                            buffer,
                            0,
                            buffer.Length
                        )
                    );
                }
            }
        }

        #endregion

        #region EncryptedString

        public string EncryptedString(string input)
        {

            return HttpUtility.UrlEncode(encrypt(input));
        }

        public string encrypt(string serializedQueryString)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(serializedQueryString);
            using (TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider())
                {
                    des.Key = MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(cryptoKey));
                    des.IV = IV;
                    return Convert.ToBase64String(
                        des.CreateEncryptor().TransformFinalBlock(
                            buffer,
                            0,
                            buffer.Length
                        )
                    );
                }
            }
        }

        #endregion

        #region variables

        // The key used for generating the encrypted string
        private const string cryptoKey = "ChangeThis!";

        // The Initialization Vector for the DES encryption routine
        private readonly byte[] IV = new byte[8] { 240, 3, 45, 29, 0, 76, 173, 59 };

        #endregion

        #endregion
    }
}
