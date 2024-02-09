using CodeBase.Common;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.PageObjects
{
    public partial class ResultsPage : BasePage
    {
        private readonly IWebDriver _driver;
        public By _headerLbl => By.CssSelector("[id='firstHeading']");

        public ResultsPage(IWebDriver browser) => _driver = browser;

        public void AssertHeaderDisplayed() =>
            Assert.IsTrue(isElementVisible(_headerLbl), "The test fail in the step 4");
    }
}
