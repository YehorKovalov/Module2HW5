using System;
using Logger.Configurations;

namespace Logger.Services.Abstractions
{
    public interface IFileService
    {
        IDisposable CreateStreamWriter(string filePath);
        void WriteLineMessage(IDisposable streamWritter, string message);
        void CloseStream(IDisposable streamWriter);
        string ReadFile(string filePath);
        void ConfigureLoggerDirectory(IConfiguration configuration);
    }
}