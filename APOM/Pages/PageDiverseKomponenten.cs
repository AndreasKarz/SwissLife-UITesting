using FunkyBDD.SxS.Selenium.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APOM.Pages
{
    public class PageDiverseKomponenten : Page
    {
        public IWebElement Parent;
        public List<IWebElement> Components = new List<IWebElement>();

        public PageDiverseKomponenten(IWebDriver driver) : base(driver)
        {
            driver.NavigateToPath("/de/testwiese/komponenten-diverse.html");
            Parent = driver.FindElementFirstOrDefault(By.Id("main"), 10);

            Components = Parent.FindElements( By.CssSelector(".o-template > div") ).ToList<IWebElement>();
        }


    }
}
