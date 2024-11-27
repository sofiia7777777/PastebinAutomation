using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PastebinPages
{
    public class PasteDetailsPage
    {
        private readonly IWebDriver driver;

        public PasteDetailsPage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

        public string GetPasteTitle()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !string.IsNullOrEmpty(driver.Title));
            return driver.Title;
        }

        public bool IsSyntaxHighlightingSet(string expectedSyntax)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var syntaxElement = wait.Until(driver => driver.FindElement(By.XPath($"//div[contains(@class, 'source {expectedSyntax}')]")));
            return syntaxElement.GetAttribute("class").Contains(expectedSyntax);
        }

        public string GetCode()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var codeField = wait.Until(d => d.FindElement(By.CssSelector("ol.bash")));
            return codeField.Text;
        }
    }
}
