using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PastebinPages;

namespace PastebinTests
{
    [TestFixture]
    public class PastebinPageTests
    {
        private IWebDriver? driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

       
        private PasteDetailsPage CreateAndSubmitPaste()
        {
            var createPastePage = new CreatePastePage(driver!);
            createPastePage.Open().AcceptCookiesIfPresent();
            createPastePage.InputCode("git config --global user.name  \"New Sheriff in Town\"\r\n" +
                                      "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\r\n" +
                                      "git push origin master --force\r\n");
            createPastePage.SyntaxHighlight("Bash");
            createPastePage.SelectPasteExpiration("10 Minutes");
            createPastePage.InputTitle("how to gain dominance among developers");
            createPastePage.CreatePaste();

            return new PasteDetailsPage(driver!);
        }
        

        [Test]
        public void VerifyPageTitle_ShowsExpectedTitle()
        {
            CreateAndSubmitPaste();
            var detailsPage = new PasteDetailsPage(driver!);
            Assert.That(detailsPage.GetPasteTitle(), Is.EqualTo("how to gain dominance among developers - Pastebin.com"));
        }

        [Test]
        public void VerifySyntaxHighlight_ShowsBash()
        {
            CreateAndSubmitPaste();
            var detailsPage = new PasteDetailsPage(driver!);
            bool isBashSyntax = detailsPage.IsSyntaxHighlightingSet("bash");
            Assert.That(isBashSyntax, Is.True);
        }

        [Test]
        public void VerifyCodeField_ContainsExpectedCode()
        {
            CreateAndSubmitPaste();
            var detailsPage = new PasteDetailsPage(driver!);
            var actualCode = detailsPage.GetCode();
            Assert.That(actualCode.TrimEnd(), Is.EqualTo("git config --global user.name  \"New Sheriff in Town\"\r\n" +
                                                         "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\r\n" +
                                                         "git push origin master --force"));
        }

        [TearDown] 
        public void TearDown() 
        {
            if (driver != null) 
            { 
                driver.Quit(); 
                driver.Dispose(); 
                driver = null; 
            }
        }
    }
}