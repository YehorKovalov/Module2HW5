using System;
using Logger.Configurations;
using Logger.Helpers;
using Logger.Models;
using Logger.Models.Abstractions;

namespace Logger
{
    public class Application
    {
        private readonly ILogger _logger;
        private readonly IActions _actions;

        public Application(ILogger loggerBase, IActions actions)
        {
            _logger = loggerBase;
            _actions = actions;
        }

        public void Run()
        {
            _logger.InitNewStreamWriter();
            const int testLogsAmount = 100;
            const int logTypesAmount = 3;
            for (var i = 0; i < testLogsAmount; i++)
            {
                try
                {
                    ChoseRandomLog(logTypesAmount);
                }
                catch (BusinessException ex)
                {
                    _logger.LogWarning($"Action got this custom Exception: {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Action failed by reason: {ex}");
                }
            }
        }

        private void ChoseRandomLog(int logTypesAmount)
        {
            var logType = (LogType)new Random().Next(logTypesAmount);
            switch (logType)
            {
                case LogType.Error:
                    _actions.BrokenMethod();
                    break;
                case LogType.Warning:
                    _actions.WarnedMethod();
                    break;
                case LogType.Info:
                    _actions.SucceedMethod();
                    break;
            }
        }
    }
}
