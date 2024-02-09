using CodeBase.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace CodeBase
{
    public class Tests
    {
        IWebDriver driver;
        
        [SetUp]
        public void Setup(string browser = "")
        {
            switch (browser.ToUpper())
            {
                case "CHROME": driver = new ChromeDriver(); break;
                case "FIREFOX": driver = new FirefoxDriver(); break;
                case "EDGE": driver = new EdgeDriver(); break;
                default: driver = new ChromeDriver(); break;
            }
        }

        [Test]
        public void Test1()
        {
            var mainPage = new MainPage(driver);
            mainPage.GoToMainPage();
            mainPage.PerformSearch("elias");
            var resultsPage = new ResultsPage(driver);
            resultsPage.AssertHeaderDisplayed();
        }

        [TearDown]
        public void TearDown() {
            driver.Close();
        }
    }
}