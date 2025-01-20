namespace OnlineQuizSystemApi.Logging
{
    public class ConsoleLogger : LoggerBase
    {
        private static ConsoleLogger _instance;
        private static readonly object _lock = new object();

        private ConsoleLogger() { }

        public static ConsoleLogger Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConsoleLogger();
                    }
                    return _instance;
                }
            }
        }

        public override void Log(string message)
        {
            Console.WriteLine($"[Console Logger]: {message}");
        }
    }


}
