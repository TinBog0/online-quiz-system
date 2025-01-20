using NUnit.Framework;
using Moq;
using QuizWebbApp.Services;
using QuizWebbApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class ProxyQuizServiceTests
    {
        private Mock<IQuizService> _mockRealService;
        private ProxyQuizService _proxy;

        [SetUp]
        public void Setup()
        {
            _mockRealService = new Mock<IQuizService>();
            _proxy = new ProxyQuizService(_mockRealService.Object);
        }

        [Test]
        public async Task GetAllQuizzesAsync_ShouldCallRealService()
        {
            var fakeQuizzes = new List<QuizDto> { new QuizDto { Title = "ProxyTestQuiz" } };
            _mockRealService
                .Setup(s => s.GetAllQuizzesAsync())
                .ReturnsAsync(fakeQuizzes);

            var result = await _proxy.GetAllQuizzesAsync();

            Assert.AreEqual(fakeQuizzes, result, "Proxy should return the same data from real service");
            _mockRealService.Verify(s => s.GetAllQuizzesAsync(), Times.Once);
        }

        [Test]
        public async Task GetQuizByIdAsync_ShouldCallRealService()
        {
            var fakeQuiz = new QuizDto { Title = "Quiz from RealService" };
            _mockRealService
                .Setup(s => s.GetQuizByIdAsync(123))
                .ReturnsAsync(fakeQuiz);

            var result = await _proxy.GetQuizByIdAsync(123);

            Assert.AreEqual(fakeQuiz, result);
            _mockRealService.Verify(s => s.GetQuizByIdAsync(123), Times.Once);
        }

        [Test]
        public async Task SubmitAnswerAsync_ShouldCallRealServiceAndReturnItsResult()
        {
            _mockRealService
                .Setup(s => s.SubmitAnswerAsync(1, 2, "Answer"))
                .ReturnsAsync(true);

            var result = await _proxy.SubmitAnswerAsync(1, 2, "Answer");

            Assert.IsTrue(result);
            _mockRealService.Verify(s => s.SubmitAnswerAsync(1, 2, "Answer"), Times.Once);
        }
    }
}
