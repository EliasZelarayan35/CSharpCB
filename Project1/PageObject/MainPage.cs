using System;
using Selenium.QA;
using Common;

public class MainPage : BasePage
{
    By _mainSearchTxt = By.CssSelector("input[name='search']");
    By _searchBtn = By.CssSelector("div[class$='search__input'] > button");

    public void PerformSearch(string searchValue)
    {
        if(searchValue.Count >= 3)
        {
            SendKeys(searchValue, _mainSearchTxt);
            ClickOn(_searchBtn);
            driver.WaitForBrowserLoad();
        }
    }
}