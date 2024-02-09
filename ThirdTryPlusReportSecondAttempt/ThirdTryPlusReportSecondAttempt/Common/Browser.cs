using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Reflection;

namespace ThirdTryPlusReportSecondAttempt
{
    public static class Browser
    {
        private static IWebDriver driver;
        private static string baseURL = "https://en.wikipedia.org/";
        private static string browser = "Chrome";
        //public Browser(IWebDriver driver)
        //{
        //    this.driver = driver;
        //}

        /// <summary>
        /// Capture a screenshot using Selenium IWebDriver
        /// </summary>
        /// <returns></returns>
        public static string GetScreenshot()
        {
            var file = ((ITakesScreenshot)driver).GetScreenshot();
            var img = file.AsBase64EncodedString;

            return img;
        }

        public static string SaveScreenshot()
        {
            var fileName = Guid.NewGuid().ToString();
            var directory = Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                 + "\\screenshots\\").FullName;
            string filePath = directory + fileName;

            var file = ((ITakesScreenshot)driver).GetScreenshot();
            file.SaveAsFile(filePath, ScreenshotImageFormat.Png);

            return filePath;
        }

        public static void Init()
        {
            switch (browser.ToUpper())
            {
                case "CHROME":
                    driver = new ChromeDriver();
                    break;
                case "FIREFOX":
                    driver = new FirefoxDriver();
                    break;
                case "EDGE":
                    driver = new EdgeDriver();
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }
            driver.Manage().Window.Maximize();
            Goto(baseURL);
        }

        public static string Title
        {
            get { return driver.Title; }
        }

        public static IWebDriver getDriver
        {
            get { return driver; }
        }

        public static void Goto(string url)
        {
            driver.Url = url;
        }

        public static void Close()
        {
            driver.Quit();
        }

        public static IWebElement WaitUntilElementExists(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        //extensions
        public static bool ControlExists(this IWebDriver driver, By by)
        {
            return driver.FindElements(by).Count == 0 ? false : true;
        }

        public static bool ControlDisplayed(this IWebElement element, bool displayed = true, uint timeoutInSeconds = 60)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.IgnoreExceptionTypes(typeof(Exception));
            return wait.Until(drv =>
            {
                if (!displayed && !element.Displayed || displayed && element.Displayed)
                {
                    return true;
                }
                return false;
            });
        }

        public static IWebElement ElementExists(this By Locator, uint timeoutInSeconds = 60)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(ExpectedConditions.ElementExists(Locator));
            }
            catch
            {
                return null;
            }
        }

        public static bool ElementlIsClickable(this IWebElement element, uint timeoutInSeconds = 60, bool displayed = true)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv =>
                {
                    if (ExpectedConditions.ElementToBeClickable(element) != null)
                        return true;
                    return false;
                });
            }
            catch
            {
                return false;
            }
        }

        public static void ClickWrapper(this IWebElement element, string elementName)
        {
            if (element.ElementlIsClickable())
            {
                element.Click();
            }
            else
            {
                throw new Exception(string.Format("[{0}] - Element [{1}] is not displayed", DateTime.Now.ToString("HH:mm:ss.fff"), elementName));
            }
        }

        public static void SendKeysWrapper(this IWebElement element, string value, string elementName)
        {
            Console.WriteLine(string.Format("[{0}] - SendKeys value [{1}] to  element [{2}]", DateTime.Now.ToString("HH:mm:ss.fff"), value, elementName));
            element.SendKeys(value);
        }

        public static void DoubleClickActionWrapper(this IWebElement element, string elementName)
        {
            Actions ClickButton = new Actions(driver);
            ClickButton.MoveToElement(element).DoubleClick().Build().Perform();
            Console.WriteLine("[{0}] - Double Click on element [{1}]", DateTime.Now.ToString("HH:mm:ss.fff"), elementName);
        }

        public static void ClearWrapper(this IWebElement element, string elementName)
        {
            Console.WriteLine("[{0}] - Clear element [{1}] content", DateTime.Now.ToString("HH:mm:ss.fff"), elementName);
            element.Clear();
            Assert.AreEqual(element.Text, string.Empty, "Element is not cleared");
        }

        public static void CheckboxWrapper(this IWebElement element, bool value, string elementName)
        {
            Console.WriteLine("[{0}] - Set value of checkbox [{1}] to [{2}]", DateTime.Now.ToString("HH:mm:ss.fff"), elementName, value.ToString());
            if ((!element.Selected && value == true) || (element.Selected && value == false))
            {
                element.Click();
            }
        }
    }
}
