using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using NUnit.Framework;

namespace ThirdTryPlusReportSecondAttempt
{
    internal class TestBase
    {
        protected IWebDriver driver { get; private set; }
        protected WebFormPage WebForm { get; private set; }

        [SetUp]
        public void Setup()
        {
            ExtentReporting.Instance.CreateTest(TestContext.CurrentContext.Test.MethodName);

            //this can be updated to work for multiple browsers (e.g. based on a value set in JSON config)
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            //this can be moved to JSON config
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void TearDown()
        {
            EndTest();
            ExtentReporting.Instance.EndReporting();

            driver.Quit();
        }

        private void EndTest()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            switch (testStatus)
            {
                case TestStatus.Failed:
                    ExtentReporting.Instance.LogFail($"Test has failed {message}");
                    break;
                case TestStatus.Skipped:
                    ExtentReporting.Instance.LogInfo($"Test skipped {message}");
                    break;
                default:
                    break;
            }

            ExtentReporting.Instance.LogScreenshot("Ending test", Browser.GetScreenshot());
        }
    }
}
