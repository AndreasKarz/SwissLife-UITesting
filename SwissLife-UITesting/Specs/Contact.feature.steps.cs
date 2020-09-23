using APOM.Organisms;
using APOM.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automated_E2E_Testing_Workshop.Specs
{
    [Binding, Scope(Feature = "Homepage Contacts")]
    public sealed class ContactSteps
    {
        private readonly ContactPage _page;
        private BaseForm _form;
        private readonly IWebDriver _driver;

        public ContactSteps(IWebDriver driver)
        {
            _driver = driver;
            _page = new ContactPage(driver);
        }

        [When(@"I open the contact form for private customers")]
        public void WhenIOpenTheContactFormForPrivateCustomers()
        {
            _page.OpenPrivateContactForm();
            _form = new BaseForm(_driver);
        }

        [When(@"I fillout the form with valid infos")]
        public void WhenIFilloutTheFormWithValidInfos()
        {
            _form.SelectInput("Betrifft-1554988802546_0");
            _form.SelectInput("Anrede-1553703294075_1");
            _form.WriteInput("Vorname-1553176421936", "Andreas");
            _form.WriteInput("Nachname-1553176421936", "Karz");
            _form.WriteInput("PLZ-1553176421936", "8041");
            _form.WriteInput("Ort-1553176421936", "Zürich");
            _form.WriteInput("email-1553176421936", "andreas.karz@swisslife.ch");
            _form.WriteInput("Telefon-1553176421936", "+41 77 814 44 46");
            _form.WriteInput("Nachricht-1553176699112", "Dieses Formular wurde durch einen automatisierten Test ausgeführt.");
            _form.SelectInput("Rueckruf-1553762364726_0");
            _form.SelectInput("Thema-1553762364726_2");
        }

        [When(@"I submit the form")]
        public void WhenISubmitTheForm()
        {
            _form.SendForm();
        }

        [Then(@"I see the confirmation")]
        public void ThenISeeTheConfirmation()
        {
            /* This is not possible in this form, because for such tests the captcha must be deactivated. */
            Assert.True(true);
        }
    }
}
