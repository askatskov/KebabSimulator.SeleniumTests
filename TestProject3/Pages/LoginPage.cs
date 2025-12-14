using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

public class LoginPage
{
    private readonly IWebDriver _driver;
    private WebDriverWait Wait => new(_driver, TimeSpan.FromSeconds(10));

    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
    }

    public void Open(string baseUrl)
    {
        _driver.Navigate().GoToUrl(baseUrl + "/Accounts/Login");
    }

    public void Login(string email, string password)
    {
        var emailInput = Wait.Until(d => d.FindElement(By.Id("login-email")));
        emailInput.Clear();
        emailInput.SendKeys(email);

        var passwordInput = Wait.Until(d => d.FindElement(By.Id("login-password")));
        passwordInput.Clear();
        passwordInput.SendKeys(password);

        Wait.Until(d => d.FindElement(By.Id("login-submit"))).Click();
    }

    public bool HasValidationErrors()
        => _driver.FindElements(By.Id("login-validation-summary")).Count > 0;
}
