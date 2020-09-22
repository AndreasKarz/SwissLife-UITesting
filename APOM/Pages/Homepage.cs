using APOM.Molecules;
using OpenQA.Selenium;

namespace APOM.Pages
{
    public class Homepage : Page
    {
        public readonly QuickLinks QuickLinks;

        public Homepage(IWebDriver driver) : base(driver)
        {
            QuickLinks = new QuickLinks(Component);
        }

    }
}
