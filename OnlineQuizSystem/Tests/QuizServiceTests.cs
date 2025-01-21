using AutoMapper;
using Moq;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Interfaces.Books;
using OnlineQuizSystemApi.Models;
using OnlineQuizSystemApi.Repositories.Quizes;

namespace Tests
{
    public class QuizServiceTests
    {
        [Fact]
        public async Task AddQuizAsync_ShouldCallRepositoryWithMappedQuiz()
        {
            // Arrange
            var quizDto = new QuizDto { Title = "Test Quiz", Description = "Description" };
            var quiz = new Quiz { Id = 1, Title = "Test Quiz", Description = "Description" };

            var quizRepositoryMock = new Mock<IQuizRepository>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<Quiz>(quizDto)).Returns(quiz);

            var quizService = new QuizService(quizRepositoryMock.Object, mapperMock.Object);

            // Act
            await quizService.AddQuizAsync(quizDto);

            // Assert
            quizRepositoryMock.Verify(repo => repo.AddQuizAsync(quiz), Times.Once);
            mapperMock.Verify(m => m.Map<Quiz>(quizDto), Times.Once);
        }

        [Fact]
        public async Task GetAllQuizzesAsync_ShouldReturnMappedQuizzes()
        {
            // Arrange
            var quizzes = new List<Quiz>
                {
                    new Quiz { Id = 1, Title = "Quiz 1" },
                    new Quiz { Id = 2, Title = "Quiz 2" }
                };
            var quizDtos = new List<QuizDto>
                 {
                     new QuizDto { Title = "Quiz 1" },
                     new QuizDto { Title = "Quiz 2" }
                 };

            var quizRepositoryMock = new Mock<IQuizRepository>();
            var mapperMock = new Mock<IMapper>();

            quizRepositoryMock.Setup(repo => repo.GetAllQuizzesAsync()).ReturnsAsync(quizzes);
            mapperMock.Setup(m => m.Map<IEnumerable<QuizDto>>(quizzes)).Returns(quizDtos);

            var quizService = new QuizService(quizRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await quizService.GetAllQuizzesAsync();

            // Assert
            Assert.Equal(quizDtos, result);
            quizRepositoryMock.Verify(repo => repo.GetAllQuizzesAsync(), Times.Once);
            mapperMock.Verify(m => m.Map<IEnumerable<QuizDto>>(quizzes), Times.Once);
        }

        [Fact]
        public async Task GetQuizByIdAsync_ShouldReturnMappedQuiz_WhenQuizExists()
        {
            // Arrange
            var quiz = new Quiz { Id = 1, Title = "Test Quiz" };
            var quizDto = new QuizDto { Title = "Test Quiz" };

            var quizRepositoryMock = new Mock<IQuizRepository>();
            var mapperMock = new Mock<IMapper>();

            quizRepositoryMock.Setup(repo => repo.GetQuizByIdAsync(1)).ReturnsAsync(quiz);
            mapperMock.Setup(m => m.Map<QuizDto>(quiz)).Returns(quizDto);

            var quizService = new QuizService(quizRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await quizService.GetQuizByIdAsync(1);

            // Assert
            Assert.Equal(quizDto, result);
            quizRepositoryMock.Verify(repo => repo.GetQuizByIdAsync(1), Times.Once);
            mapperMock.Verify(m => m.Map<QuizDto>(quiz), Times.Once);
        }

        [Fact]
        public async Task GetQuizByIdAsync_ShouldThrowException_WhenQuizDoesNotExist()
        {
            // Arrange
            var quizRepositoryMock = new Mock<IQuizRepository>();
            var mapperMock = new Mock<IMapper>();

            quizRepositoryMock.Setup(repo => repo.GetQuizByIdAsync(1)).ReturnsAsync((Quiz)null);

            var quizService = new QuizService(quizRepositoryMock.Object, mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => quizService.GetQuizByIdAsync(1));
            quizRepositoryMock.Verify(repo => repo.GetQuizByIdAsync(1), Times.Once);
            mapperMock.Verify(m => m.Map<QuizDto>(It.IsAny<Quiz>()), Times.Never);
        }
    }
}
