using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace KebabSimulator.SeleniumTests.Pages
{
    public class StorePage
    {
        private readonly IWebDriver _driver;
        private WebDriverWait Wait => new(_driver, TimeSpan.FromSeconds(10));

        public StorePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void StartClicker()
            => Wait.Until(d => d.FindElement(By.CssSelector("[data-game='clicker']"))).Click();

        public void StartReaction()
            => Wait.Until(d => d.FindElement(By.CssSelector("[data-game='reaction']"))).Click();

        public void StartMath()
            => Wait.Until(d => d.FindElement(By.CssSelector("[data-game='math']"))).Click();
    }
}
