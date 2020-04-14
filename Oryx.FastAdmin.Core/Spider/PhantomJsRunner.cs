using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.FastAdmin.Core.Spider
{
    public class PhantomJsRunner
    {
        private PhantomJSDriverService driverService { get; set; }

        public IWebDriver WebDriver { get; set; }

        public PhantomJsRunner(Cookie cookie, string proxyAddress, string proxyPort)
        {
            driverService = PhantomJSDriverService.CreateDefaultService(AppDomain.CurrentDomain.BaseDirectory);
            driverService.LoadImages = false;
            driverService.HideCommandPromptWindow = false;
            driverService.AddArgument("--webdriver-loglevel=ERROR");
            driverService.LogFile = AppDomain.CurrentDomain.BaseDirectory + "phantomjs.log";

            var options = new PhantomJSOptions();
            options.AddAdditionalCapability("phantomjs.page.settings.userAgent", "Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1");
            options.AddAdditionalCapability("phantomjs.page.customHeaders.Referer", "http://m.yaoqi520.net/");


            if (!string.IsNullOrEmpty(proxyAddress) && !string.IsNullOrEmpty(proxyPort))
            {
                SetProxy(proxyAddress, proxyPort);
            }
            WebDriver = new PhantomJSDriver(driverService, options);
            if (cookie != null)
            {
                SetCookie(cookie);
            }

        }

        public void SetCookie(Cookie cookie)
        {

            WebDriver.Manage().Cookies.AddCookie(cookie);
        }

        public void SetProxy(string address, string port)
        {
            Proxy proxy = new Proxy();
            proxy.HttpProxy = string.Format(address + ":" + port);
            driverService.Proxy = proxy.HttpProxy;
            driverService.ProxyType = "https";
        }
    }
}
