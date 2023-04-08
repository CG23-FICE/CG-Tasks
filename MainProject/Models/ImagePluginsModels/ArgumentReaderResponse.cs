namespace MainProject.Models.ImagePluginsModels
{
    public class ArgumentReaderResponse
    {
        public FileInfo Image { get; set; }
        public string OutputDirectoryPath { get; set; }
        public string GoalFormat { get; set; }
    }
}
