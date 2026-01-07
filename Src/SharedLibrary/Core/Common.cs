namespace Core
{
    public static class Common
    {
        public static string RegularMatchKey = "reg_";
        public static string ToSelfUrl(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            string outputStr = text.Trim().Replace(":", "").Replace("&", "").Replace(" ", "-").Replace("'", "").Replace(",", "").Replace("(", "").Replace(")", "").Replace("--", "").Replace(".", "");
            return Regex.Replace(outputStr.Trim().ToLower().Replace("--", ""), "[^a-zA-Z0-9_-]+", "", RegexOptions.Compiled);
        }

        public static string RandomString(int length)
        {
            Random Random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string ServiceResultGetMethod(string serviceUrl)
        {
            string target;
            var httpWReq = (HttpWebRequest)WebRequest.Create(serviceUrl);
            httpWReq.Method = "GET";
            httpWReq.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            var httpWResp = (HttpWebResponse)httpWReq.GetResponse();
            var resp = httpWResp.GetResponseStream();
            {
                if (httpWResp.ContentEncoding.ToLower().Contains("gzip"))
                    resp = new System.IO.Compression.GZipStream(resp, System.IO.Compression.CompressionMode.Decompress);
                else if (httpWResp.ContentEncoding.ToLower().Contains("deflate"))
                    resp = new System.IO.Compression.DeflateStream(resp, System.IO.Compression.CompressionMode.Decompress);

                var responseMessage = new StreamReader(resp).ReadToEnd();
                target = responseMessage;
            }
            return target;
        }

        public static string ServiceResultPostMethod(string url, string postdata)
        {
            string resxml;
            string requestString = postdata;
            byte[] postBytes = Encoding.ASCII.GetBytes(requestString);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.KeepAlive = false;
            request.Method = "POST";
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            try
            {
                Stream streamData = request.GetRequestStream();
                streamData.Write(postBytes, 0, postBytes.Length);
                HttpWebResponse httpWRes = (HttpWebResponse)request.GetResponse();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resp = response.GetResponseStream();
                {
                    if (httpWRes.ContentEncoding.ToLower().Contains("gzip"))
                        resp = new System.IO.Compression.GZipStream(resp, System.IO.Compression.CompressionMode.Decompress);
                    else if (httpWRes.ContentEncoding.ToLower().Contains("deflate"))
                        resp = new System.IO.Compression.DeflateStream(resp, System.IO.Compression.CompressionMode.Decompress);
                    String responseMessage = new StreamReader(resp).ReadToEnd();
                    resxml = responseMessage;
                }
            }
            catch (WebException webException)
            {
                WebResponse response = webException.Response;
                if (response != null)
                {
                    Stream stream = response.GetResponseStream();
                    String responseMessage = new StreamReader(stream).ReadToEnd();

                    resxml = responseMessage;
                }
                else
                {
                    resxml = "<Errors><error>Request Timed Out</error></Errors>";
                }
            }
            return resxml;
        }

        public static string GenerateHtmlFromUrl(string path)
        {
            try
            {
                using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
                {
                    string htmlCode = client.DownloadString(path);
                    return htmlCode;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return string.Empty;
        }

        public static bool HtmlToPdfbyContent(string htmlText, string fileName, string headerUrl, string footerUrl, string topM, string htopm, string fbotm)
        {
            // assemble destination PDF file name

            var exeWorkingDir = AppDomain.CurrentDomain.BaseDirectory + "\\WKHTML";
            System.Diagnostics.Process p =
                new System.Diagnostics.Process { StartInfo = { FileName = exeWorkingDir + "\\wkhtmltopdf.exe" } };


            dynamic switches = "--margin-right 0mm --margin-left 0mm --margin-top " + htopm + " --margin-bottom " + fbotm;
            if (!String.IsNullOrEmpty(headerUrl))
            {
                switches += " --header-line -T " + topM + " --header-html " + Uri.EscapeUriString(headerUrl);
            }
            if (!String.IsNullOrEmpty(footerUrl))
            {
                switches += " --footer-line --footer-html " + Uri.EscapeUriString(footerUrl);
            }
            switches += " --page-size A4 ";

            p.StartInfo.Arguments = switches + " " + "-" + " " + fileName;

            p.StartInfo.UseShellExecute = false;
            // needs to be false in order to redirect output
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            // redirect all 3, as it should be all 3 or none
            p.StartInfo.WorkingDirectory = exeWorkingDir;

            p.Start();
            dynamic sw = p.StandardInput;
            sw.Write(htmlText);
            sw.Close();

            // read the output here...
            p.StandardOutput.ReadToEnd();

            // ...then wait n milliseconds for exit (as after exit, it can't read the output)
            p.WaitForExit(60000);

            // read the exit code, close process
            var returnCode = p.ExitCode;
            p.Close();

            // if 0 or 2, it worked (not sure about other values, I want a better way to confirm this)
            return (returnCode <= 2);
        }

        public static string PasswordMatch(string password, string hash)
        {
            var res = VerifyHashedPassword(hash, password);
            return res ? $"matched" : "notmatch";
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            try
            {
                byte[] buffer4;
                if (string.IsNullOrEmpty(hashedPassword))
                    return false;

                if (string.IsNullOrEmpty(password))
                    throw new ArgumentNullException(nameof(password));

                var src = Convert.FromBase64String(hashedPassword);
                if (src.Length != 0x31 || src[0] != 0)
                    return false;

                var dst = new byte[0x10];
                Buffer.BlockCopy(src, 1, dst, 0, 0x10);

                var buffer3 = new byte[0x20];
                Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

                using (var bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
                    buffer4 = bytes.GetBytes(0x20);
                return buffer3.SequenceEqual(buffer4);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public class AsyncLocker<T>
        {
            private readonly LazyDictionary<T, SemaphoreSlim> semaphoreDictionary = new LazyDictionary<T, SemaphoreSlim>();

            public async Task<IDisposable> LockAsync(T key)
            {
                var semaphore = semaphoreDictionary.GetOrAdd(key, () => new SemaphoreSlim(1, 1));
                await semaphore.WaitAsync();
                return new ActionDisposable(() => semaphore.Release());
            }
        }

        public class LazyDictionary<TKey, TValue>
        {
            //here we use Lazy<TValue> as the value in the dictionary
            //to guard against the fact the the initializer function
            //in ConcurrentDictionary.AddOrGet can, under some conditions, 
            //run more than once per key, with the result of all but one of 
            //the runs being discarded. 
            //If this happens, only uninitialized
            //Lazy values are discarded. Only the Lazy that actually 
            //made it into the dictionary is materialized by accessing
            //its Value property.
            private readonly ConcurrentDictionary<TKey, Lazy<TValue>> dictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>();
            public TValue GetOrAdd(TKey key, Func<TValue> valueGenerator)
            {
                var lazyValue = dictionary.GetOrAdd(key, k => new Lazy<TValue>(valueGenerator));
                return lazyValue.Value;
            }
        }
        public sealed class ActionDisposable : IDisposable
        {
            //useful for making arbitrary IDisposable instances
            //that perform an Action when Dispose is called
            //(after a using block, for instance)
            private readonly Action action;
            public ActionDisposable(Action action)
            {
                this.action = action;
            }
            public void Dispose()
            {
                var action = this.action;
                if (action != null)
                {
                    action();
                }
            }
        }


        private static readonly byte[] Key = Encoding.UTF8.GetBytes("HqE4dUkwLbXzR0IG"); // 16 bytes for AES-128
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("HqE4dUkwLbXzR0IG"); // 16 bytes for IV
        public static string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        public static string Decrypt(string cipherText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

    }
}
