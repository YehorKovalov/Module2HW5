using Logger.Services.Abstractions;
using Newtonsoft.Json;

namespace Logger.Configurations
{
    public class Configuration : IConfiguration
    {
        private const string ConfigPath = "config.json";
        private readonly IFileService _fileService;
        public Configuration(IFileService fileService)
        {
            _fileService = fileService;
        }

        public Config GetConfig()
        {
            var configFile = _fileService.ReadFile(ConfigPath);
            return JsonConvert.DeserializeObject<Config>(configFile);
        }
    }
}
