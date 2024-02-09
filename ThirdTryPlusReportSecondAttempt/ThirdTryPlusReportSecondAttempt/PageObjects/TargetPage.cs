using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdTryPlusReportSecondAttempt
{
    public class TargetPage
    {
        #region locators
        [FindsBy(How = How.Id, Using = "message")]
        IWebElement Message;
        #endregion locators

        #region methods
        public string GetMessage()
        {
            return Message.Text;
        }
        #endregion methods
    }
}
