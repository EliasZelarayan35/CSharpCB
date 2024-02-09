using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ThirdTryPlusReportSecondAttempt
{
    public class Home
    {
        //Locators
        [FindsBy(How = How.CssSelector, Using = "input[name='search']")]
        private IWebElement SearchInput;

        private By SearchButton => By.CssSelector("form#searchform > button");


        //Actions
        public void isAt() => Assert.IsTrue(Browser.Title.Equals("Wikipedia, the free encyclopedia"));

        public void PerformSearch(string searchText)
        {
            Browser.WaitUntilElementExists(SearchButton);
            SearchInput.SendKeys(searchText + Keys.Enter);
        }
    }
}
