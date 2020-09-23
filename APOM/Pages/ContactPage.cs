using OpenQA.Selenium;

namespace APOM.Pages
{
    public class ContactPage : Page
    {
        private readonly IWebDriver _driver;

        public ContactPage(IWebDriver driver) : base(driver)
        {

        }

        public void OpenPrivateContactForm()
        {
            Component.FindElement(By.CssSelector("a[data-analytics-id='kontakt-private-cta']")).Click();
        }
    }
}
