using FunkyBDD.SxS.Selenium.APOM;
using FunkyBDD.SxS.Selenium.WebDriver;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace APOM.Organisms
{
    public class BaseForm : DefaultProps
    {
        public Dictionary<string, IWebElement> Inputs = new Dictionary<string, IWebElement>();

        private void InitComponent()
        {
            foreach (var (input, label, type) in from IWebElement input in Component.FindElements(By.TagName("input"))
                                                 let label = input.GetAttribute("id")
                                                 let type = input.GetAttribute("type")
                                                 select (input, label, type))
            {
                if (type != "hidden")
                {
                    Inputs.Add(label, input);
                }
            }

            foreach (var (input, label, type) in from IWebElement input in Component.FindElements(By.TagName("textarea"))
                                                 let label = input.GetAttribute("id")
                                                 let type = input.GetAttribute("type")
                                                 select (input, label, type))
            {
                Inputs.Add(label, input);
            }
        }

        public BaseForm(IWebDriver driver)
        {
            Component = driver.FindElementFirstOrDefault(By.CssSelector("div[data-fetch-content='true']"));
            Thread.Sleep(600); /* wait until the animation is finished */
            InitComponent();
        }

        public void SelectInput(string key)
        {
            Inputs[key].SendKeys(Keys.Space);
        }

        public void WriteInput(string key, string value)
        {
            Inputs[key].SendKeys(value);
        }

        public void SendForm()
        {
            Component.FindElementFirstOrDefault(By.CssSelector("button[type='submit']")).Click();
        }
    }
}
