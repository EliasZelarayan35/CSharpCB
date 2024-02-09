using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.PageObjects
{
    public partial class MainPage
    {
        public By _mainSearchTxt => By.CssSelector("input[name='search']");
        public By _searchBtn => By.CssSelector("div[class$='search__input'] > button");
    }
}
