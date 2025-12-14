using KebabSimulator.SeleniumTests.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

[TestFixture]
[Order(5)]
public class KebabIndexTests
{
    private IWebDriver driver;
    private WebDriverWait wait;
    private const string BaseUrl = "https://localhost:7240";

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    [Test]
    public void Kebab_Index_Loads_And_Shows_Kebabs()
    {
        var page = new KebabIndexPage(driver);
        page.Open(BaseUrl);

        Assert.That(page.HasKebabs(), Is.True);
    }

    [Test]
    public void Can_Open_Kebab_Details_And_Go_Back()
    {
        var page = new KebabIndexPage(driver);
        page.Open(BaseUrl);

        page.OpenFirstKebabDetails();

        Assert.That(driver.Url, Does.Contain("/Kebab/Details/"));

        driver.Navigate().Back();

        wait.Until(d => d.Url.EndsWith("/Kebab"));
        Assert.That(driver.Url, Does.Contain("/Kebab"));
    }

    [Test]
    public void Can_Navigate_To_Marketplace_And_Upgrades()
    {
        var page = new KebabIndexPage(driver);
        page.Open(BaseUrl);

        page.GoToMarketplace();
        Assert.That(driver.Url, Does.Contain("/Marketplace"));

        driver.Navigate().Back();

        page.GoToUpgrades();
        Assert.That(driver.Url, Does.Contain("/Marketplace/Upgrades"));
    }

    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}
