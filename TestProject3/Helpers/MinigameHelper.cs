using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace KebabSimulator.SeleniumTests.Helpers
{
    public static class MinigameHelper
    {
        public static void PlayUntilKebabReady(
            IWebDriver driver,
            WebDriverWait wait,
            Action playStep
        )
        {
            while (true)
            {
                var timeLeftEl = driver.FindElement(By.Id("timeLeft"));
                int timeLeft = int.Parse(timeLeftEl.Text);

                if (timeLeft <= 0)
                    break;

                playStep();

                // маленькая пауза, чтобы UI успел обновиться
                wait.Until(d =>
                {
                    var t = d.FindElement(By.Id("timeLeft"));
                    return int.Parse(t.Text) <= timeLeft;
                });
            }
        }
    }
}
