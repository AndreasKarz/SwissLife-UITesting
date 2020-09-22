using APOM.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automated_E2E_Testing_Workshop.Specs
{
    [Binding]
    public sealed class Suche
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly Homepage homepage;
        private SearchResultPage searchResultPage;

        public Suche(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;
            homepage = new Homepage(_driver);
            if (homepage.Disclaimer.Displayed)
            {
                homepage.AcceptCookieDisclaimer();
            }
        }

        [When(@"Ich nach dem Begriff '(.*)' suche")]
        public void WennIchNachDemBegriffSuche(string suchbegriff)
        {
            homepage.SearchBar.SearchFor(suchbegriff);
            homepage.SearchBar.SearchWithEnter();
            searchResultPage = new SearchResultPage(_driver);
        }

        [Then(@"Werden mindestens (.*) Resultate gefunden")]
        public void DannWerdenMindestensResultateGefunden(int anzahl)
        {
            Assert.GreaterOrEqual(searchResultPage.Count, anzahl);
        }

        [Then(@"Wird die Meldung '(.*)' angezeigt")]
        public void DannWirdDieMeldungAngezeigt(string meldung)
        {
            Assert.AreEqual(0, searchResultPage.Count);
            Assert.AreEqual(meldung, searchResultPage.SearchResult.ResultMessage);
        }

    }
}
