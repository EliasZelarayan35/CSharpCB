using OpenQA.Selenium;
using CodeBase.Common;

namespace CodeBase.PageObjects
{
    public partial class MainPage : BasePage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"en.wikipedia.org";
        public MainPage(IWebDriver browser) => _driver = browser;

        public void GoToMainPage()
        {
            _driver.Navigate().GoToUrl(_url);
            //_driver.WaitForBrowserLoad();
        }

        public void PerformSearch(string searchValue)
        {
            if (searchValue.Length >= 3)
            {
                SendKeys(searchValue, _mainSearchTxt);
                _driver.FindElement(_searchBtn).Click();
                //_driver.WaitForBrowserLoad();
            }
        }
    }
}
