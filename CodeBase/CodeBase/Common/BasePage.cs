using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace CodeBase.Common
{
    public class BasePage
    {
        public IWebDriver _driverInstance;

        public void ClickOnElement(By locator)
        {
            if (isElementPresent(locator))
                _driverInstance.FindElement(locator).Click();
        }

        public void SendKeys(string text, By locator)
        {
            if (isElementPresent(locator))
            {
                var element = _driverInstance.FindElement(locator);
                if (!element.Displayed)
                    MoveToElement(element);
                element.SendKeys(text);
            }

        }

        public void SendKeys(string text, IWebElement element)
        {
            if (!element.Displayed)
                MoveToElement(element);
            element.SendKeys(text);
        }

        #region Cursor Movement section
        private void MoveToElement(By locator)
        {
            var element = _driverInstance.FindElement(locator);
            Actions actions = new Actions(_driverInstance);
            actions.MoveToElement(element);
            actions.Perform();
        }

        private void MoveToElement(IWebElement element)
        {
            Actions actions = new Actions(_driverInstance);
            actions.MoveToElement(element);
            actions.Perform();
        }

        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            IJavaScriptExecutor je = (IJavaScriptExecutor)_driverInstance;
            je.ExecuteScript(js);
        }

        public IWebElement ScrollToView(By selector)
        {
            var element = _driverInstance.FindElement(selector);
            ScrollToView(element);
            return element;
        }

        public void ScrollToView(IWebElement element)
        {
            if (element.Location.Y > 200)
            {
                ScrollTo(0, element.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
            }

        }
        #endregion

        #region Questions section
        protected bool isElementPresent(By by)
        {
            try
            {
                _driverInstance.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public bool isElementVisible(By by)
        {
            bool isVisible = false;
            try
            {
                var element = _driverInstance.FindElement(by);
                if (element.Displayed && element.Enabled)
                {
                    isVisible = true;
                }
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return isVisible;
        }
        #endregion

        #region Wait section
        //this will search for the element until a timeout is reached
        public static IWebElement WaitUntilElementExists(By elementLocator, int timeout = 10)
        {
            try
            {
                //var wait = new WebDriverWait(_driverInstance, TimeSpan.FromSeconds(timeout));
                //return wait.Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        //this will search for the element until a timeout is reached
        public static IWebElement WaitUntilElementVisible(By elementLocator, int timeout = 10)
        {
            try
            {
                //var wait = new WebDriverWait(_driverInstance, TimeSpan.FromSeconds(timeout));
                //return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
                throw;
            }
        }

        //this will search for the element until a timeout is reached
        public static IWebElement WaitUntilElementClickable(By elementLocator, int timeout = 10)
        {
            try
            {
                //var wait = new WebDriverWait(_driverInstance, TimeSpan.FromSeconds(timeout));
                //return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }
        #endregion
    }
}
