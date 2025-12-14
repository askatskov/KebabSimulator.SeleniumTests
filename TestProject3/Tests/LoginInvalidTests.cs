using KebabSimulator.SeleniumTests.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;

[TestFixture]
[Order(2)]
public class LoginInvalidTests
{
    private IWebDriver driver;
    private const string BaseUrl = "https://localhost:7240";

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
    }

    [Test]
    public void Login_With_Invalid_Credentials_Does_Not_Login_User()
    {
        var login = new LoginPage(driver);
        login.Open(BaseUrl);

        login.Login("wrong@test.com", "wrongpassword");

        Assert.That(driver.Url, Does.Contain("/Accounts/Login"));

        Assert.That(
            driver.PageSource.ToLower(),
            Does.Contain("invalid")
                .Or.Contain("error")
                .Or.Contain("incorrect")
        );
    }


    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}
