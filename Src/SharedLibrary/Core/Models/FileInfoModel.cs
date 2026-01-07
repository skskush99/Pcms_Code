namespace Core.Models
{
    public class FileInfoModel
    {
        public string Name { get; set; }
        public string Parent { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public FileInfo FileInfo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
