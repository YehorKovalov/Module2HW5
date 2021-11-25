using System;
using System.IO;
using Logger.Configurations;
using Logger.Helpers;
using Logger.Models.Abstractions;
using Logger.Services;
using Logger.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Logger
{
    public class Startup
    {
        public void Run()
        {
            var serviceProvider = ConfigureServices();
            var app = serviceProvider?.GetService<Application>();
            app?.Run();
        }

        public ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration, Configuration>();
            services.AddSingleton<ILogger, Models.Logger>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IActions, Actions>();
            services.AddTransient<Application>();

            return services.BuildServiceProvider();
        }
    }
}
