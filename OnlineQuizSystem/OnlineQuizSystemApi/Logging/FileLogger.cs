namespace OnlineQuizSystemApi.Logging
{
    using System.IO;

    public class FileLogger : LoggerBase
    {
        private static FileLogger _instance;
        private static readonly object _lock = new object();
        private readonly string _filePath;

        private FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public static FileLogger Instance(string filePath)
        {
            lock (_lock)
            {
                if (_instance == null || _instance._filePath != filePath)
                {
                    _instance = new FileLogger(filePath);
                }
                return _instance;
            }
        }

        public override void Log(string message)
        {
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }
    }
}
