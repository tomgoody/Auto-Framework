using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using vendorPOM.Config;
using OpenQA.Selenium;


namespace vendorPOM.PageMethod
{
     class LoginPage
    {
        
        string userName = _configuration[Configuration.EnvConfig + ":Username"];
        string Password = _configuration[Configuration.EnvConfig + ":Password"];
        string URL = _configuration[Configuration.EnvConfig + ":Url"];
        string institutionURL = _configuration[Configuration.EnvConfig + ":InstitutionURL"];
        string username = "userName";
        string password = "password";
        string institutions = _configuration[Configuration.EnvConfig + ":Institution"];
        string appPicker = "[alt='Applications']";
        string product = "[alt='vendor']";
        string productLogo = "[full-name='vendormanager.contractmanager']";


        private IWebDriver driver;
        private static IConfiguration _configuration;

        static LoginPage()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void goToPage()
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void enterUserName()
        {
            IWebElement user = driver.FindElement(By.Id(username));
            user.SendKeys(userName);
            user.SendKeys(Keys.Return);
        }

        public void enterPassword()
        {
            IWebElement pword = driver.FindElement(By.Id(password));
            pword.SendKeys(Password);
            pword.SendKeys(Keys.Return);
            Thread.Sleep(5000);
        }

        public void selectInstitution()
        {
            string currentUrl = driver.Url;
            
            if (currentUrl == institutionURL)
            {
                IWebElement institution = driver.FindElement(By.CssSelector(institutions));
                institution.Click();
                Thread.Sleep(4000);
            }
        }

        public void selectProduct()
        {
            IWebElement picker = driver.FindElement(By.CssSelector(appPicker));
            picker.Click();
            Thread.Sleep(1000);

            IWebElement appSelection = driver.FindElement(By.CssSelector(product));
            appSelection.Click();
            Thread.Sleep(3000);

            //change focus to new window
            int windows = driver.WindowHandles.Count;
            if (windows == 2)
            {
                driver.SwitchTo().Window(driver.WindowHandles[1]);
            }
        }
        public bool verifyProduct()
        {
            Boolean result = driver.FindElement(By.CssSelector(productLogo)).Displayed;
            return result;
        }
        public void closeBrowser()
        {
            driver.Quit();
        }
    }
}
