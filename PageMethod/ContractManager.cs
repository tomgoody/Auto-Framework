using Microsoft.Extensions.Configuration;
using vendorPOM.Config;
using OpenQA.Selenium;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using AventStack.ExtentReports;
//using Assert = NUnit.Framework.Assert;


//Thread.Sleep was used in place of waiting for page loads because they were more reliable.
namespace vendorPOM.PageMethod
{
    class ContractManager
    {

        static ExtentTest test;

        //Variables
        string ContractNameAbbr = _configuration[Configuration.EnvConfig + ":ContractNameAbbr"];
        string AgreementType = _configuration[Configuration.EnvConfig + ":AgreementType"];
        string Vendor = _configuration[Configuration.EnvConfig + ":Vendor"];
        string ContractType = _configuration[Configuration.EnvConfig + ":ContractType"];
        string thisDay = Configuration.currentDate.ToString();


        //Contracts Manager Add Contracts Elements
        string contractLeftNav = "[full-name='vendormanager.contractmanager']";
        string addContractBtn = "//button[text()='Add Contracts']";
        string browseBtn = "[class='browse']";
        string fileUpload = "input[type = 'file']";
        string uploadCheckmark = "[alt='checkmark']"; 
        string nextBtn = "//button[text()=' Next Step ']";
        string contractName = "[data-automation='custom_name_input']";
        string agreement = "react-select-2-input";
        string vendorDropdown = "react-select-3-input";
        string cType = "react-select-4-input";
        string contractSave = "//button[text()='Save']";
        string searchBar = "[placeholder='Search']";
        string contractNameLabel = "[class='text-amber-800']";

        //Contracts Manager Contract's Tabs
        string details = "[class='Details']";
        string relatedContracts = "[class='Related Contracts']";
        string notifications = "[class='Notifications']";
        string scorecard = "[class='Scorecard']";

        //Contracts Manager Edit Contract Name Elements
        string contractSummaryEdit = "[alt='edit']";
        string contractNameEdit = "[placeholder='Provide a contract name']";
        string nameAppend = " Edited";
        string editSummarySave = "//button[text()='Save']";
        string returnToGrid = "[data-automation='return_to_contracts_link']";
        string clearSearch = "[alt='Clear Search']";

        //Contracts Manager AI Generator
        string aiPanelButton = "[data-automation='ai_panel_button']";
        string overview = "[class='Overview']";
        string useAiSummary = "//button[text()='Use AI Summary']";
        string terms = "[class='Terms']";
        string extractTerms = "//button[text()='Extract Terms']";
        string backToAiPanel = "[data-automation='back_to_main_ai']";
        string guidanceSummaries = "[class='Guidance Summaries']";
        string generateSummaries = "//button[text()='Generate Summaries']";
        string completed = "[class='Completed']";
        string inProgress = "[class='In Progress']";
        string notStarted = "[class='Not Started']";

        private IWebDriver driver;
        private static IConfiguration _configuration;



        static ContractManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public string ContractName()
        {
            string newContract = thisDay + ContractNameAbbr;
            return newContract;
        }



        public ContractManager(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void goToContracts()
        {
            try
            {
                IWebElement leftNav = driver.FindElement(By.CssSelector(contractLeftNav));
                leftNav.Click();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                test.Fail(ex);
            }
            
        }

        public void uploadContract()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.ClassName("iframeit")));
            IWebElement AddContract = driver.FindElement(By.XPath(addContractBtn));
            AddContract.Click();
            Thread.Sleep(3000);

            //IWebElement browseContract = driver.FindElement(By.CssSelector(browseBtn));
            //browseContract.Click();
            Thread.Sleep(2000);

            // Execute the AutoIt script to handle the file dialog
            //Process process = new Process();
            //process.StartInfo.FileName = @"Contracts\contractUpload.exe";
            //process.Start();


            //// Wait for the process to complete
            //process.WaitForExit();

            IWebElement fileInput = driver.FindElement(By.CssSelector(fileUpload));

            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            var contractPath = projectPath + "Contracts\\182.docx";

            fileInput.SendKeys(contractPath.ToString());
            Thread.Sleep(3000);
            Assert.That(driver.FindElement(By.CssSelector(uploadCheckmark)).Enabled);

            //IWebElement uploadSuccess = driver.FindElement(By.CssSelector(uploadCheckmark));
            //if (uploadSuccess.Enabled)
            //{
            //    test.Log(Status.Info, contractPath + " contract upload successful");
            //}

            IWebElement nextButton = driver.FindElement(By.XPath(nextBtn));
            if (nextButton.Enabled == false)
            {
                Thread.Sleep(5000);
            }
            else
            {
                nextButton.Click();
                Thread.Sleep(3000);
            }
        }

        public void changeContractName()
        {
            //Change Contract Name
            string newContract = ContractName();
            IWebElement newContractName = driver.FindElement(By.CssSelector(contractName));
            newContractName.Click();
            newContractName.SendKeys(Keys.Control + "a");
            newContractName.SendKeys(newContract);
            Thread.Sleep(1000);

        }

        public void setAgreementType()
        {
            //Set Agreement Type
            IWebElement agreementType = driver.FindElement(By.Id(agreement));
            agreementType.Click();
            Thread.Sleep(1000);
            agreementType.SendKeys(AgreementType);
            Thread.Sleep(1000);
            agreementType.SendKeys(Keys.ArrowDown);
            agreementType.SendKeys(Keys.Enter);
        }

        public void setVendor()
        {
            //Set Vendor
            IWebElement vendor = driver.FindElement(By.Id(vendorDropdown));
            vendor.Click();
            Thread.Sleep(1000);
            vendor.SendKeys(Vendor);
            Thread.Sleep(1000);
            vendor.SendKeys(Keys.ArrowDown);
            vendor.SendKeys(Keys.Enter);
        }

        public void setContractType()
        {
            //Set Contract Type
            IWebElement contractType = driver.FindElement(By.Id(cType));
            contractType.Click();
            Thread.Sleep(1000);
            contractType.SendKeys(ContractType);
            Thread.Sleep(1000);
            contractType.SendKeys(Keys.ArrowDown);
            contractType.SendKeys(Keys.Enter);
        }

        public void nextSaveButtons()
        {
            IWebElement nextButton = driver.FindElement(By.XPath(nextBtn));
            //click Next Step and Save
            nextButton.Click();

            IWebElement save = driver.FindElement(By.XPath(contractSave));
            save.Click();
            Thread.Sleep(2000);
        }

        public void searchContractUpload()
        {
            //Search for added contract
            string newContract = ContractName();
            IWebElement search = driver.FindElement(By.CssSelector(searchBar));
            search.Click();
            search.SendKeys(newContract);

        }

        public bool VerifyContractUpload()
        {
            string newContract = ContractName();
            IWebElement nameLabel = driver.FindElement(By.CssSelector(contractNameLabel));
             string uploadName = nameLabel.Text;
            bool results = uploadName == newContract;
            return results;
            
            
        }
            public void EditContractName()
        {
            //Search for Contract
            string newContract = ContractName();
            driver.SwitchTo().Frame(driver.FindElement(By.ClassName("iframeit")));
            IWebElement search = driver.FindElement(By.CssSelector(searchBar));
            search.Click();
            //IWebElement clear = driver.FindElement(By.CssSelector(clearSearch));
            //clear.Click();
            search.SendKeys(newContract);

            IWebElement contractLabel = driver.FindElement(By.CssSelector(contractNameLabel));
            String uploadName = contractLabel.Text;
            contractLabel.Click();
            Thread.Sleep(3000);

            IWebElement edit = driver.FindElement(By.CssSelector(contractSummaryEdit));
            edit.Click();
            Thread.Sleep(2000);

            IWebElement nameEdit = driver.FindElement(By.CssSelector(contractNameEdit));
            nameEdit.Click();
            nameEdit.SendKeys(nameAppend);

            IWebElement editSave = driver.FindElement(By.XPath(editSummarySave));
            editSave.Click();
            Thread.Sleep(2000);

            IWebElement goBack = driver.FindElement(By.CssSelector(returnToGrid));
            goBack.Click();
        }
            public Boolean verifyContractNameEdit()
            {
            string newContractName = ContractName();
            IWebElement addContract = driver.FindElement(By.XPath(addContractBtn));
            string searchText = newContractName + nameAppend;
            if (addContract.Enabled == false)
            {
                Thread.Sleep(5000);
            }
            else
            {
                IWebElement searchEdit = driver.FindElement(By.CssSelector(searchBar));
                searchEdit.Click();
                Thread.Sleep(2000);
                IWebElement clear = driver.FindElement(By.CssSelector(clearSearch));
                clear.Click();
                searchEdit.SendKeys(searchText);
                Thread.Sleep(3000);
            }

            IWebElement contractLabel1 = driver.FindElement(By.CssSelector(contractNameLabel));
            string edits = contractLabel1.Text;
            bool result = (edits == searchText);
            return result;

           // Assert.AreEqual(edits, searchText);

        }

        public Boolean goToAIPanel()
        {
            return true;

        }
    }
    
}
