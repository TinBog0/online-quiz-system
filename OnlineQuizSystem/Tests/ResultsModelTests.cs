using NUnit.Framework;
using QuizWebbApp.Pages;

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class ResultsModelTests
    {
        [Test]
        public void OnGet_ShouldDoNothing()
        {
            var pageModel = new ResultsModel();

            pageModel.OnGet();

            Assert.Pass("No exception, method called successfully.");
        }
    }
}
