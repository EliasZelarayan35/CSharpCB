using NUnit.Framework;
using ThirdTry;

namespace Tests
{
    [TestFixture]
    public class WikiTest
    {
        [SetUp]
        public void StartUpTest()
        {
            WebDriverFacade.Init();
        }
        [TearDown]
        public void EndTest()
        {
            WebDriverFacade.Close();
        }
        [Test]
        public void HelloWorldTest()
        {
            Pages.home.isAt();
            Pages.home.PerformSearch("elias");
            Pages.resultPage.AssertHeaderDisplayed();
        }
    }
}
