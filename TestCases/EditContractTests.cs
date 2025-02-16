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
using NvendorPOM.PageMethod.Nvendor;


namespace NvendorPOM.TestCases
{
    [TestFixture]
    internal class EditContractTests : Configuration
    {

        LoginPage loginPage;
        ContractManager contractManager;


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

        [Test, Category("Edit Contract"), Order(2)]
        public void Test_EditContractName()
        {
            contractManager = new ContractManager(GetDriver());
            contractManager.goToContracts();
            contractManager.EditContractName();
            contractManager.verifyContractNameEdit();
        }

        //[Test, Category("Edit Contract"), Order(3)]
        //public void Test_EditContractDescription()
        //{
        //    contractManager = new ContractManager(GetDriver());
        //   var results = contractManager.goToAIPanel();
        //    if (results == true)
        //    {
        //        Assert.Pass("We did it!");
        //    }
        //}
    }
}
