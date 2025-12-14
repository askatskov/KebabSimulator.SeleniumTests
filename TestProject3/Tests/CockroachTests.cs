using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KebabSimulator.SeleniumTests.Drivers;
using System;

[TestFixture]
[Order(10)]
public class Auth_Register_Login_Cockroach_Test
{
    private IWebDriver driver;
    private WebDriverWait wait;

    private const string BaseUrl = "https://localhost:7240";
    private const string Password = "Test123!";

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
    }

    [Test]
    public void Register_Login_And_Kill_Cockroach_Works()
    {
        var email = $"test_{Guid.NewGuid():N}@example.com";

        var register = new RegisterPage(driver);
        register.Open(BaseUrl);

        register.FillForm(
            email: email,
            password: Password,
            confirm: Password,
            city: "Tallinn"
        );

        register.Submit();

        wait.Until(d => d.Url.Contains("/Accounts/Login"));
        Assert.That(driver.Url, Does.Contain("/Accounts/Login"));

        var login = new LoginPage(driver);
        login.Login(email, Password);

        wait.Until(d => d.Url.Contains(""));
        Assert.That(driver.Url, Does.Contain(""));

        var page = new CockroachPage(driver);

        page.Open(BaseUrl);

        var roach = page.WaitForCockroach();
        Assert.That(roach.Displayed, Is.True);

        page.KillCockroach();

        wait.Until(_ => page.IsDead());

        Assert.Pass("Cockroach successfully killed");
    }

    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}


