using System;
using Logger.Configurations;
using Logger.Models.Abstractions;
using Logger.Services.Abstractions;

namespace Logger.Models
{
    public class Logger : ILogger
    {
        private readonly IFileService _fileService;
        private readonly LoggerConfig _loggerConfig;
        private readonly IConfiguration _configuration;
        private IDisposable _streamWriter;

        public Logger(
            IFileService fileService,
            IConfiguration configuration)
        {
            _fileService = fileService;
            _configuration = configuration;
            _loggerConfig = _configuration.GetConfig().LoggerConfig;
            _fileService.ConfigureLoggerDirectory(_configuration);
        }

        ~Logger()
        {
            _fileService.CloseStream(_streamWriter);
        }

        public void LogInfo(string message) => Log(LogType.Info, message);
        public void LogWarning(string message) => Log(LogType.Warning, message);
        public void LogError(string message) => Log(LogType.Error, message);
        public void InitNewStreamWriter()
        {
            var filePath = GetFilePath();
            _streamWriter = _fileService.CreateStreamWriter(filePath);
        }

        private void Log(LogType logType, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var report = FormReport(logType, message);
                _fileService.WriteLineMessage(_streamWriter, message);
                Console.Write(report);
            }
        }

        private string FormReport(LogType logType, string message)
        {
            var utcNow = DateTime.UtcNow
                .ToString(_loggerConfig.TimeFormat);
            var report = $"{utcNow}: {logType}: {message}";
            return report;
        }

        private string GetFilePath()
        {
            var fileTitle = DateTime.UtcNow.ToString(_loggerConfig.FileNameFormat);
            var extension = _loggerConfig.FileExtension;
            var dirPath = _loggerConfig.DirectoryPath;
            return $"{dirPath}/{fileTitle}{extension}";
        }
    }
}
