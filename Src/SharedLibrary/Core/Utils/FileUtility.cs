using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    internal class FileUtility
    {
        public static string GetLastLineOfTextFile(string filePath, bool readAllLines = false)
        {
            try
            {
                //string flNameUpdate = Server.MapPath("~/Logs/ApplicaionUpdate.txt");
                if (System.IO.File.Exists(filePath) == false)
                {
                    return "File " + filePath + " does not exist. Please check file name and path.";
                }
                else if (readAllLines == true)
                {
                    int index = 1;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div>");
                    foreach (string line in System.IO.File.ReadLines(filePath))
                    {
                        sb.Append(index.ToString() + " - " + line + "</br>");
                        index = index + 1;
                    }
                    sb.Append("</div>");
                    return sb.ToString();
                }
                else
                {
                    string lastLine = System.IO.File.ReadLines(filePath).Last();
                    return lastLine;
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public static void FillDirectories(string parentDirectoryPath, List<FileInfoModel> lst)
        {
            try
            {
                foreach (string directoryPath in Directory.GetDirectories(parentDirectoryPath))
                {
                    lst.Add(new FileInfoModel() { Name = Path.GetFileName(directoryPath), Parent = parentDirectoryPath, Path = directoryPath });
                    FillDirectories(directoryPath, lst);
                }
            }
            catch (Exception)
            {

            }
        }
        public static List<FileInfoModel> GetFiles(string parentDirectoryPath)
        {
            List<FileInfoModel> lst = new List<FileInfoModel>();
            try
            {

                foreach (string filePath in Directory.GetFiles(parentDirectoryPath))
                {
                    lst.Add(new FileInfoModel() { Name = Path.GetFileName(filePath), Parent = parentDirectoryPath, Path = filePath, FileInfo = new FileInfo(filePath), CreatedOn = File.GetCreationTime(filePath), Extension = Path.GetExtension(filePath), UpdatedOn = File.GetLastWriteTime(filePath) });
                }
            }
            catch (Exception)
            {

            }
            return lst;
        }
        public static byte[] ConvertFileToByteArray(string filePath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(filePath,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(filePath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }
        public static int GetDocumentPageCountandSizeInKb(string Extention, string fileName, out Int64 size)
        {
            try
            {
                string ImageFiles = "jpg,png,gif,bmp";
                int PageCount = 1;

                using var file = System.IO.File.OpenRead(fileName);
                size = file.Length / 1024;
                if (Extention.ToLower().Contains("pdf"))
                {
                    StreamReader sr = new StreamReader(file);
                    Regex regex = new Regex(@"/Type\s*/Page[^s]");
                    MatchCollection matches = regex.Matches(sr.ReadToEnd());
                    PageCount = matches.Count;
                    sr.Close();
                }
                else if (ImageFiles.ToUpper().Split(',').Contains(Extention.ToUpper()))
                {
                    PageCount = 1;
                }

                file.Close();

                return PageCount;
            }
            catch (Exception)
            {
                size = 0;
                return 1;
            }
        }
    }
}
