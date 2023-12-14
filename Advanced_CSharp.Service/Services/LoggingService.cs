using Advanced_CSharp.Service.Interfaces;
using log4net;

namespace Advanced_CSharp.Service.Services
{
    public class LoggingService : IloggingService
    {
        private readonly ILog _logger;
        /// <summary>
        /// LoggingService
        /// </summary>
        public LoggingService()
        {
            _logger = LogManager.GetLogger(typeof(LoggingService));
        }
        /// <summary>
        /// LogError
        /// </summary>
        /// <param name="exception"></param>
        public void LogError(Exception exception)
        {
            _logger.Error(exception);
        }
        /// <summary>
        /// LogInfo
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
