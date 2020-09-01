using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using APOM.Pages;
using FunkyBDD.SxS.Selenium.WebDriver;
using FunkyBDD.SxS.Selenium.WebElement;
using OpenQA.Selenium;
using SwissLife.SxS.Helpers;
using TechTalk.SpecFlow;

namespace Automated_E2E_Testing_Workshop.Specs
{
    [Binding]
    public sealed class DiverseKomponentenSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly PageDiverseKomponenten page;

        public DiverseKomponentenSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            page = new PageDiverseKomponenten(Hooks.Page.Driver);
        }

        [Then(@"Die Screenshots aller Komponenten stimmen mit der Baseline überein")]
        public void ThenDieScreenshotsAllerKomponentenStimmenMitDerBaselineUberein()
        {
            foreach (var component in page.Components)
            {
                if (component.Size.Width == 0 || component.Size.Height == 0)
                {
                    continue;
                }

                component.ScrollTo();
                Thread.Sleep(2000);
                var imageComparisonPath = Hooks.Config["ImageComparisonPath"].ToString();

                var type = FileHelpers.RemoveIllegalFileNameChars(component.GetAttribute("class"));
                var hash = GetHash(component.Text);
                string  fileName = $"{imageComparisonPath}/{type}__{hash}.png";

                var screenshot = Hooks.Page.Driver.GetElementScreenshot(component);
                screenshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

            }
        }

        private static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        private static string GetHash(string inputString)
        {
            if (String.IsNullOrEmpty(inputString))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}
