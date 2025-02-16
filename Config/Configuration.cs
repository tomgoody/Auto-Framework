using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using TestContext = NUnit.Framework.TestContext;

namespace vendorPOM.Config
{
    [SetUpFixture]
    public abstract class Configuration
    {
        public static string EnvConfig = "F-settings";

        string environment = _configuration[Configuration.EnvConfig + ":Environment"];
        string user = _configuration[Configuration.EnvConfig + ":Username"];

        public static DateTime currentDate = DateTime.Now;
        static ExtentReports extent;
        static ExtentTest test;
        public IWebDriver _driver;
        private static IConfiguration _configuration;

        static Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        [OneTimeSetUp]
        protected void OneTimeSetup()
        {
            extent = ExtentManager.getInstance();

            //Set Chrome options Add "--headless" to empty options.AddArgument(); to run in background
            ChromeOptions options = new ChromeOptions();
            options.AddArguments();
            options.AddArguments();

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            _driver = new ChromeDriver(service, options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            _driver.Manage().Window.Maximize();

        }

        [SetUp]
        public void Setup()
        {
            extent = ExtentManager.getInstance();

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            // Add categories to the ExtentTest
            var categories = TestContext.CurrentContext.Test.Properties["Category"];
            foreach (var category in categories)
            {
                test.AssignCategory(category.ToString());
            }


        }

        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            extent.AddSystemInfo("Environment", environment);
            extent.AddSystemInfo("UserName", user);
            _driver.Quit(); 
        }


        [TearDown]

    public void AfterTest()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? " " : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
        Status logstatus;
        switch (status)
        {
            case TestStatus.Failed:
                logstatus = Status.Fail;
                DateTime time = DateTime.Now;
                //string fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
                //string screenShotPath = Capture(_driver, fileName);
                test.Log(Status.Fail, "Fail");
                //_test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath("Screenshots\\" + fileName));

                break;
            case TestStatus.Inconclusive:
                logstatus = Status.Warning;
                break;
            case TestStatus.Skipped:
                logstatus = Status.Skip;
                break;
            default:
                logstatus = Status.Pass;
                break;
        }
        test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
        extent.Flush();
        //_driver.Quit();

        }
        public IWebDriver GetDriver()
        {
            return _driver;
        }

        //public static string Capture(IWebDriver driver, string screenShotName)
        //{
        //    ITakesScreenshot ts = (ITakesScreenshot)driver;
        //    Screenshot screenshot = ts.GetScreenshot();
        //    string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
        //    string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "ErrorScreenshots\\" + screenShotName + ".png";
        //    string localpath = new Uri(finalpth).LocalPath;
        //    screenshot.SaveAsFile(localpath);
        //    return localpath;

        //}

    }
}
