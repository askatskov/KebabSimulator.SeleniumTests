using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using KebabSimulator.SeleniumTests.Drivers;
using KebabSimulator.SeleniumTests.Pages;
using System;

[TestFixture]
[Order(9)]
public class MathTests
{
    private IWebDriver driver;
    private WebDriverWait wait;
    private const string BaseUrl = "https://localhost:7240";

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
    }

    [Test]
    public void Math_Game_Completes_With_Correct_Answers_And_Sell_Works()
    {
        var marketplace = new MarketplacePage(driver);
        marketplace.Open(BaseUrl);
        marketplace.CookFirstUnlocked();

        var store = new StorePage(driver);
        store.StartMath();

        var questionEl = wait.Until(d => d.FindElement(By.Id("math-q")));
        var answerInput = driver.FindElement(By.Id("math-a"));
        var submitBtn = driver.FindElement(By.Id("math-submit"));

        Assert.That(questionEl.Displayed);
        Assert.That(answerInput.Displayed);
        Assert.That(submitBtn.Displayed);

        while (true)
        {
            var timeLeftEl = driver.FindElement(By.Id("timeLeft"));
            int timeLeft = int.Parse(timeLeftEl.Text);

            if (timeLeft <= 0)
                break;

            string correctAnswer = questionEl.GetAttribute("data-answer");
            Assert.That(correctAnswer, Is.Not.Null.And.Not.Empty);

            answerInput.Clear();
            answerInput.SendKeys(correctAnswer);
            submitBtn.Click();

            wait.Until(d =>
                d.FindElement(By.Id("math-q")).GetAttribute("data-answer") != correctAnswer
                || int.Parse(d.FindElement(By.Id("timeLeft")).Text) <= 0
            );
        }

        var sellBtn = wait.Until(d => d.FindElement(By.Id("sellBtn")));
        Assert.That(sellBtn.Enabled);
        sellBtn.Click();

        wait.Until(d => d.Url.Contains("/Kebab"));
        Assert.That(driver.Url, Does.Contain("/Kebab"));
    }

    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
        driver.Dispose();
    }
}
