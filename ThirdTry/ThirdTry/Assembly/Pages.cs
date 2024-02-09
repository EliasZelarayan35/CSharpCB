using SeleniumExtras.PageObjects;

namespace ThirdTry
{
    public static class Pages
    {
        private static T getPages<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(WebDriverFacade.getDriver, page);
            return page;
        }
        
        public static Home home
        {
            get { return getPages<Home>(); }
        }

        public static ResultPage resultPage
        {
            get { return getPages<ResultPage>(); }
        }
    }
}
