namespace Client.ViewModel
{
    public class FileVM
    {
        public int FileId { get; set; } = 0;
        public string FileName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public List<FileVM> Files { get; set; } = new List<FileVM>();
    }
}
