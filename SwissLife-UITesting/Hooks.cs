using APOM.Pages;
using BoDi;
using FunkyBDD.SxS.Selenium.Browserstack;
using FunkyBDD.SxS.Selenium.WebDriver;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SwissLife.SxS.Helpers;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace Automated_E2E_Testing_Workshop
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IWebDriver _driver;
        private Homepage _homepage;
        private readonly string _baseURL;
        private static string _UUID;
        public static string TestPath;
        public static string BasePath;
        private static int _testTotalCount = 0;
        private static int _testFailCount = 0;
        private static DataTable _table = new DataTable("testTable");

        public IWebDriver Driver => _driver;

        public Hooks(IWebDriver driver)
        {
            _driver = driver;
            _baseURL = Config["BaseURL"].ToString();
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _UUID = Guid.NewGuid().ToString();
            BasePath = Config["BasePath"];
            TestPath = $"{BasePath}/{_UUID}";
            Directory.CreateDirectory(BasePath);
            Directory.CreateDirectory(TestPath);

            _table.Columns.Add(new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "feature"
            }); _table.Columns.Add(new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "scenario"
            });
            _table.Columns.Add(new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "status"
            });
            _table.Columns.Add(new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "meldung"
            });
        }

        [BeforeFeature]
        public static void BeforeFeature(ObjectContainer objectContainer)
        {

            /* initialize & register Browser and IWebDriver */
            Browser browser = Config["Browser"] == null ? new Browser("FirefoxLocal") : new Browser(Config["Browser"]);
            objectContainer.RegisterInstanceAs<IWebDriver>(browser.Driver);
            objectContainer.RegisterInstanceAs<Browser>(browser);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _testTotalCount++;
        }

        [Given(@"I open the Homepage"), Given(@"Die Homepage ist geöffnet")]
        public void IOpenTheTestPage()
        {
            _driver.Navigate().GoToUrl(_baseURL);
            _driver.ExecuteJavaScript("localStorage.setItem('survey_psyma_modal', 'closed')");
            _homepage = new Homepage(_driver);
        }

        [Given(@"I navigate to '(.*)'"), When(@"I navigate to '(.*)'")]
        public void GivenINavigateTo(string path)
        {
            _driver.NavigateToPath(path);
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
        public void AfterScenarion(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            if (_driver != null)
            {
                var row = _table.NewRow();
                row["feature"] = featureContext.FeatureInfo.Title;
                row["scenario"] = scenarioContext.ScenarioInfo.Title;

                if (scenarioContext.TestError != null)
                {
                    _testFailCount++;
                    row["status"] = "NOK";
                    row["meldung"] = scenarioContext.TestError.Message;
                    var screenShotFileName = $"{featureContext.FeatureInfo.Title}__{scenarioContext.ScenarioInfo.Title}.png";
                    screenShotFileName = FileHelpers.RemoveIllegalFileNameChars(screenShotFileName);
                    _driver.MakeScreenshot(screenShotFileName, TestPath);
                }
                else
                {
                    row["status"] = "OK";
                    row["meldung"] = scenarioContext.ScenarioInfo.Arguments.ToProperString();
                }

                _table.Rows.Add(row);

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

        [AfterTestRun]
        public static void SendReport()
        {
            var date = DateTime.Now.ToLongDateString();
            var time = DateTime.Now.ToLongTimeString();

            var mailBody = File.ReadAllText("MailBody.html");
            var sb = new StringBuilder(mailBody);
            sb.Replace("{TotalCount}", _testTotalCount.ToString());
            sb.Replace("{FailCount}", _testFailCount.ToString());
            sb.Replace("{SuccessCount}", (_testTotalCount - _testFailCount).ToString());
            sb.Replace("{SuccessPercent}", (100 - ((double)_testFailCount / (double)_testTotalCount * 100)).ToString());
            sb.Replace("{FailPercent}", ((double)_testFailCount / (double)_testTotalCount * 100).ToString());
            sb.Replace("{Date}", date);
            sb.Replace("{Time}", time);

            string rowString = "";
            foreach (DataRow row in _table.Rows)
            {
                var sbRow = new StringBuilder(@"
                <tr>
                    <td>{feature}</td>
                    <td>{scenario}</td>
                    <td><span class=""badge bg-{state}"">{status}</span></td>
                    <td>{meldung}</td>
                </tr > 
                ");

                sbRow.Replace("{feature}", row["feature"].ToString());
                sbRow.Replace("{scenario}", row["scenario"].ToString());
                sbRow.Replace("{status}", row["status"].ToString());
                sbRow.Replace("{meldung}", row["meldung"].ToString());
                sbRow.Replace("{state}", (row["status"].ToString() == "OK") ? "success" : "danger");
                rowString += sbRow.ToString();
            }

            sb.Replace("{TableRows}", rowString);

            var config = Config;

            var smtpClient = new SmtpClient(config["Smtp:Host"])
            {
                Port = int.Parse(config["Smtp:Port"]),
                Credentials = new NetworkCredential(config["Smtp:Username"], config["Smtp:Password"]),
                EnableSsl = bool.Parse(config["Smtp:Ssl"]),
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("sl.selenium.test@gmail.com"),
                Subject = $"Testresultat vom {date}",
                Body = sb.ToString(),
                IsBodyHtml = true,
            };


            string[] files = Directory.GetFiles(TestPath, "*.png", SearchOption.TopDirectoryOnly);
            if (files.Length > 0)
            {
                foreach (var item in files)
                {
                    var attachment = new Attachment(item);
                    mailMessage.Attachments.Add(attachment);

                }
            }

            mailMessage.To.Add("andreas.karz@swisslife.ch");

            smtpClient.Send(mailMessage);

            Directory.Delete(TestPath);
        }

        public static IConfiguration Config => new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile("appsettings.user.json", optional: true)
                    .AddEnvironmentVariables(prefix: "E2E_")
                    .Build();

    }
}
