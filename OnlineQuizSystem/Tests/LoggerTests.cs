using OnlineQuizSystemApi.Logging;

namespace Tests
{
    public class LoggerTests
    {
        [Fact]
        public void ConsoleLogger_ShouldLogToConsole()
        {
            // Arrange
            var consoleLogger = ConsoleLogger.Instance;

            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var message = "Test Console Log";

            // Act
            consoleLogger.Log(message);

            // Assert
            Assert.Contains($"[Console Logger]: {message}", stringWriter.ToString());
        }

        [Fact]
        public void FileLogger_ShouldLogToFile()
        {
            // Arrange
            var filePath = "testlog.txt";
            var fileLogger = FileLogger.Instance(filePath);
            var message = "Test File Log";

            // Act
            fileLogger.Log(message);

            // Assert
            var fileContent = File.ReadAllText(filePath);
            Assert.Contains(message, fileContent);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void TimestampLogger_ShouldAddTimestampToLog()
        {
            // Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var consoleLogger = ConsoleLogger.Instance;
            var timestampLogger = new TimestampLogger(consoleLogger);

            var message = "Test Timestamp Log";

            // Act
            timestampLogger.Log(message);

            // Assert
            var loggedMessage = stringWriter.ToString();
            Assert.Contains(message, loggedMessage);
            Assert.Matches(@"\[\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2}\]", loggedMessage);
        }
    }
}
