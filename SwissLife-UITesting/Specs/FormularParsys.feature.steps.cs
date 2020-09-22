using APOM.Organisms;
using APOM.Pages;
using FunkyBDD.SxS.Selenium.WebDriver;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace Automated_E2E_Testing_Workshop.Specs
{
    [Binding]
    public sealed class FormularParsysSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        private readonly Page page;
        private readonly Dictionary<string, IWebElement> buttonList = new Dictionary<string, IWebElement>();
        private BaseForm form;

        public FormularParsysSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _driver = driver;
            driver.NavigateToPath("/de/testwiese/formular-parsys.html");
            page = new Page(driver);

            foreach (IWebElement button in page.Component.FindElements(By.CssSelector("a.a-button[data-analytics-id]")))
            {
                var label = button.GetAttribute("data-analytics-id").Replace("parsys-", "");
                buttonList.Add(label, button);
            }
        }

        [When(@"I select '(.*)'")]
        public void GivenISelect(string key)
        {
            buttonList[key].Click();
            form = new BaseForm(_driver);
        }

        [When(@"I select input '(.*)'")]
        public void WhenISelectInput(string key)
        {
            form.SelectInput(key);
        }

        [When(@"I fillout the customer")]
        public void WhenIFilloutTheCustomer()
        {
            form.WriteInput("Vorname-1553176421936", "Giuseppe");
            form.WriteInput("Nachname-1553176421936", "Ottaviani");
            form.WriteInput("Adresse-1553176421936", "Tomorrowland 2021");
            form.WriteInput("PLZ-1553176421936", "9999");
            form.WriteInput("Ort-1553176421936", "Demotown");
            form.WriteInput("E-Mail-1553176421936", "andreas.karz@swisslife.ch");
            form.WriteInput("Telefon-1553176421936", "+41 77 814 44 46");
        }

        [When(@"I write '(.*)' as message to '(.*)'")]
        public void WhenIWriteAsMessageTo(string message, string key)
        {
            form.WriteInput(key, message);
        }

        [When(@"I submit the form")]
        public void WhenISubmitTheForm()
        {
            form.SendForm();
        }

        [Then(@"The I see the confirmation")]
        public void ThenTheISeeTheConfirmation()
        {
            var confirmation = new BaseConfirmation(_driver);
            Assert.AreEqual("Danke für Ihre Kontaktaufnahme!", confirmation.Title);
            Thread.Sleep(5000);
            confirmation.Close();
        }


    }
}
