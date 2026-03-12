using CCTNSDto;
using CCTNSDto.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Buffers.Text;
using System.Data;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace CCTNSServiceBus.CCTNS
{
    public class CCTNSService : ICCTNSService
    {
        private readonly string encryptionKey = "897J4n32yd323K09vf9E654328756431";
        private const string EncryptionAlgoType = "AES";
        private const string ALGO = "AES/GCM/NoPadding";
        private const string UtfFormat = "UTF-8";

        private const int GcmTagSize = 16;
        private const int GcmNonceSize = 12;


        // =========================
        // Generate Random IV
        // =========================
        public string GenerateRandomIV()
        {
            int length = 16;
            byte[] aesKey = new byte[16];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(aesKey);
            }

            StringBuilder result = new StringBuilder();
            foreach (byte b in aesKey)
            {
                result.Append(b.ToString("x2")); // hex
            }

            string hex = result.ToString();

            return length > hex.Length ? hex : hex.Substring(0, length);
        }

        // =========================
        // Encrypt
        // =========================

        public string Encrypt(string value, string initVector, string encryptionKey)
        {
            try
            {
                byte[] ivBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(value);

                using (Aes aes = Aes.Create())
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7; // PKCS5 == PKCS7 in .NET
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    using (ICryptoTransform encryptor = aes.CreateEncryptor())
                    {
                        byte[] encryptedBytes =
                            encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MasterCrypto Encrypt Error: " + ex);
            }
            return null;
        }

        // =========================
        // Decrypt
        // =========================
        public string Decrypt(string encrypted, string initVector, string encryptionKey)
        {
            try
            {
                byte[] ivBytes = Encoding.UTF8.GetBytes(initVector);
                byte[] keyBytes = Encoding.UTF8.GetBytes(encryptionKey);
                byte[] cipherTextBytes = Convert.FromBase64String(encrypted);

                using (Aes aes = Aes.Create())
                {
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        byte[] originalBytes =
                            decryptor.TransformFinalBlock(cipherTextBytes, 0, cipherTextBytes.Length);

                        return Encoding.UTF8.GetString(originalBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MasterCrypto Decrypt Error: " + ex);
            }

            return null;
        }

        // ================================
        // MAIN PUBLIC METHOD
        // ================================       
        public async Task<ResponseWithoutPaginationModel> GetClientAppToken(CCTNSCredentials data)
        {
            ResponseWithoutPaginationModel result = new();

            try
            {
                CCTNSService crypto = new CCTNSService();

                string encryptionKey = "897J4n32yd323K09vf9E654328756431";

                // Payload (same as console)
                var payload = new
                {
                    clientId = data.ClientId,
                    clientSecret = data.ClientSecret
                };

                string payloadJson = JsonConvert.SerializeObject(payload);

                // Generate IV
                string iv = crypto.GenerateRandomIV();

                // Encrypt payload
                string encryptedPayload = crypto.Encrypt(payloadJson, iv, encryptionKey);

                // Encode IV to Base64 (IMPORTANT)
                string encodedIV = Convert.ToBase64String(Encoding.UTF8.GetBytes(iv));

                var requestBody = new
                {
                    v1 = encryptedPayload,
                    v2 = encodedIV
                };

                string jsonRequest = JsonConvert.SerializeObject(requestBody);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("clientId", data.ClientId);

                    var content = new StringContent(
                        jsonRequest,
                        Encoding.UTF8,
                        "application/json"
                    );

                    var response = await client.PostAsync(data.BaseUrl, content);

                    string responseText = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        result.Status = false;
                        result.Message = responseText;
                        return result;
                    }

                    dynamic apiResponse = JsonConvert.DeserializeObject(responseText);

                    string responseV1 = apiResponse.v1;
                    string responseV2 = apiResponse.v2;

                    // Decode IV from Base64
                    string responseIV = Encoding.UTF8.GetString(
                        Convert.FromBase64String(responseV2));

                    // Decrypt response
                    string decryptedResponse = crypto.Decrypt(
                        responseV1,
                        responseIV,
                        encryptionKey
                    );

                    result.Status = true;
                    result.Message = "Success";
                    result.Data = JsonConvert.DeserializeObject<ExpandoObject>(decryptedResponse);
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }

            return result;
        }

        // ================================
        // Get FIR Details METHOD
        // ================================
        // 
        // =========================
        // FIREncrypt
        // =========================

        public string FIREncrypt(string plainText, string secretKey, string ivBase64)
        {
            try
            {
                byte[] iv = Convert.FromBase64String(ivBase64);
                byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                byte[] cipherText = new byte[plainBytes.Length];
                byte[] tag = new byte[GcmTagSize];

                using (AesGcm aesGcm = new AesGcm(keyBytes))
                {
                    aesGcm.Encrypt(
                        nonce: iv,
                        plaintext: plainBytes,
                        ciphertext: cipherText,
                        tag: tag
                    );
                }

                // Java GCM returns: ciphertext + tag
                byte[] combined = new byte[cipherText.Length + tag.Length];
                Buffer.BlockCopy(cipherText, 0, combined, 0, cipherText.Length);
                Buffer.BlockCopy(tag, 0, combined, cipherText.Length, tag.Length);

                return Convert.ToBase64String(combined);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MasterCryptoAESGCM Encrypt: " + ex.Message);
            }

            return null;
        }

        // =========================
        // FIRDecrypt
        // =========================
        public string FIRDecrypt(string cipherTextEncoded, string secretKey, string ivBase64)
        {
            try
            {
                byte[] iv = Convert.FromBase64String(ivBase64);
                byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
                byte[] combined = Convert.FromBase64String(cipherTextEncoded);

                byte[] cipherText = new byte[combined.Length - GcmTagSize];
                byte[] tag = new byte[GcmTagSize];

                Buffer.BlockCopy(combined, 0, cipherText, 0, cipherText.Length);
                Buffer.BlockCopy(combined, cipherText.Length, tag, 0, tag.Length);

                byte[] plainText = new byte[cipherText.Length];

                using (AesGcm aesGcm = new AesGcm(keyBytes))
                {
                    aesGcm.Decrypt(
                        nonce: iv,
                        ciphertext: cipherText,
                        tag: tag,
                        plaintext: plainText
                    );
                }

                return Encoding.UTF8.GetString(plainText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in MasterCryptoAESGCM Decrypt: " + ex.Message);
            }

            return null;
        }


        public string GenerateBase64IV()
        {
            byte[] iv = new byte[GcmNonceSize];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(iv);
            }

            return Convert.ToBase64String(iv);
        }

        public async Task<ResponseWithoutPaginationModel> GetFIRDetails(CCTNSCredentials data, string firNum)
        {
            ResponseWithoutPaginationModel result = new();

            try
            {
                var urls = "https://policetraining.rajasthan.gov.in/psa-client/firDetails";
                string url = urls;
                string secretKey = "348U7c55wd348L07ah7E333443345678";


                CCTNSService cryptoAESGCM = new CCTNSService();

                // Payload (same as console)
                var payload = new
                {
                    clientId = data.ClientId,
                    clientSecret = secretKey
                };
                string payloadJson = JsonConvert.SerializeObject(payload);

                var plainText = new
                {
                    //firNum = "27564051250030",
                    firNum = firNum
                };
                string plainTextJsons = JsonConvert.SerializeObject(plainText);                

                // Generate IV
                string iv = cryptoAESGCM.GenerateBase64IV();

                // Encrypt payload
                string encryptedPayload = cryptoAESGCM.FIREncrypt(plainTextJsons, secretKey, iv);

                // Encode IV to Base64 (IMPORTANT)
                string encodedIV = Convert.ToBase64String(Encoding.UTF8.GetBytes(iv));

                var requestBody = new
                {
                    v1 = encryptedPayload,
                    v2 = iv
                };
                var BearerToken = GetClientAppToken(data).Result;

                Dictionary<string, string> tokenData = new Dictionary<string, string>();

                foreach (var row in BearerToken.Data)
                {
                    var pair = (KeyValuePair<string, object>)row;

                    tokenData.Add(
                        pair.Key,
                        pair.Value.ToString()
                    );
                }

                string jwtToken = tokenData["jwtToken"];
                string status = tokenData["status"];
                string message = tokenData["message"];
                if (status == "True")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
                        client.DefaultRequestHeaders.Add("clientId", data.ClientId);

                        var json = JsonConvert.SerializeObject(requestBody);

                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(url, content);

                        string responseData = await response.Content.ReadAsStringAsync();

                        if (!response.IsSuccessStatusCode)
                        {
                            result.Status = false;
                            result.Message = responseData;
                            return result;
                        }

                        // Fix double JSON response
                        string cleanJson = JsonConvert.DeserializeObject<string>(responseData);

                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(cleanJson);


                        // Decrypt response
                        string decrypted = cryptoAESGCM.FIRDecrypt(apiResponse.v1, secretKey, apiResponse.v2);

                        result.Status = true;
                        result.Message = "Success";
                        result.Data = JsonConvert.DeserializeObject<ExpandoObject>(decrypted);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public class ApiResponse
        {
            public string v1 { get; set; }
            public string v2 { get; set; }
        }

    }
}