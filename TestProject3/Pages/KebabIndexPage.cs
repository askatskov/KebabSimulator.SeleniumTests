using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

public class KebabIndexPage
{
    private readonly IWebDriver _driver;
    private WebDriverWait Wait => new(_driver, TimeSpan.FromSeconds(10));

    public KebabIndexPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Open(string baseUrl)
    {
        _driver.Navigate().GoToUrl(baseUrl + "/Kebab");
        Wait.Until(d => d.FindElement(By.Id("kebab-index-title")).Displayed);
    }

    public void GoToMarketplace()
    {
        Wait.Until(d => d.FindElement(By.Id("btn-marketplace"))).Click();
    }

    public void GoToUpgrades()
    {
        Wait.Until(d => d.FindElement(By.Id("btn-upgrades"))).Click();
    }

    public void OpenFirstKebabDetails()
    {
        var btn = Wait.Until(d =>
            d.FindElements(By.CssSelector(".kebab-details-btn")).First()
        );

        btn.Click();
    }

    public bool HasKebabs()
    {
        return _driver.FindElements(By.CssSelector(".kebab-row")).Any();
    }
}
