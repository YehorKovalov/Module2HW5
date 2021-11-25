using System;
using System.Collections.Generic;
using System.IO;
using Logger.Configurations;
using Logger.Helpers;
using Logger.Services.Abstractions;

namespace Logger.Services
{
    public class FileService : IFileService
    {
        public void CloseStream(IDisposable streamWriter)
        {
            var writter = streamWriter as StreamWriter;
            writter?.Close();
            writter?.Dispose();
        }

        public IDisposable CreateStreamWriter(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return null;
            }

            var fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                var newFile = fileInfo.Create();
                newFile.Close();
            }

            return new StreamWriter(filePath, true);
        }

        public string ReadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return null;
            }

            return File.ReadAllText(filePath);
        }

        public void WriteLineMessage(IDisposable streamWritter, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            (streamWritter as StreamWriter)?.WriteLine(message);
        }

        public void ConfigureLoggerDirectory(IConfiguration configuration)
        {
            var loggerConfig = configuration.GetConfig().LoggerConfig;
            var dirPath = loggerConfig.DirectoryPath;
            var filesAmountLimit = loggerConfig.FilesAmountLimit;

            var dirInfo = new DirectoryInfo(dirPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            else
            {
                var files = Directory.GetFiles(dirPath);
                if (files.Length > filesAmountLimit)
                {
                    Array.Sort(files, new FilesCreationTimeComparer());
                    var extraFilesAmount = files.Length - filesAmountLimit;
                    FileInfo fileInfo;
                    for (int i = 0; i <= extraFilesAmount; i++)
                    {
                        fileInfo = new FileInfo(files[i]);
                        fileInfo.Delete();
                    }
                }
            }
        }
    }
}
