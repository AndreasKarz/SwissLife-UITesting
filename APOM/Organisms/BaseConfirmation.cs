using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebDriver;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;

namespace APOM.Organisms
{
    public class BaseConfirmation : DefaultProps
    {
        public Dictionary<string, IWebElement> Inputs = new Dictionary<string, IWebElement>();
        public string Title;
        public string Text;
        private IWebElement CloseButton;

        private void initComponent()
        {
            Title = Component.FindElement(By.TagName("h2")).Text;
            Text = Component.FindElement(By.TagName("p")).Text;
            CloseButton = Component.FindElement(By.CssSelector("button.o-overlay-header__close-button"));
        }

        public BaseConfirmation(IWebDriver driver)
        {
            Component = driver.FindElementFirstOrDefault(By.CssSelector("div[data-fetch-content='true']"), 3);
            Thread.Sleep(2000);
            initComponent();
        }

        public void SendForm()
        {
            Component.FindElementFirstOrDefault(By.CssSelector("button[type='submit']")).Click();
        }

        public void Close()
        {
            CloseButton.Click();
        }
    }
}
