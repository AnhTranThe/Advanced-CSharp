using Advanced_CSharp.Service.Interfaces;
using log4net;

namespace Advanced_CSharp.Service.Services
{
    public class LoggingService : IloggingService
    {
        private readonly ILog _logger;
        public LoggingService()
        {
            _logger = LogManager.GetLogger(typeof(LoggingService));
        }
        public void LogError(Exception exception)
        {
            _logger.Error(exception);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
