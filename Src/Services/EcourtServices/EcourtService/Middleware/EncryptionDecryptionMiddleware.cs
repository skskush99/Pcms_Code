using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Core;

namespace EcourtService.Middleware
{
    public class EncryptionDecryptionMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly string _secretKey;
        private IConfiguration Configuration;
        public EncryptionDecryptionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            this.Configuration = configuration;
            _secretKey = this.Configuration["Jwt:SecretKey"]; // Retrieve from configuration
        }

        public async Task Invoke(HttpContext context)
        {
            //if (context.Request.Headers.TryGetValue("X-Encrypted-Payload", out var encryptedPayloadHeader))
            //{
            try
            {
                // Check if the request is from Swagger
                if (IsSwaggerRequest(context))
                {
                    await _next(context); // Skip processing for Swagger requests
                    return;
                }

                if (context.Request.ContentType != null && context.Request.ContentType.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase))
                {
                    await _next(context);
                }
                else
                {
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                    {
                        string encryptedData = await reader.ReadToEndAsync();
                        var jsonData = JsonConvert.DeserializeObject<JObject>(encryptedData);
                        if (jsonData != null)
                        {
                            string secretDataKey = jsonData["data"] == null ? "" : Convert.ToString(jsonData["data"].Value<string>());
                            if (!string.IsNullOrEmpty(secretDataKey))
                            {
                                string decryptedPayload = EncryptionDecryptionExtension.Decrypt(secretDataKey, _secretKey);
                                var requestheader = JsonConvert.SerializeObject(context.Request.Headers);

                                if (!string.IsNullOrEmpty(decryptedPayload))
                                {
                                    var decryptedBytes = Encoding.UTF8.GetBytes(decryptedPayload);
                                    context.Request.Body = new MemoryStream(decryptedBytes);
                                    context.Request.ContentLength = decryptedBytes.Length;
                                    context.Request.ContentType = "application/json";
                                    context.Request.Body.Position = 0;
                                }
                                else
                                {
                                    await _next(context); // Skip processing for Swagger requests
                                    return;
                                }
                            }
                            else
                            {
                                await _next(context); // Skip processing for Swagger requests
                                return;
                            }
                        }
                        else
                        {
                            await _next(context); // Skip processing for Swagger requests
                            return;
                        }
                    }

                    var originalBodyStream = context.Response.Body;
                    using (var newBodyStream = new MemoryStream())
                    {
                        context.Response.Body = newBodyStream;

                        await _next(context);
                        newBodyStream.Seek(0, SeekOrigin.Begin);
                        string rawBody = await new StreamReader(newBodyStream).ReadToEndAsync();
                        newBodyStream.Seek(0, SeekOrigin.Begin);

                        if (!string.IsNullOrEmpty(rawBody))
                        {
                            try
                            {
                                //var response = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(rawBody);
                                //var status = response.GetProperty("status").GetBoolean();
                                //if (status == true)
                                //{
                                string encryptedResponse = EncryptionDecryptionExtension.Encrypt(rawBody, _secretKey);

                                var responseObject = new
                                {
                                    Data = encryptedResponse,
                                };
                                // Serialize the response model to JSON
                                string jsonResponse = System.Text.Json.JsonSerializer.Serialize(responseObject);
                                byte[] encryptedBytes = Encoding.UTF8.GetBytes(jsonResponse);
                                var data = originalBodyStream.WriteAsync(encryptedBytes, 0, encryptedBytes.Length);
                                //}
                                //else
                                //{
                                //    // If there's no response, just copy the original body
                                //    await newBodyStream.CopyToAsync(originalBodyStream);
                                //}
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }

                        //context.Response.Body.Seek(0, SeekOrigin.Begin);
                        //using (var reader = new StreamReader(context.Response.Body))
                        //{
                        //    string responseBody = await reader.ReadToEndAsync();
                        //    string encryptedResponse = Encrypt(responseBody);

                        //    var responseObject = new
                        //    {
                        //        Data = encryptedResponse,
                        //    };
                        //    // Serialize the response model to JSON
                        //    string jsonResponse = System.Text.Json.JsonSerializer.Serialize(responseObject);
                        //    byte[] encryptedBytes = Encoding.UTF8.GetBytes(jsonResponse);
                        //    var data = originalBodyStream.WriteAsync(encryptedBytes, 0, encryptedBytes.Length);
                        //}
                    }
                }
            }
            catch (CryptographicException ex)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authentication failed.");
                return;
            }
            catch (System.Text.Json.JsonException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid payload format.");
                return;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An error occurred.");
                return;
            }
            //}
        }

        private bool IsSwaggerRequest(HttpContext context)
        {
            // Check User-Agent header
            if (context.Request.Headers.TryGetValue("Referer", out var Referer) &&
                Referer.ToString().Contains("Swagger", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Check request path
            if (context.Request.Path.StartsWithSegments("/swagger", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
        //public string Decrypt(string encryptedText)
        //{
        //    try
        //    {
        //        byte[] fullCipher = Convert.FromBase64String(encryptedText);
        //        byte[] key = Encoding.UTF8.GetBytes(_secretKey); // 16 bytes for AES-128
        //        byte[] iv = Encoding.UTF8.GetBytes(_secretKey); // 16 bytes for IV

        //        using (Aes aes = Aes.Create())
        //        {
        //            aes.Key = key;
        //            aes.IV = iv;
        //            aes.Mode = CipherMode.CBC;
        //            aes.Padding = PaddingMode.PKCS7;
        //            using (ICryptoTransform decryptor = aes.CreateDecryptor())
        //            {
        //                byte[] decryptedBytes = decryptor.TransformFinalBlock(fullCipher, 0, fullCipher.Length);
        //                return Encoding.UTF8.GetString(decryptedBytes);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Decryption error: " + ex.Message);
        //        throw;
        //    }
        //}
        //public string Encrypt(string plainText)
        //{
        //    try
        //    {
        //        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        //        byte[] key = Encoding.UTF8.GetBytes(_secretKey); // 16 bytes for AES-128
        //        byte[] iv = Encoding.UTF8.GetBytes(_secretKey); // 16 bytes for IV

        //        using (Aes aes = Aes.Create())
        //        {
        //            aes.Key = key;
        //            aes.IV = iv;
        //            aes.Mode = CipherMode.CBC;
        //            aes.Padding = PaddingMode.PKCS7;

        //            using (ICryptoTransform encryptor = aes.CreateEncryptor())
        //            {
        //                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        //                return Convert.ToBase64String(encryptedBytes);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Encryption error: " + ex.Message);
        //        throw;
        //    }
        //}
    }
}
