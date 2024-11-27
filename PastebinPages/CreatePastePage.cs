using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PastebinPages
{
    public class CreatePastePage
    {
        public static string Url { get; } = "https://pastebin.com/";

        private readonly IWebDriver driver;

        public CreatePastePage(IWebDriver driver) => this.driver = driver ?? throw new ArgumentNullException(nameof(driver));

        public CreatePastePage Open()
        {
            driver.Url = Url;
            return this;
        }

        public IWebElement WaitForElement(By elementLocator)
        {
            var waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromSeconds(0.25),
                Message = $"Element with {elementLocator} has not been found"
            };

            return waitForElement.Until(driver => driver.FindElement(elementLocator));
        }


        public void InputCode(string code)
        {
            var codeField = WaitForElement(By.Id("postform-text"));
            codeField.Click();
            codeField.SendKeys(code);
        }

        public void SyntaxHighlight(string style)
        {
            var syntaxDropdown = WaitForElement(By.Id("postform-format"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].style.visibility = 'visible'; arguments[0].style.height = 'auto'; arguments[0].style.width = 'auto';", syntaxDropdown);

            var selectElement = new SelectElement(syntaxDropdown);

            selectElement.SelectByText(style);
        }

        public void SelectPasteExpiration(string expiration)
        {
            var expirationSelectElement = WaitForElement(By.Id("postform-expiration"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].style.visibility = 'visible'; arguments[0].style.height = 'auto'; arguments[0].style.width = 'auto';", expirationSelectElement);

            var selectElement = new SelectElement(expirationSelectElement);

            string expirationValue = SelectExpirationOption(expiration);

            selectElement.SelectByValue(expirationValue);
        }

        public void InputTitle(string title)
        {
            var pasteTitleField = WaitForElement(By.Id("postform-name"));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", pasteTitleField);

            pasteTitleField.SendKeys(title);
        }

        public void CreatePaste()
        {
            var createPasteButton = WaitForElement(By.CssSelector("button.btn.-big"));
            createPasteButton.Click();
        }

        public static string SelectExpirationOption(string expirationText)
        {
            var expirationMapping = new Dictionary<string, string>
            {
                { "Never", "N" },
                { "Burn after read", "B" },
                { "10 Minutes", "10M" },
                { "1 Hour", "1H" },
                { "1 Day", "1D" },
                { "1 Week", "1W" },
                { "2 Weeks", "2W" },
                { "1 Month", "1M" },
                { "6 Months", "6M" },
                { "1 Year", "1Y" }
            };

            string valueToSelect = expirationMapping[expirationText];

            return valueToSelect;
        }

        public void AcceptCookiesIfPresent()
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var agreeButton = wait.Until(driver => driver.FindElement(By.XPath("//button[contains(span/text(), 'AGREE')]")));
                agreeButton.Click();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("No consent modal found, proceeding...");
            }
        }
    }
}
