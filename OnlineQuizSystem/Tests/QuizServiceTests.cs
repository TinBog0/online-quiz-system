using NUnit.Framework;
using QuizWebbApp.Services;
using Microsoft.Extensions.Options;
using QuizWebbApp.Config;
using QuizWebbApp.DTOs;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class QuizServiceTests
    {
        private QuizService _service;
        private MockHttpMessageHandler _mockHttp;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _mockHttp = new MockHttpMessageHandler();
            _httpClient = new HttpClient(_mockHttp);

            var apiSettings = Options.Create(new ApiSettings
            {
                BaseUrl = "http://example.com"
            });
            _service = new QuizService(_httpClient, apiSettings);
        }
        [Test]
        public void GetQuizByIdAsync_ShouldThrow_OnHttpClientException()
        {
            _mockHttp.EnqueueResponse("Simulated network fail", HttpStatusCode.InternalServerError);
            Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await _service.GetQuizByIdAsync(123);
            });
        }
        [Test]
        public void GetAllQuizzesAsync_WhenHttpClientThrowsException_ShouldThrowHttpRequestException()
        {
            _mockHttp.EnqueueResponse("Simulated network fail", HttpStatusCode.InternalServerError);

            Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await _service.GetAllQuizzesAsync();
            });
        }

        
        [TearDown]
    public void TearDown()
    {
        _httpClient?.Dispose();
        _mockHttp?.Dispose();
    }

        [Test]
        public async Task GetAllQuizzesAsync_200OK_ReturnsList()
        {
            var fakeQuizzes = new List<QuizDto>
            {
                new QuizDto { Title = "Quiz A" },
                new QuizDto { Title = "Quiz B" }
            };
            var jsonResponse = JsonSerializer.Serialize(fakeQuizzes);
            _mockHttp.EnqueueResponse(jsonResponse, HttpStatusCode.OK);

            var result = await _service.GetAllQuizzesAsync();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Quiz A", result.First().Title);
        }

        [Test]
        public void GetAllQuizzesAsync_NonSuccess_ThrowsException()
        {
            _mockHttp.EnqueueResponse("Server error", HttpStatusCode.InternalServerError);

            Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await _service.GetAllQuizzesAsync();
            });
        }


        [Test]
        public async Task GetQuizByIdAsync_200OK_ReturnsQuiz()
        {
            var fakeQuiz = new QuizDto { Title = "SingleQuiz" };
            var jsonResponse = JsonSerializer.Serialize(fakeQuiz);
            _mockHttp.EnqueueResponse(jsonResponse, HttpStatusCode.OK);

            var result = await _service.GetQuizByIdAsync(123);

            Assert.IsNotNull(result);
            Assert.AreEqual("SingleQuiz", result.Title);
        }

        [Test]
        public async Task GetQuizByIdAsync_NonSuccess_ReturnsNull()
        {
            _mockHttp.EnqueueResponse("Not found", HttpStatusCode.NotFound);

            var result = await _service.GetQuizByIdAsync(999);

            Assert.IsNull(result, "Should return null if status code is not success");
        }


        [Test]
        public async Task SubmitAnswerAsync_200OK_ReturnsTrue()
        {
            _mockHttp.EnqueueResponse("", HttpStatusCode.OK);

            var result = await _service.SubmitAnswerAsync(1, 2, "MyAnswer");

            Assert.IsTrue(result, "200 OK means success => true");
        }
        [Test]
        public void SubmitAnswerAsync_WhenHttpClientThrowsException_ShouldBubbleUpException()
        {
            _mockHttp.EnqueueResponse("Network error", HttpStatusCode.InternalServerError);

            Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await _service.SubmitAnswerAsync(1, 2, "SomeAnswer");
            });
        }

        [Test]
        public async Task SubmitAnswerAsync_NonSuccess_ReturnsFalse()
        {
            
            _mockHttp.EnqueueResponse("Bad Request", HttpStatusCode.BadRequest);

            var result = await _service.SubmitAnswerAsync(1, 2, "MyAnswer");

            Assert.IsFalse(result, "Non-success status => false");
        }
    }

    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private Exception _throwOnSend;
        private readonly Queue<HttpResponseMessage> _responses = new Queue<HttpResponseMessage>();

        public void ThrowsOnSend(Exception ex)
        {
            _throwOnSend = ex;
        }

        public void EnqueueResponse(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(content)
            };
            _responses.Enqueue(response);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_throwOnSend != null)
            {
                throw _throwOnSend;
            }

            if (_responses.Count == 0)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("[]")
                });
            }

            return Task.FromResult(_responses.Dequeue());
        }
    }


}
