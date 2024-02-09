using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdTryPlusReportSecondAttempt
{
    public static class Pages
    {
        private static T getPages<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(Browser.getDriver, page);
            return page;
        }

        public static TargetPage targetPage
        {
            get { return getPages<TargetPage>(); }
        }
        public static WebFormPage webFormPage
        {
            get { return getPages<WebFormPage>(); }
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
