using APOM.Atoms;
using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;

namespace APOM.Organisms
{
    public class SearchBar : DefaultProps
    {
        public Link Link;
        public Input Input;

        private void InitComponent(IWebElement parent)
        {
            Link = new Link(parent, By.Id("a11y-header-search-link"));
            Input = new Input(Component);
        }

        public SearchBar(IWebElement parent)
        {
            Component = parent.FindElementFirstOrDefault(By.CssSelector("div[data-g-name='SearchBox']"), 2);
            if (Component != null)
            {
                InitComponent(parent);
            }
        }

        public SearchBar(IWebElement parent, By by)
        {
            Component = parent.FindElement(by);
            InitComponent(parent);
        }

        public SearchBar(IWebElement parent, string dataTestId)
        {
            Component = parent.FindElement(By.XPath($".//*[@data-test-id='{dataTestId}']"));
            InitComponent(parent);
        }

        public void Open()
        {
            Link.Click();
        }

        public void SearchFor(string query)
        {
            Open();
            Input.Write(query);
        }

        public void SearchWithEnter() => Input.Write(Keys.Enter);
    }
}
