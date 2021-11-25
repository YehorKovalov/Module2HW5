namespace Logger.Models.Abstractions
{
    public interface ILogger
    {
        void InitNewStreamWriter();
        void LogError(string message);
        void LogInfo(string message);
        void LogWarning(string message);
    }
}