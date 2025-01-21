using Moq;
using OnlineQuizSystemApi.Grading;

namespace Tests
{
    public class GradingContextTests
    {
        [Fact]
        public void ExecuteGrading_ShouldUseSetStrategyToCalculateGrade()
        {
            // Arrange
            var mockStrategy = new Mock<IGradingStrategy>();
            mockStrategy.Setup(s => s.CalculateGrade(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns("Mock Grade");

            var context = new GradingContext();
            context.SetStrategy(mockStrategy.Object);

            int correctAnswers = 8;
            int totalQuestions = 10;

            // Act
            var result = context.ExecuteGrading(correctAnswers, totalQuestions);

            // Assert
            Assert.Equal("Mock Grade", result);
            mockStrategy.Verify(s => s.CalculateGrade(correctAnswers, totalQuestions), Times.Once);
        }

        [Fact]
        public void ExecuteGrading_ShouldThrowException_WhenStrategyIsNotSet()
        {
            // Arrange
            var context = new GradingContext();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => context.ExecuteGrading(8, 10));
        }
    }

}
