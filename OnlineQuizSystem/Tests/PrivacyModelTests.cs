using NUnit.Framework;
using QuizWebbApp.Pages;
using Microsoft.Extensions.Logging;
using Moq;

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class PrivacyModelTests
    {
        [Test]
        public void OnGet_ShouldDoNothing()
        {
            var mockLogger = new Mock<ILogger<PrivacyModel>>();
            var pageModel = new PrivacyModel(mockLogger.Object);

            pageModel.OnGet();

            Assert.Pass("No exception, method called successfully.");
        }
    }
}
