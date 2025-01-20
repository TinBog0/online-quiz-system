using NUnit.Framework;
using QuizWebbApp.Pages;

namespace QuizWebbApp.Tests
{
    [TestFixture]
    public class LoginModelTests
    {
        [Test]
        public void OnGet_ShouldDoNothing()
        {
            
            var pageModel = new LoginModel();

            pageModel.OnGet();

            Assert.Pass("No exception, method called successfully.");
        }
    }
}


