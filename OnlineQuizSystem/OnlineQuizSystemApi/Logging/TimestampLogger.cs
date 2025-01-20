namespace OnlineQuizSystemApi.Logging
{
    public class TimestampLogger : LoggerBase
    {
        private readonly LoggerBase _innerLogger;

        public TimestampLogger(LoggerBase logger)
        {
            _innerLogger = logger;
        }

        public override void Log(string message)
        {
            var timestampedMessage = $"[{DateTime.Now}] {message}";
            _innerLogger.Log(timestampedMessage);
        }
    }

}
