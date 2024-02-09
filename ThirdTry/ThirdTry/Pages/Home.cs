using BasePageObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ThirdTry
{
    public class Home
    {
        //Locators
        [FindsBy(How = How.CssSelector, Using = "input[name='search']")]
        private IWebElement SearchInput;

        private By SearchButton => By.CssSelector("form#searchform > button");


        //Actions
        public void isAt() => Assert.IsTrue(WebDriverFacade.Title.Equals("Wikipedia, the free encyclopedia"));

        public void PerformSearch(string searchText)
        {
            WebDriverFacade.WaitUntilElementExists(SearchButton);
            SearchInput.SendKeys(searchText + Keys.Enter);
        }
    }
}
