using NUnit.Framework;
using Moq;
using QuizWebbApp.Services;    
using QuizWebbApp.Interface;   
using QuizWebbApp.DTOs;        
using System.Collections.Generic;

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class QuizFacadeTests
    {
        [Test]
        public void GetQuizForDisplay_QuizExists_ReturnsDisplayModel()
        {
            var quizServiceMock = new Mock<IQuizService>();
            quizServiceMock.Setup(s => s.GetQuizByIdAsync(5))
                .ReturnsAsync(new QuizDto
                {
                    Title = "Facade Quiz",
                    Questions = new List<QuestionDto>
                    {
                        new QuestionDto { QuestionId = 1, QuestionText = "Q1" }
                    }
                });

            var facade = new QuizFacade(quizServiceMock.Object);

            var displayModel = facade.GetQuizForDisplay(5);

            Assert.That(displayModel, Is.Not.Null);
            Assert.That(displayModel.QuizId, Is.EqualTo(5));
            Assert.That(displayModel.QuizTitle, Is.EqualTo("Facade Quiz"));
            Assert.That(displayModel.Questions.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetQuizForDisplay_QuizNotFound_ReturnsNull()
        {
            var quizServiceMock = new Mock<IQuizService>();
            quizServiceMock.Setup(s => s.GetQuizByIdAsync(999))
                           .ReturnsAsync((QuizDto)null);

            var facade = new QuizFacade(quizServiceMock.Object);

            var result = facade.GetQuizForDisplay(999);

            Assert.That(result, Is.Null);
        }
    }
}
