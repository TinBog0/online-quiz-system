using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineQuizSystemApi.Controllers;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Interfaces.Quizes;
using OnlineQuizSystemApi.Models;

namespace Tests
{
    public class QuizControllerTests
    {
        private readonly Mock<IQuizService> _quizServiceMock;
        private readonly QuizsController _controller;

        public QuizControllerTests()
        {
            _quizServiceMock = new Mock<IQuizService>();
            _controller = new QuizsController(_quizServiceMock.Object);
        }

        [Fact]
        public async Task AddQuiz_ShouldReturnOk_WhenQuizIsAdded()
        {
            // Arrange
            var quizDto = new QuizDto { Title = "New Quiz", Description = "New Quiz Description" };
            var quiz = new Quiz { Id = 1, Title = quizDto.Title, Description = quizDto.Description };

            _quizServiceMock.Setup(service => service.AddQuizAsync(It.IsAny<QuizDto>()))
                            .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddQuiz(quizDto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Quiz>>(result);
            var okResult = Assert.IsType<OkResult>(actionResult.Result);
            _quizServiceMock.Verify(service => service.AddQuizAsync(quizDto), Times.Once);
        }


        [Fact]
        public void GradeQuizAttempt_ShouldReturnBadRequest_WhenGradingMethodIsInvalid()
        {
            // Arrange
            int correctAnswers = 8;
            int totalQuestions = 10;
            string gradingMethod = "invalid";

            // Act
            var result = _controller.GradeQuizAttempt(correctAnswers, totalQuestions, gradingMethod);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid grading method.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAllQuizzes_ShouldReturnOk_WithQuizzes()
        {
            // Arrange
            var quizzes = new List<QuizDto>
        {
            new QuizDto { Id = 1, Title = "Quiz 1", Description = "Test Quiz 1" },
            new QuizDto { Id = 2, Title = "Quiz 2", Description = "Test Quiz 2" }
        };
            _quizServiceMock.Setup(service => service.GetAllQuizzesAsync()).ReturnsAsync(quizzes);

            // Act
            var result = await _controller.GetAllQuizzes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedQuizzes = Assert.IsType<List<QuizDto>>(okResult.Value);
            Assert.Equal(2, returnedQuizzes.Count);
        }

        [Fact]
        public async Task GetQuizById_ShouldReturnOk_WithQuiz()
        {
            // Arrange
            int quizId = 1;
            var quiz = new QuizDto { Id = quizId, Title = "Test Quiz", Description = "A sample quiz" };
            _quizServiceMock.Setup(service => service.GetQuizByIdAsync(quizId)).ReturnsAsync(quiz);

            // Act
            var result = await _controller.GetQuizById(quizId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedQuiz = Assert.IsType<QuizDto>(okResult.Value);
            Assert.Equal(quizId, returnedQuiz.Id);
        }

        [Fact]
        public async Task GetQuizById_ShouldReturnNotFound_WhenQuizDoesNotExist()
        {
            // Arrange
            int quizId = 999;
            _quizServiceMock.Setup(service => service.GetQuizByIdAsync(quizId))
                            .ThrowsAsync(new KeyNotFoundException("Quiz not found."));

            // Act
            var result = await _controller.GetQuizById(quizId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Quiz not found.", notFoundResult.Value);
        }
    }
}