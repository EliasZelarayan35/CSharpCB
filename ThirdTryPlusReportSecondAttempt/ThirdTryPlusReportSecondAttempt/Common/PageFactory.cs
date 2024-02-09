using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ThirdTryPlusReportSecondAttempt
{
    public sealed class PageFactory
    {
        private PageFactory()
        {
        }

        public static T InitElements<T>(IWebDriver driver)
        {
            return InitElements<T>(new DefaultElementLocator(driver));
        }

        public static T InitElements<T>(IElementLocator locator)
        {
            ConstructorInfo constructor = typeof(T).GetConstructor(new Type[1] { typeof(IWebDriver) });
            if (constructor == null)
            {
                throw new ArgumentException("No constructor for the specified class containing a single argument of type IWebDriver can be found");
            }

            if (locator == null)
            {
                throw new ArgumentNullException("locator", "locator cannot be null");
            }

            if (!(locator.SearchContext is IWebDriver))
            {
                throw new ArgumentException("The search context of the element locator must implement IWebDriver", "locator");
            }

            T obj = (T)constructor.Invoke(new object[1] { locator.SearchContext as IWebDriver });
            InitElements(obj, locator);
            return obj;
        }

        public static void InitElements(ISearchContext driver, object page)
        {
            InitElements(page, new DefaultElementLocator(driver));
        }

        public static void InitElements(ISearchContext driver, object page, IPageObjectMemberDecorator decorator)
        {
            InitElements(page, new DefaultElementLocator(driver), decorator);
        }

        public static void InitElements(object page, IElementLocator locator)
        {
            InitElements(page, locator, new DefaultPageObjectMemberDecorator());
        }

        public static void InitElements(object page, IElementLocator locator, IPageObjectMemberDecorator decorator)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page", "page cannot be null");
            }

            if (locator == null)
            {
                throw new ArgumentNullException("locator", "locator cannot be null");
            }

            if (decorator == null)
            {
                throw new ArgumentNullException("locator", "decorator cannot be null");
            }

            if (locator.SearchContext == null)
            {
                throw new ArgumentException("The SearchContext of the locator object cannot be null", "locator");
            }

            Type type = page.GetType();
            List<MemberInfo> list = new List<MemberInfo>();
            list.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.Public));
            list.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.Public));
            while (type != null)
            {
                list.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic));
                list.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic));
                type = type.BaseType;
            }

            foreach (MemberInfo item in list)
            {
                object obj = decorator.Decorate(item, locator);
                if (obj != null)
                {
                    FieldInfo fieldInfo = item as FieldInfo;
                    PropertyInfo propertyInfo = item as PropertyInfo;
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(page, obj);
                    }
                    else if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(page, obj, null);
                    }
                }
            }
        }
    }
}
