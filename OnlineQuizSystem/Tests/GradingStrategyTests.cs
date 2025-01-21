using OnlineQuizSystemApi.Grading;

namespace Tests
{
    public class GradingStrategyTests
    {
        [Fact]
        public void PercentageGradingStrategy_ShouldReturnCorrectPercentage()
        {
            // Arrange
            var strategy = new PercentageGradingStrategy();
            int correctAnswers = 8;
            int totalQuestions = 10;

            // Act
            var result = strategy.CalculateGrade(correctAnswers, totalQuestions);

            // Assert
            Assert.Equal("Grade: 80.00%", result);
        }

        [Fact]
        public void PassFailGradingStrategy_ShouldReturnPass_ForSufficientScore()
        {
            // Arrange
            var strategy = new PassFailGradingStrategy();
            int correctAnswers = 6;
            int totalQuestions = 10;

            // Act
            var result = strategy.CalculateGrade(correctAnswers, totalQuestions);

            // Assert
            Assert.Equal("Grade: Pass", result);
        }

        [Fact]
        public void PassFailGradingStrategy_ShouldReturnFail_ForInsufficientScore()
        {
            // Arrange
            var strategy = new PassFailGradingStrategy();
            int correctAnswers = 4;
            int totalQuestions = 10;

            // Act
            var result = strategy.CalculateGrade(correctAnswers, totalQuestions);

            // Assert
            Assert.Equal("Grade: Fail", result);
        }
    }
}
