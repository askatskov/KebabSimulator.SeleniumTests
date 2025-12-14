using KebabSimulator.SeleniumTests.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

[TestFixture]
[Order(4)]
public class LoginValidTests
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
    public void Login_With_Valid_Data_Works()
    {
        Assert.That(
            TestUserContext.Email,
            Is.Not.Null,
            "RegisterValidTests must run before LoginValidTests"
        );

        var login = new LoginPage(driver);
        login.Open(BaseUrl);

        login.Login(
            TestUserContext.Email,
            TestUserContext.Password
        );

        wait.Until(d => d.Url.Contains(""));

        Assert.That(driver.Url, Does.Contain(""));
    }

    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}
