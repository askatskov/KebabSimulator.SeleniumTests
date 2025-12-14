using NUnit.Framework;
using OpenQA.Selenium;
using KebabSimulator.SeleniumTests.Drivers;
using KebabSimulator.SeleniumTests.Pages;

[TestFixture]
[Order(6)]
public class MarketplaceTests
{
    private IWebDriver driver;
    private const string BaseUrl = "https://localhost:7240";

    [SetUp]
    public void Setup() => driver = DriverFactory.Create();

    [Test]
    public void Marketplace_Cook_Kebab()
    {
        var mp = new MarketplacePage(driver);
        mp.Open(BaseUrl);
        mp.CookFirstUnlocked();

        Assert.That(driver.Url, Does.Contain("/Kebab/Store"));
    }


    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}
