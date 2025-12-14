using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KebabSimulator.SeleniumTests.Drivers;
using KebabSimulator.SeleniumTests.Pages;
using KebabSimulator.SeleniumTests.Helpers;
using System;

[TestFixture]
[Order(7)]
public class ClickerTests
{
    private IWebDriver driver;
    private WebDriverWait wait;
    private const string BaseUrl = "https://localhost:7240";

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
    }

    [Test]
    public void Clicker_Game_Completes_And_Sell_Works()
    {
        var mp = new MarketplacePage(driver);
        mp.Open(BaseUrl);
        mp.CookFirstUnlocked();

        var store = new StorePage(driver);
        store.StartClicker();

        var grillBtn = wait.Until(d => d.FindElement(By.Id("grillButton")));

        MinigameHelper.PlayUntilKebabReady(driver, wait, () =>
        {
            grillBtn.Click();
        });

        var sellBtn = wait.Until(d => d.FindElement(By.Id("sellBtn")));
        sellBtn.Click();

        wait.Until(d => d.Url.Contains("/Kebab"));
        Assert.That(driver.Url, Does.Contain("/Kebab"));
    }

    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}
