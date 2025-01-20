namespace OnlineQuizSystemApi.Logging
{
    public class LoggerFactory
    {
        public static LoggerBase CreateLogger(string loggerType, bool withTimestamp = false, string filePath = null)
        {
            LoggerBase logger = loggerType switch
            {
                "Console" => ConsoleLogger.Instance,
                "Database" => DatabaseLogger.Instance,
                "File" => FileLogger.Instance(filePath ?? "default.log"),
                _ => throw new ArgumentException("Invalid logger type")
            };

            if (withTimestamp)
            {
                logger = new TimestampLogger(logger);
            }

            return logger;
        }
    }
}
