using NUnit.Framework;
using NvendorPOM.PageMethod;
using OpenQA.Selenium;
using NvendorPOM.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework.Legacy;
using NvendorPOM.Config;

namespace NvendorPOM.TestCases
{
    [TestFixture]
    public class LoginTest : Configuration
    {
        string user = _configuration[Configuration.EnvConfig + ":Username"];
        string password = _configuration[Configuration.EnvConfig + ":Password"];
        string institutions = _configuration[Configuration.EnvConfig + ":Institution"];


        LoginPage loginPage;

        

        [Test, Order(1)]
        [Category("1 - Login")]
        public void test_Login()
        {
            loginPage = new LoginPage(GetDriver());
            loginPage.goToPage();
            loginPage.enterUserName(user);
            loginPage.enterPassword(password);
            loginPage.selectInstitution(institution);
            loginPage.selectProduct();
            ClassicAssert.IsTrue(loginPage.verifyProduct());
            //loginPage.closeBrowser();   
        }
    }
}
