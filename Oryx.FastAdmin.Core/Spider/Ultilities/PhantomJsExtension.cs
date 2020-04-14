using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core.Spider.Ultilities
{
    public static class PhantomJsExtension
    {
        public static List<string> GetSpiderResults(this PhantomJSDriver driver, string query)
        {
            try
            {
                var querySplitArr = query.Split('@');
                var queryStr = querySplitArr[0];
                var queryAttribute = querySplitArr[1];
                var finedElements = driver.FindElements(By.CssSelector(queryStr));
                var spiderResult = new List<string>();
                foreach (var elementItem in finedElements)
                {
                    if (queryAttribute == "text")
                    {
                        spiderResult.Add(elementItem.Text);
                    }
                    else if (queryAttribute == "html")
                    {
                        spiderResult.Add(elementItem.GetAttribute("innerHTML"));
                    }
                    else
                    {
                        spiderResult.Add(elementItem.GetAttribute(queryAttribute));
                    }
                }
                return spiderResult;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return default(List<string>);
        }

        public static void ScrollToEnd(this IWebDriver driver, int times = 1, int durationSecond = 5)
        {
            for (int i = 0; i < times; i++)
            {
                var _driver = (PhantomJSDriver)driver;
                _driver.ExecuteScript("window.scrollTo(0,document.body.offsetHeight);");
                Thread.Sleep(durationSecond * 1000);
            }
        }

        public static string FindValueByCss(this IWebElement webElement, string query)
        {
            var querySplitArr = query.Split('@');
            var queryStr = querySplitArr[0];
            var queryAttribute = querySplitArr[1];
            var targetElement = webElement.FindElement(By.CssSelector(queryStr));

            if (targetElement == null)
            {
                return string.Empty;
            }

            var result = string.Empty;
            if (queryAttribute == "text")
            {
                result = targetElement.Text;
            }
            else if (queryAttribute == "html")
            {
                result = targetElement.GetAttribute("innerHTML");
            }
            else
            {
                result = targetElement.GetAttribute(queryAttribute);
            }

            return result.Trim();
        }

        public static List<string> FindMultiValueByCss(this IWebElement webElement, string query)
        {
            try
            {
                var querySplitArr = query.Split('@');
                var queryStr = querySplitArr[0];
                var queryAttribute = querySplitArr[1];
                var finedElements = webElement.FindElements(By.CssSelector(queryStr));
                var spiderResult = new List<string>();
                if (finedElements == null || finedElements.Count < 1)
                {
                    return null;
                }
                foreach (var elementItem in finedElements)
                {
                    if (queryAttribute == "text")
                    {
                        spiderResult.Add(elementItem.Text.Trim());
                    }
                    else if (queryAttribute == "html")
                    {
                        spiderResult.Add(elementItem.GetAttribute("innerHTML").Trim());
                    }
                    else
                    {
                        spiderResult.Add(elementItem.GetAttribute(queryAttribute).Trim());
                    }
                }
                return spiderResult;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            return default(List<string>);
        }
    }
}
