using KebabSimulator.SeleniumTests.Drivers;
using NUnit.Framework;
using OpenQA.Selenium;

[TestFixture]
[Order(1)]
public class RegisterInvalidTests
{
    private IWebDriver driver;
    private const string BaseUrl = "https://localhost:7240";

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
    }

    [Test]
    public void Register_With_Invalid_Data_Shows_Validation_Errors()
    {
        var register = new RegisterPage(driver);
        register.Open(BaseUrl);

        register.FillForm(
            email: "bademail",
            password: "123",
            confirm: "456",
            city: ""
        );

        register.Submit();

        Assert.That(register.HasValidationErrors(), Is.True);
        Assert.That(driver.Url, Does.Contain("/Accounts/Register"));
    }

    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}
