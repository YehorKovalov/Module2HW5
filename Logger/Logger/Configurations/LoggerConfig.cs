namespace Logger.Configurations
{
    public class LoggerConfig
    {
        public string FileNameFormat { get; init; }
        public string DirectoryPath { get; init; }
        public string FileExtension { get; init; }
        public int FilesAmountLimit { get; init; }
        public string TimeFormat { get; init; }
    }
}