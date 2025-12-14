using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

public class CockroachPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public CockroachPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
    }



    public void Open(string baseUrl)
    {
        driver.Navigate().GoToUrl(baseUrl + "/Kebab");
    }

    public IWebElement WaitForCockroach()
    {
        wait.Until(d =>
        {
            var js = (IJavaScriptExecutor)d;
            return (bool)(js.ExecuteScript(
                "return window.cockroachOnScreen === true;"
            ));
        });

        return driver.FindElement(By.Id("cockroach"));
    }

    public void KillCockroach()
    {
        var roach = WaitForCockroach();

        ((IJavaScriptExecutor)driver)
            .ExecuteScript("arguments[0].click();", roach);
    }

    public bool IsDead()
    {
        var roach = driver.FindElement(By.Id("cockroach"));
        var opacity = roach.GetCssValue("opacity");
        return opacity == "0";
    }
}



