using APOM.Organisms;
using OpenQA.Selenium;

namespace APOM.Pages
{
    public class SearchResultPage : Page
    {
        private readonly IWebElement _parent;
        public readonly SearchResult SearchResult;

        public SearchResultPage(IWebDriver driver) : base(driver)
        {
            _parent = driver.FindElement(By.TagName("body"));
            SearchResult = new SearchResult(_parent);
        }

        public int Count => SearchResult.SearchResultCount;
    }
}
