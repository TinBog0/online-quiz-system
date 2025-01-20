using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using QuizWebbApp.Pages;
using QuizWebbApp.Interface;
using QuizWebbApp.Services;
using QuizWebbApp.DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class QuizRoomTests
    {
        private Mock<IQuizFacade> _mockFacade;
        private Mock<IQuizService> _mockService;
        private QuizRoom _pageModel;

        [SetUp]
        public void Setup()
        {
            _mockFacade = new Mock<IQuizFacade>();
            _mockService = new Mock<IQuizService>();

            _pageModel = new QuizRoom(_mockFacade.Object, _mockService.Object)
            {
                PageContext = new PageContext
                {
                    HttpContext = new DefaultHttpContext()
                },
                TempData = new TempDataDictionary(
                    new DefaultHttpContext(),
                    Mock.Of<ITempDataProvider>()
                )
            };
        }


        [Test]
        public void OnGet_InvalidQuizId_RedirectsToIndex()
        {
            _pageModel.QuizId = 0;

            var result = _pageModel.OnGet();

            Assert.IsInstanceOf<RedirectToPageResult>(result);
            var redirect = (RedirectToPageResult)result;
            Assert.AreEqual("/Index", redirect.PageName);
        }

        [Test]
        public void OnGet_QuizNotFound_RedirectsToIndex()
        {
            
            _pageModel.QuizId = 999;
            _mockFacade
                .Setup(f => f.GetQuizForDisplay(999))
                .Returns((QuizDisplayModel)null); 

            var result = _pageModel.OnGet();

            Assert.IsInstanceOf<RedirectToPageResult>(result);
            var redirect = (RedirectToPageResult)result;
            Assert.AreEqual("/Index", redirect.PageName);
        }

        [Test]
        public void OnGet_ValidQuiz_ReturnsPage()
        {
            _pageModel.QuizId = 10;
            _mockFacade
                .Setup(f => f.GetQuizForDisplay(10))
                .Returns(new QuizDisplayModel
                {
                    QuizId = 10,
                    QuizTitle = "Some Quiz",
                    Questions = new List<QuestionDto>()
                });

            var result = _pageModel.OnGet();

            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsNotNull(_pageModel.QuizDisplay);
            Assert.AreEqual(10, _pageModel.QuizDisplay.QuizId);
        }


        [Test]
        public void OnPost_QuizIdLessThanOrEqualZero_SetsErrorMessage_ReturnsPage()
        {
            _pageModel.QuizId = 0;
            _pageModel.UserAnswer = "Any answer";

            var result = _pageModel.OnPost();

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Invalid Quiz ID.", _pageModel.ErrorMessage);
        }

        [Test]
        public void OnPost_EmptyUserAnswer_SetsErrorMessage_ReturnsPage()
        {
            _pageModel.QuizId = 5;
            _pageModel.UserAnswer = ""; // empty

            _mockFacade
                .Setup(f => f.GetQuizForDisplay(5))
                .Returns(new QuizDisplayModel
                {
                    QuizId = 5,
                    Questions = new List<QuestionDto> { new QuestionDto() }
                });

            var result = _pageModel.OnPost();

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Answer cannot be empty.", _pageModel.ErrorMessage);
        }

        [Test]
        public void OnPost_QuizDisplayNull_SetsErrorMessage_ReturnsPage()
        {
            _pageModel.QuizId = 5;
            _pageModel.UserAnswer = "Valid answer";
            _mockFacade
                .Setup(f => f.GetQuizForDisplay(5))
                .Returns((QuizDisplayModel)null);

            var result = _pageModel.OnPost();

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Quiz not found.", _pageModel.ErrorMessage);
        }

        [Test]
        public void OnPost_OutOfRangeQuestionIndex_SetsErrorMessage_ReturnsPage()
        {
            _pageModel.QuizId = 5;
            _pageModel.UserAnswer = "Valid answer";
            _pageModel.CurrentQuestionIndex = 10; // out of range

            _mockFacade
                .Setup(f => f.GetQuizForDisplay(5))
                .Returns(new QuizDisplayModel
                {
                    QuizId = 5,
                    Questions = new List<QuestionDto>
                    {
                        new QuestionDto { QuestionId = 123 }
                    }
                });

            var result = _pageModel.OnPost();

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Invalid question index.", _pageModel.ErrorMessage);
        }

        [Test]
        public void OnPost_ValidData_SubmitsAnswer_IncrementIndex_SetsSuccessMessage()
        {
            _pageModel.QuizId = 5;
            _pageModel.UserAnswer = "MyAnswer";
            _pageModel.CurrentQuestionIndex = 0;

            _mockFacade
                .Setup(f => f.GetQuizForDisplay(5))
                .Returns(new QuizDisplayModel
                {
                    QuizId = 5,
                    Questions = new List<QuestionDto>
                    {
                        new QuestionDto { QuestionId = 1 }
                    }
                });

            var result = _pageModel.OnPost();

            Assert.IsInstanceOf<PageResult>(result);
            Assert.IsNull(_pageModel.ErrorMessage);
            Assert.IsNotNull(_pageModel.SuccessMessage);
            Assert.IsTrue(_pageModel.SuccessMessage.Contains("MyAnswer"));
            Assert.AreEqual(1, _pageModel.CurrentQuestionIndex);

        }
        [Test]
        public void OnPageHandlerSelected_ShouldRestoreQuestionIndexFromTempData()
        {
            _pageModel.TempData["CurrentQuestionIndex"] = 2;
            var context = new PageHandlerSelectedContext(
                _pageModel.PageContext,
                new List<IFilterMetadata>(),
                new HandlerMethodDescriptor());

            _pageModel.OnPageHandlerSelected(context);

            Assert.That(_pageModel.CurrentQuestionIndex, Is.EqualTo(2));
        }
        [Test]
        public void OnPost_NullQuizDisplayButIndexIsZero_ShouldFailCondition()
        {
            _pageModel.QuizId = 5;
            _pageModel.UserAnswer = "Something";

            _mockFacade
                .Setup(f => f.GetQuizForDisplay(5))
                .Returns((QuizDisplayModel)null);

            _pageModel.CurrentQuestionIndex = 0;

            var result = _pageModel.OnPost();

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Quiz not found.", _pageModel.ErrorMessage);
        }

        [Test]
        public void OnPost_QuizDisplayIsNotNullBu_ShouldFailTheCondition()
        {
            _pageModel.QuizId = 5;
            _pageModel.UserAnswer = "Answer";
            _pageModel.CurrentQuestionIndex = 1; 

            _mockFacade
                .Setup(f => f.GetQuizForDisplay(5))
                .Returns(new QuizDisplayModel
                {
                    QuizId = 5,
                    Questions = new List<QuestionDto> { new QuestionDto() } // only 1 question
                });

            var result = _pageModel.OnPost();

            Assert.IsInstanceOf<PageResult>(result);
            Assert.AreEqual("Invalid question index.", _pageModel.ErrorMessage);
        }

    }



}
