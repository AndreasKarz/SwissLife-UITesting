using FunkyBDD.SxS.Selenium.WebDriver;
using FunkyBDD.SxS.Selenium.WebElement;
using ImageMagick;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using TechTalk.SpecFlow;

namespace Automated_E2E_Testing_Workshop.Specs
{
    [Binding]
    public sealed class ComponentsSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IWebDriver _driver;
        private ReadOnlyCollection<IWebElement> _components;

        public ComponentsSteps(IWebDriver driver)
        {
            _driver = driver;
        }

        [When(@"I open the component page")]
        public void GivenIOpenTheComponentPage()
        {
            _driver.Navigate().GoToUrl("https://www.swisslife.ch/de/autom_testing/komponenten-alle.html#Widget");
            Thread.Sleep(1000);
            _driver.Navigate().Refresh();
            Thread.Sleep(1000);
            _driver.Navigate().Refresh();
            Thread.Sleep(1000);
        }

        [When(@"I collect all components")]
        public void WhenICollectAllComponents()
        {
            _components = _driver.FindElements(By.XPath("//*[@id='a11y-main']/div/div"));
            Actions actionProvider = new Actions(_driver);
            actionProvider.MoveToElement(_components[0]).Build().Perform();
            actionProvider.MoveByOffset(-(_components[0].Size.Width / 2), 0).Build().Perform();
        }

        [When(@"I remove the nav header")]
        public void WhenIRemoveTheNavHeader()
        {
            _driver.ExecuteJavaScript("document.getElementsByTagName('header')[0].remove();");
        }

        [Then(@"Should found (.*) components")]
        public void ThenShouldFoundComponents(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _components.Count);
        }

        [Then(@"All components should correspond to the basic image")]
        public void ThenAllComponentsShouldCorrespondToTheBasicImage()
        {
            var baseFilePath = "c:/temp/SwissLife-UITesting";

            Directory.CreateDirectory("c:/temp");
            Directory.CreateDirectory(baseFilePath);
            var count = 0;
            var errorCount = 0;

            _components[^1].ScrollTo();
            Thread.Sleep(3000);

            foreach (var component in _components)
            {
                component.ScrollTo();
                Thread.Sleep(5000);

                using Bitmap newImage = _driver.GetElementScreenshot(component, false);
                count++;
                var baseFileName = $"{baseFilePath}/component_{count}";
                if (!File.Exists($"{baseFileName}.1_base.png"))
                {
                    newImage.Save($"{baseFileName}.1_base.png", ImageFormat.Png);

                }
                else
                {
                    using var baseImg = new MagickImage($"{baseFileName}.1_base.png");
                    using var newImg = new MagickImage(ImageToByte(newImage));
                    using var diffImg = new MagickImage();
                    baseImg.ColorFuzz = new Percentage(25);
                    var diff = baseImg.Compare(newImg, new CompareSettings { Metric = ErrorMetric.Absolute }, diffImg);

                    if (diff > 9000)
                    {
                        newImg.Write($"{baseFileName}.2_new.png");
                        diffImg.Write($"{baseFileName}.3_diff.png");
                        TestContext.AddTestAttachment($"{baseFileName}.1_base.png", "BaseImg");
                        TestContext.AddTestAttachment($"{baseFileName}.2_new.png", "NewBaseImg");
                        TestContext.AddTestAttachment($"{baseFileName}.3_diff.png", "DiffImg");
                        errorCount++;
                    }
                }
            }
            Assert.AreEqual(0, errorCount, "Es wurden {errorCount} visuelle Fehler gefunden.");
        }

        private static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
