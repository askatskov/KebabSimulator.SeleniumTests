using KebabSimulator.SeleniumTests.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

[TestFixture]
[Order(3)]
public class RegisterValidTests
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
    public void Register_With_Valid_Data_Works()
    {
        var register = new RegisterPage(driver);
        register.Open(BaseUrl);

        TestUserContext.Email = $"test_{Guid.NewGuid():N}@example.com";

        register.FillForm(
            email: TestUserContext.Email,
            password: TestUserContext.Password,
            confirm: TestUserContext.Password,
            city: "Tallinn"
        );

        register.Submit();

        wait.Until(d => d.Url.Contains("/Accounts/Login"));

        Assert.That(driver.Url, Does.Contain("/Accounts/Login"));
    }

    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}
