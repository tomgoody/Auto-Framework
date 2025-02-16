using NUnit.Framework;
using NvendorPOM.PageMethod;
using OpenQA.Selenium;
using NvendorPOM.Config;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Legacy;
using AventStack.ExtentReports;
using NvendorPOM.PageMethod.Nvendor;
//using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace NvendorPOM.TestCases
{
    [TestFixture]
    public class AddContractTest : Configuration
    {

        LoginPage loginPage;
        ContractManager contractManager;


        // The login could be cleaned up so that on a new test, all that needs to be called is one loginPage.login()
        [SetUp]
        public void Test_login()
        {
            loginPage = new LoginPage(GetDriver());
            loginPage.goToPage();
            loginPage.enterUserName();
            loginPage.enterPassword();
            loginPage.selectInstitution();
            loginPage.selectProduct();
            loginPage.verifyProduct();
        }

        [Test, Description("Add a Contract"), Order(1)]
        [Category("Add Contract")]
        public void Test_addContract()
        {
            contractManager = new ContractManager(GetDriver());
            contractManager.goToContracts();
            contractManager.uploadContract();
            contractManager.changeContractName();
            contractManager.setAgreementType();
            contractManager.setVendor();
            //contractManager.setContractType();
            contractManager.nextSaveButtons();
            contractManager.searchContractUpload();
            contractManager.VerifyContractUpload();
           //var results = contractManager.VerifyContractUpload();
            //Assert.That(results, Is.EqualTo(true));
        }

    }
}
