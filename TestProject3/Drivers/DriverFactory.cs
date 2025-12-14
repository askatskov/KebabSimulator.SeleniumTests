using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace KebabSimulator.SeleniumTests.Drivers
{
    public static class DriverFactory
    {
        public static IWebDriver Create()
        {
            var options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;
            options.AddArgument("--width=1400");
            options.AddArgument("--height=900");
            return new FirefoxDriver(options);
        }
    }
}
