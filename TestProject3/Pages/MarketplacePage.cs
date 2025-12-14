using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace KebabSimulator.SeleniumTests.Pages
{
    public class MarketplacePage
    {
        private readonly IWebDriver _driver;
        private WebDriverWait Wait => new(_driver, TimeSpan.FromSeconds(10));

        public MarketplacePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Open(string baseUrl)
        {
            _driver.Navigate().GoToUrl(baseUrl + "/Marketplace");
            Wait.Until(d => d.FindElement(By.Id("marketplace-page")));
        }

        public void CookFirstUnlocked()
        {
            Wait.Until(d =>
                d.FindElements(By.ClassName("kebab-cook-btn")).Any()
            );

            _driver.FindElements(By.ClassName("kebab-cook-btn")).First().Click();
        }
    }
}
