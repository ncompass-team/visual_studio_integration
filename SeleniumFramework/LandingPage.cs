using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using RelevantCodes.ExtentReports;

namespace SeleniumFramework
{
    [TestFixture]
    [Parallelizable]
    
    public class LandingPage
    {
        IWebDriver Driver;
        ExtentReports TestReport = new ExtentReports("C:\\Users\\mumair\\Documents\\Visual Studio 2015\\Test Report\\Test Results.html");

        [SetUp]
        public void OpenBrowser()

        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }
       
        [Test]
        public void CorporateSignin()

        {
            var CorporateSigninReport = TestReport.StartTest("Corporate Sign in", "This test will sign in and take user to the corporate dashboard");
            
            Driver.Url="https://qa.fransupport.com/new/corporate/login.aspx";
            
            IWebElement SigninField = Driver.FindElement(By.Id("txtUserName"));
            SigninField.SendKeys("mumair@broadpeakit.com");

            IWebElement PasswordField = Driver.FindElement(By.Id("txtPassword"));
            PasswordField.SendKeys("admin1953");
            
            IWebElement SigninButton = Driver.FindElement(By.Id("btnSignIn"));
            SigninButton.Click();
            
            //     System.Diagnostics.Debugger.Launch();

            int CommunicationPresent = Driver.FindElements(By.Id("btnAck")).Count();

            if (CommunicationPresent == 1)
             {
                Driver.FindElement(By.Id("btnAck")).Click();
                int MultipleMessages = Driver.FindElements(By.Id("btnAck")).Count();
                CorporateSigninReport.Log(LogStatus.Pass, "Communications Acknowledged");
                while (MultipleMessages > 0)
                {
                    Driver.FindElement(By.Id("btnAck")).Click();
                    MultipleMessages = Driver.FindElements(By.Id("btnAck")).Count();
                }
               }

            CorporateSigninReport.Log(LogStatus.Pass, "Signed in Successfully");
            IWebElement ncomassLink = Driver.FindElement(By.Id("liNcompass"));
                ncomassLink.Click();

            CorporateSigninReport.Log(LogStatus.Pass, "Test case has passed");
            TestReport.EndTest(CorporateSigninReport);

            //new tab code
            //     Driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
            //     string W = Driver.WindowHandles.Last();
            //    Driver.SwitchTo().Window(W);
            //    Driver.Navigate().GoToUrl("https://corporate.naranga.com/ncompass/corporate/login.aspx");

           
           //This is 3 line code for browsing and uploading Image
           // System.Threading.Thread.Sleep(1000);
           // driver.FindElement(By.ClassName("btnUpload")).Click();
           //   SendKeys.SendWait(@"D:\Ibrahim\Pictures\image1.JPG");
           //   SendKeys.SendWait(@"{Enter}");

            //This is a single line code for uploading image
            //driver.FindElement(By.Id("fileUpload")).SendKeys("D:\\Ibrahim\\Pictures\\image1.jpg");

        }

    [Test]
      //  [Ignore ("Ignore this test")]
        public void LocationSignin()

        {
            var LocationSigninReport = TestReport.StartTest("Location Sign in", "This test will sign in and take user to the Location LIVEWALL");
            LocationSigninReport.Log(LogStatus.Info, "Status of the test case");

            Driver.Url="https://qa.fransupport.com/new/location/login.aspx";

            IWebElement SigninField = Driver.FindElement(By.Id("txtUserName"));
            SigninField.SendKeys("mumair@broadpeakit.com");

            IWebElement PasswordField = Driver.FindElement(By.Id("txtPassword"));
            PasswordField.SendKeys("admin1953");

            IWebElement SigninButton = Driver.FindElement(By.Id("btnSignIn"));
            SigninButton.Click();

            //     Handle if Communications are present
            int CommunicationPresent = Driver.FindElements(By.Id("btnAck")).Count();

            if (CommunicationPresent == 1)

            {
                Driver.FindElement(By.Id("btnAck")).Click();
                int MultipleMessages = Driver.FindElements(By.Id("btnAck")).Count();

                while (MultipleMessages > 0)
                {
                    Driver.FindElement(By.Id("btnAck")).Click();
                    MultipleMessages = Driver.FindElements(By.Id("btnAck")).Count();
                }
            }

            //Handle if Unverified e-mails popup is present
            int UnverifiedEmails = Driver.FindElements(By.Id("BtnCancel_Alert")).Count();

            if (UnverifiedEmails == 1)

            {
                Driver.FindElement(By.Id("btnAck")).Click();
                int MultipleEmails = Driver.FindElements(By.Id("BtnCancel_Alert")).Count();

                while (MultipleEmails > 0)
                {
                    Driver.FindElement(By.Id("btnAck")).Click();
                    MultipleEmails = Driver.FindElements(By.Id("BtnCancel_Alert")).Count();
                }

                TestReport.EndTest(LocationSigninReport);
                
            }


        }


        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
            TestReport.Flush();
        }

    }
}
