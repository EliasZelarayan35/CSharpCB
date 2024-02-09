using system
using Selenium.QA;

public class BasePage
{
    public void ClickOnElement(By locator)
    {
        if(isElementPresent(locator))
            driver.Click(locator);
    }

    public void SendKeys(string text ,By locator)
    {
        if(isElementPresent(locator)){
            var element = driver.FindElement(element);
            if(!element.isDisplayed)
                driver.MoveToElement(element);
            driver.SendKeys(text, element);
        }
            
    }

    protected boolean isElementPresent(By by){
        try{
            driver.FindElement(by);
            return true;
        }
        catch(NoSuchElementException e){
            return false;
        }
    }
}