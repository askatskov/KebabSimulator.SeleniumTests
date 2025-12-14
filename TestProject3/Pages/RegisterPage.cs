using OpenQA.Selenium;

public class RegisterPage
{
    private readonly IWebDriver _driver;
    public RegisterPage(IWebDriver driver) => _driver = driver;

    public void Open(string baseUrl)
        => _driver.Navigate().GoToUrl(baseUrl + "/Accounts/Register");

    public void FillForm(string email, string password, string confirm, string city)
    {
        _driver.FindElement(By.Id("register-email")).SendKeys(email);
        _driver.FindElement(By.Id("register-password")).SendKeys(password);
        _driver.FindElement(By.Id("register-confirm-password")).SendKeys(confirm);
        _driver.FindElement(By.Id("register-city")).SendKeys(city);
    }

    public void Submit()
        => _driver.FindElement(By.Id("register-submit")).Click();

    public bool HasValidationErrors()
        => _driver.FindElements(By.Id("register-validation-summary")).Count > 0;
}
