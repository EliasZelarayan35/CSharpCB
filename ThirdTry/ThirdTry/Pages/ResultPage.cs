using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ThirdTry
{
    public class ResultPage
    {
        [FindsBy(How = How.CssSelector, Using = "[id='firstHeading']")]
        private IWebElement _headerLbl;

        public void AssertHeaderDisplayed() =>
            Assert.IsTrue(_headerLbl.Displayed, "The test fail in the step 4");
    }
}
