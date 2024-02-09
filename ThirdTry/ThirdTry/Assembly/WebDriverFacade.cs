using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;

namespace ThirdTry
{
    public static class WebDriverFacade
    {
        private static IWebDriver WebDriver;

        //public static IWebDriver WebDriver
        //{
        //    get
        //    {
        //        if (WebDriver == null)
        //        {
        //            WebDriver = new ChromeDriver();
        //        }
        //        return WebDriver;
        //    }
        //}

        private static string baseURL = "https://en.wikipedia.org/";
        private static string browser = "Chrome";

        public static void Init()
        {
            switch (browser.ToUpper())
            {
                case "CHROME":
                    WebDriver = new ChromeDriver();
                    break;
                case "FIREFOX":
                    WebDriver = new FirefoxDriver();
                    break;
                case "EDGE":
                    WebDriver = new EdgeDriver();
                    break;
                default:
                    WebDriver = new ChromeDriver();
                    break;
            }
            WebDriver.Manage().Window.Maximize();
            Goto(baseURL);
        }

        public static string Title
        {
            get { return WebDriver.Title; }
        }

        public static IWebDriver getDriver
        {
            get { return WebDriver; }
        }

        public static void Goto(string url)
        {
            WebDriver.Url = url;
        }

        public static void Close()
        {
            WebDriver.Quit();
        }

        public static IWebElement WaitUntilElementExists(By elementLocator, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeout));
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
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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
                WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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
            Actions ClickButton = new Actions(WebDriver);
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
