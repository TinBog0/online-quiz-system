using NUnit.Framework;
using Moq;
using QuizWebbApp.Pages;
using QuizWebbApp.Services;
using QuizWebbApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class IndexPageTests
    {
        [Test]
        public async Task OnGetAsync_ShouldFetchAllQuizzesAndSetProperty()
        {
            var mockService = new Mock<IQuizService>();
            mockService
                .Setup(s => s.GetAllQuizzesAsync())
                .ReturnsAsync(new List<QuizDto>
                {
                    new QuizDto { Title = "Quiz 1" },
                    new QuizDto { Title = "Quiz 2" }
                });

            var pageModel = new IndexPage(mockService.Object);

            await pageModel.OnGetAsync();

            Assert.IsNotNull(pageModel.Quizzes, "Quizzes should not be null after OnGetAsync");
            Assert.AreEqual(2, ((List<QuizDto>)pageModel.Quizzes).Count, "Should have two quizzes from service");
            mockService.Verify(s => s.GetAllQuizzesAsync(), Times.Once, "Service should be called exactly once");
        }
    }
}
