using APOM.Pages;
using BoDi;
using FunkyBDD.SxS.Selenium.Browserstack;
using FunkyBDD.SxS.Selenium.WebDriver;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SwissLife.SxS.Helpers;
using System;
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

        public Hooks(FeatureContext featureContext, ScenarioContext scenarioContext, IWebDriver driver, IConfiguration config)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
            _driver = driver;

            _testURL = config["BaseURL"];
        }

        [BeforeFeature]
        public static void BeforeFeature(IObjectContainer objectContainer)
        {
            /* initialize & register IConfiguration */
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddUserSecrets("81ff8e37-cda6-49f3-8b10-08b9d00f64d6")
                    .AddEnvironmentVariables(prefix: "E2E_")
                    .Build();
            objectContainer.RegisterInstanceAs<IConfiguration>(config);

            /* initialize & register Browser and IWebDriver */
            Browser browser = config["Browser"] == null ? new Browser("FirefoxLocal") : new Browser(config["Browser"]);
            objectContainer.RegisterInstanceAs<IWebDriver>(browser.Driver);
            objectContainer.RegisterInstanceAs<Browser>(browser);
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
