using NUnit.Framework;
using Moq;
using QuizWebbApp.Services; // IQuizService

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class SubmitAnswerCommandTests
    {
        [Test]
        public void Execute_ShouldCallService_WithCorrectParameters()
        {
            var quizServiceMock = new Mock<IQuizService>();
            quizServiceMock
                .Setup(s => s.SubmitAnswerAsync(2, 1, "MyAnswer"))
                .ReturnsAsync(true);

            var command = new SubmitAnswerCommand(
                quizId: 2,
                questionIndex: 1,
                userAnswer: "MyAnswer",
                quizServiceMock.Object
            );

            command.Execute();

            quizServiceMock.Verify(s =>
                s.SubmitAnswerAsync(2, 1, "MyAnswer"), Times.Once);
        }
    }
}
