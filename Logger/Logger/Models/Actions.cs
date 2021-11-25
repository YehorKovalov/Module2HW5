using System;
using Logger.Helpers;
using Logger.Models.Abstractions;

namespace Logger
{
    public class Actions : IActions
    {
        private readonly ILogger _loggerBase;
        public Actions(ILogger loggerBase)
        {
            _loggerBase = loggerBase;
        }

        public void SucceedMethod()
        {
            _loggerBase.LogInfo($"Start method:{nameof(SucceedMethod)}");
        }

        public void WarnedMethod()
        {
            throw new BusinessException($"Skipped logic in methods");
        }

        public void BrokenMethod()
        {
            throw new Exception($"I broke a logic");
        }
    }
}
