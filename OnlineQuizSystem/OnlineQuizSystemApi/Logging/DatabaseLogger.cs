namespace OnlineQuizSystemApi.Logging
{
    public class DatabaseLogger : LoggerBase
    {
        private static DatabaseLogger _instance;
        private static readonly object _lock = new object();

        private DatabaseLogger() { }

        public static DatabaseLogger Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseLogger();
                    }
                    return _instance;
                }
            }
        }

        public override void Log(string message)
        {
            Console.WriteLine($"[Database Logger]: {message}");
        }
    }

}
