using APOM.Pages;
using BoDi;
using FunkyBDD.SxS.Selenium.Browserstack;
using FunkyBDD.SxS.Selenium.WebDriver;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SwissLife.SxS.Helpers;
using System;
using System.IO;
using System.Threading;
using TechTalk.SpecFlow;

namespace Automated_E2E_Testing_Workshop
{
    [Binding]
    public sealed class Hooks
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        private readonly string _testURL;
        private Homepage _homepage;

        public IWebDriver Driver => _driver;

        public Hooks(FeatureContext featureContext, ScenarioContext scenarioContext, IWebDriver driver, JObject config)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
            _driver = driver;

            _testURL = config["BaseURL"].ToString();
        }

        [BeforeTestRun]
        public static void InitFramework()
        {
        }

        [BeforeFeature]
        public static void BeforeFeature(IObjectContainer objectContainer, FeatureContext featureContext)
        {
            string configText = File.ReadAllText("appsettings.json");
            var config = JObject.Parse(configText);
            objectContainer.RegisterInstanceAs<JObject>(config);

            Browser browser;
            if (Environment.GetEnvironmentVariable("TEST_BROWSER") == null)
            {
                browser = new Browser("FirefoxLocal");
            }
            else
            {
                browser = new Browser(Environment.GetEnvironmentVariable("TEST_BROWSER"));
            }

            objectContainer.RegisterInstanceAs<IWebDriver>(browser.Driver);
            objectContainer.RegisterInstanceAs<Browser>(browser);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
        }

        [Given(@"I open the Homepage"), Given(@"Die Homepage ist geöffnet")]
        public void IOpenTheTestPage()
        {
            _driver.Navigate().GoToUrl(_testURL);
            _homepage = new Homepage(_driver);
        }

        [Given(@"The language is set to '(.*)'"), When(@"I change the language to '(.*)'"), Given(@"Die Sprache ist auf '(.*)' gestellt")]
        public void GivenTheLanguageIsSetTo(string lang)
        {
            _homepage.SetLangTo(lang);
            _homepage = new Homepage(_driver); /* ReInit the Page, otherwise all elements will be staled! */
        }

        [Given(@"I confirm the disclaimer"), When(@"I confirm the disclaimer"), When(@"Ich den Disclaimer bestätige")]
        public void WennIchDenDisclaimerBestatige()
        {
            try
            {
                _homepage.AcceptCookieDisclaimer();
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                //
            }
        }

        [AfterScenario]
        public void AfterScenarion()
        {
            if (_driver != null)
            {
                var screenShotFileName = $"{_featureContext.FeatureInfo.Title}__{_scenarioContext.ScenarioInfo.Title}.png";
                screenShotFileName = FileHelpers.RemoveIllegalFileNameChars(screenShotFileName);
                _driver.MakeScreenshot(screenShotFileName);
            }
        }

        [AfterFeature]
        public static void AfterFeature(Browser browser)
        {
            if (browser != null)
            {
                browser.DisposeDriver();
            }
        }
    }
}
