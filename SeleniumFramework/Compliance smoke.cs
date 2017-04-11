using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Text;
using System.Web;


namespace SeleniumFramework
{
    [TestFixture]
    [Parallelizable]

    public class SmokeTest : DataClass

    {   //Define WebDriver Instance
        IWebDriver Driver;

        //Define and Initialize Extent Report instance for loggin results in HTML file
        ExtentReports TestReport = new ExtentReports("C:\\Users\\mumair\\Documents\\Visual Studio 2015\\Test Report\\SmokeTestCompliance.html");
        
        [SetUp]
        public void OpenBrowser()

        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }

   //     [Test, TestCaseSource("DataReg")]
        [Test]
        public void ComplianceSmoke()

        {   
            //Define a variable for the test script and description
            var ComplianceSmokeTestReport = TestReport.StartTest("Compliance Smoke Test", "This test will perform a high level smoke on Compliance Module");

            Driver.Url = "https://qa.fransupport.com/new/corporate/login.aspx";

            //One method of using a control
            Driver.FindElement(By.Id("txtUserName")).SendKeys("mumair@broadpeakit.com"); ;
            
            //Second method of using a control
            IWebElement PasswordField = Driver.FindElement(By.Id("txtPassword"));
            PasswordField.SendKeys("admin1953");

            Driver.FindElement(By.Id("btnSignIn")).Click();

            //Handle if there are any communications present
            try

            {   
                //Click Acknowledge button if exists
                Driver.FindElement(By.Id("btnAck")).Click();

                //Print that sign in step has passed
                ComplianceSmokeTestReport.Log(LogStatus.Pass, "Signed in Successfully");

                //Look if there are multiplt messages
                int MultipleMessages = Driver.FindElements(By.Id("btnAck")).Count();
               
                while (MultipleMessages > 0)
                {
                    Driver.FindElement(By.Id("btnAck")).Click();
                    MultipleMessages = Driver.FindElements(By.Id("btnAck")).Count();
                }

                //Print that communication(s) have been acknowledged.
                ComplianceSmokeTestReport.Log(LogStatus.Pass, "Communications Acknowledged");
                //select ncompass app
                Driver.FindElement(By.Id("liNcompass")).Click();

            }

            catch (Exception)

            {
                //select ncompass app
                Driver.FindElement(By.Id("liNcompass")).Click();

                //Print that sign in step has passed
                ComplianceSmokeTestReport.Log(LogStatus.Pass, "Signed in Successfully");
            }

            //select compliance module
            Driver.FindElement(By.Id("ctl00_leftPanel_liCompliance")).Click();
            System.Threading.Thread.Sleep(3000);

            //Acknowledge Location Alet popup
            try
            {   
                //Wait untill the button appears
                WebDriverWait Wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 10));
                Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("btnacknowledge")));
                System.Threading.Thread.Sleep(2000);
                Driver.FindElement(By.Id("btnacknowledge")).Click();
                ComplianceSmokeTestReport.Log(LogStatus.Pass, "Location Alert Popup Acknowledged");
            }

            catch(Exception)
            {
                ComplianceSmokeTestReport.Log(LogStatus.Fail, "Location Alert Popup did not Appear");
            }

                   
               //Click Add New Link in the center widgets
               System.Threading.Thread.Sleep(5000);
               Driver.FindElement(By.XPath("//*[@id='middle']/div[1]/div[2]/span/a[1]")).Click();

     try
               {   
                //Wait for the Div to appear
                 WebDriverWait WaitforWidgetDiv = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
                 WaitforWidgetDiv.Until(ExpectedConditions.ElementIsVisible(By.ClassName("WidgetBackgound")));

                //Loop to Add All Widgets
                 int RemainingWidgetCount = Driver.FindElements(By.XPath("//*[@id='dv-popup-content']/div[1]/div[1]/span/input")).Count();
                 while (RemainingWidgetCount > 0)
                 {
                   Driver.FindElement(By.XPath("//*[@id='dv-popup-content']/div[1]/div[1]/span/input")).Click();
                   WaitforWidgetDiv.Until(ExpectedConditions.ElementIsVisible(By.Id("divPopup_Alert")));
                   Driver.FindElement(By.Id("BtnCancel_Alert")).Click();
                   RemainingWidgetCount = Driver.FindElements(By.XPath("//*[@id='dv-popup-content']/div[1]/div[1]/span/input")).Count();
                   }

                  Driver.FindElement(By.Id("btnCancel")).Click();
                  ComplianceSmokeTestReport.Log(LogStatus.Pass, "All Widgets Added Successfully");
                  }

     catch(Exception)
                {
                   Driver.FindElement(By.Id("btnCancel_InfoBox")).Click();
                   ComplianceSmokeTestReport.Log(LogStatus.Info, "There was no widget to Add");
                      }

                   //Deleting the Widgets
    try
              {
                Driver.FindElement(By.XPath("//*[@id='middle']/div[1]/div[2]/span/a[2]")).Click();
                //Wait for the Div to appear
                WebDriverWait Wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
                Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("EditRightPanelDiv")));

               //Loop to Delete All Widgets
               int RemainingWidgetCount = Driver.FindElements(By.XPath("//*[@id='EditRightPanelDiv']/div[1]/div[3]/img")).Count();

              while (RemainingWidgetCount > 0)
               {
                 Driver.FindElement(By.XPath("//*[@id='EditRightPanelDiv']/div[1]/div[3]/img")).Click();
                 RemainingWidgetCount = Driver.FindElements(By.XPath("//*[@id='EditRightPanelDiv']/div[1]/div[3]/img")).Count();
                   }

                Driver.FindElement(By.Id("btnUpdateAll")).Click();
                ComplianceSmokeTestReport.Log(LogStatus.Pass, "All Widgets Deleted Successfully");
                   }

     catch(Exception)
              { ComplianceSmokeTestReport.Log(LogStatus.Info, "There was no widget to Delete");}

                  System.Threading.Thread.Sleep(3000);
                  Driver.FindElement(By.Id("liLocationListFranMgmt")).Click();
                  Driver.FindElement(By.Id("ctl00_cntMain_txtSearch")).SendKeys(Keys.NumberPad5+Keys.NumberPad5+Keys.NumberPad5+Keys.Enter);
                  System.Threading.Thread.Sleep(7000);
       try
              {
                 Driver.FindElement(By.Id("ctl00_cntMain_lstLocation_ctrl0_lbllocationname")).Click();

                 System.Threading.Thread.Sleep(3000);
                 Driver.FindElement(By.Id("spnEdit")).Click();

                 IWebElement SaveButton = Driver.FindElement(By.XPath("//*[@id='form1']/div[5]/div[33]/div/input[1]"));
                 Actions Scroll = new Actions(Driver);
                 Scroll.MoveToElement(SaveButton).Build().Perform();
                 SaveButton.Click();

                 System.Threading.Thread.Sleep(3000);
                 IWebElement Ownership = Driver.FindElement(By.LinkText("Ownership"));
                 Scroll.MoveToElement(Ownership).Build().Perform();
                 Ownership.Click();

                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Lease")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Managers")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Pools")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Vehicle Information")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Flag/Alerts")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Login Access")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Financial Information")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Notes")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Photo Gallery")).Click();
                 System.Threading.Thread.Sleep(2000);
                 Driver.FindElement(By.LinkText("Documents")).Click();
                 System.Threading.Thread.Sleep(3000);
                 Driver.FindElement(By.LinkText("History Log")).Click();
                 System.Threading.Thread.Sleep(2000);

                 WebDriverWait WaitForDemographics = new WebDriverWait(Driver, new TimeSpan(0, 0, 10));
                 WaitForDemographics.Until(ExpectedConditions.ElementExists(By.LinkText("Demographics")));
                 IWebElement Demographics = Driver.FindElement(By.LinkText("Demographics"));
                 Scroll.MoveToElement(Demographics).Build().Perform();
                 Demographics.Click();

                 ComplianceSmokeTestReport.Log(LogStatus.Pass, "All Location Info Links are Working");

                  }

            catch (Exception)

             { ComplianceSmokeTestReport.Log(LogStatus.Fail, "Some Location Info Links were not Working"); }

              //Going to Reports Section
              System.Threading.Thread.Sleep(5000);
              Driver.FindElement(By.Id("liReportFranMgmt")).Click();

               try
                 { 
                           System.Threading.Thread.Sleep(3000);
                           IWebElement ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           System.Threading.Thread.Sleep(2000);
                           WebDriverWait WaitForVisibility = new WebDriverWait(Driver, new TimeSpan(0, 0, 10));
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           Actions Scroll = new Actions(Driver);
                           Scroll.MoveToElement(ExportToExcel).Build().Perform();
                           ExportToExcel.Click();

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Location List by Status")));
                           Driver.FindElement(By.LinkText("Location List by Status")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='dvStoreListByStatus']/table/tbody/tr[3]/td[2]/input")));
                           IWebElement ExportToExcelButton = Driver.FindElement(By.XPath("//*[@id='dvStoreListByStatus']/table/tbody/tr[3]/td[2]/input"));
                           Scroll.MoveToElement(ExportToExcelButton).Build().Perform();
                           ExportToExcelButton.Click();
                           System.Threading.Thread.Sleep(2000);

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Owners Deleted")));
                           Driver.FindElement(By.LinkText("Owners Deleted")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           Scroll.MoveToElement(ExportToExcel).Build().Perform();
                           ExportToExcel.Click();
                           System.Threading.Thread.Sleep(2000);

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Location Status History Report")));
                           Driver.FindElement(By.LinkText("Location Status History Report")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           Scroll.MoveToElement(ExportToExcel).Build().Perform();
                           ExportToExcel.Click();
                           System.Threading.Thread.Sleep(2000);

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Transfer History Report")));
                           Driver.FindElement(By.LinkText("Transfer History Report")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           Scroll.MoveToElement(ExportToExcel).Build().Perform();
                           ExportToExcel.Click();
                           System.Threading.Thread.Sleep(2000);

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Multi-Unit Enterprise Report")));
                           Driver.FindElement(By.LinkText("Multi-Unit Enterprise Report")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           ExportToExcel.Click();
                           System.Threading.Thread.Sleep(2000);

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Multi-Unit Enterprise Revenue Report")));
                           Driver.FindElement(By.LinkText("Multi-Unit Enterprise Revenue Report")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           ExportToExcel.Click();
                           System.Threading.Thread.Sleep(2000);

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Single-Unit Enterprise Report")));
                           Driver.FindElement(By.LinkText("Single-Unit Enterprise Report")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           ExportToExcel.Click();
                           System.Threading.Thread.Sleep(2000);

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Owner List By Religion")));
                           Driver.FindElement(By.LinkText("Owner List By Religion")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           ExportToExcel.Click();
                           System.Threading.Thread.Sleep(2000);
/*
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Franchisee Areas Report")));
                           Driver.FindElement(By.LinkText("Franchisee Areas Report")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnPdf")));
                           IWebElement ExportToPDF = Driver.FindElement(By.Id("btnPdf"));
                           Scroll.MoveToElement(ExportToPDF).Build().Perform();
                           ExportToPDF.Click();
                           System.Threading.Thread.Sleep(2000);
*/
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("Demographics")));
                           Driver.FindElement(By.LinkText("Demographics")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.Id("btnExcel")));
                           ExportToExcel = Driver.FindElement(By.Id("btnExcel"));
                           Scroll.MoveToElement(ExportToExcel).Build().Perform();
                           ExportToExcel.Click();
                           System.Threading.Thread.Sleep(2000);

                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.LinkText("FDD Location List Report")));
                           Driver.FindElement(By.LinkText("FDD Location List Report")).Click();
                           System.Threading.Thread.Sleep(3000);
                           WaitForVisibility.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='dvFDDStoreList']/table/tbody/tr[3]/td/input")));
                           ExportToExcel = Driver.FindElement(By.XPath("//*[@id='dvFDDStoreList']/table/tbody/tr[3]/td/input"));
                           ExportToExcel.Click();

                          ComplianceSmokeTestReport.Log(LogStatus.Pass, "All Reports Generated Successfully");

                       }

               catch (Exception)

                  { ComplianceSmokeTestReport.Log(LogStatus.Fail, "Some Reports were not Generated"); }
           

            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("liFranmgmtZone")).Click();

            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("btnAddNew")).Click();

            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("ctl00_cntMain_spnBack")).Click();
            ComplianceSmokeTestReport.Log(LogStatus.Pass, "New Zone Button is Working");
            System.Threading.Thread.Sleep(2000);

            try
            {
                Driver.FindElement(By.XPath("//*[@id='ctl00_cntMain_pnlGroup']/table/tbody/tr[2]/td[3]/div/a[1]")).Click();
                System.Threading.Thread.Sleep(2000);
                Driver.FindElement(By.Id("ctl00_cntMain_spnBack")).Click();
                ComplianceSmokeTestReport.Log(LogStatus.Pass, "Zone Edit is Working");
            }
            catch(Exception)
            { ComplianceSmokeTestReport.Log(LogStatus.Info, "No Zones Exist"); }

            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("liTermAndCondition")).Click();

            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("ctl00_cntMain_dvEdit")).Click();

            System.Threading.Thread.Sleep(2000);
            Driver.FindElement(By.Id("Span1")).Click();
            ComplianceSmokeTestReport.Log(LogStatus.Pass, "Term and Conditions link working");
            System.Threading.Thread.Sleep(2000);

            try
            {
                Driver.FindElement(By.Id("ctl00_cntMain_dvEdit")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("ctl00_leftPanel_liEuEnrollments")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.XPath("//*[@id='ctl00_cntMain_ddlSession']/option[4]")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.XPath("//*[@id='ctl00_cntMain_ddlStatus']/option[3]")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.XPath("//*[@id='ctl00_cntMain_pnlGroup']/div[1]/div[2]/div/span[4]/input")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("spnEnrollmentSessions")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("spnArchived")).Click();
                ComplianceSmokeTestReport.Log(LogStatus.Pass, "EU Enrollment Links are Working");
                System.Threading.Thread.Sleep(2000);
            }

            catch
            { ComplianceSmokeTestReport.Log(LogStatus.Pass, "Non-EA Clients do not have EU Enrollment"); }

            try
            { Driver.FindElement(By.Id("ctl00_leftPanel_liSemEnrollments")).Click();
              ComplianceSmokeTestReport.Log(LogStatus.Pass, "SEM Links Verified");
            }
            catch(Exception)
            { ComplianceSmokeTestReport.Log(LogStatus.Info, "Non-EA Clients do not have SEM"); }

                System.Threading.Thread.Sleep(2000);
                Driver.FindElement(By.Id("liMoreFranMgmt")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("spnCompanies")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("spnMaster")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("spnBS")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("spnEO")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("spnRB")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("spnTemplates")).Click();
                System.Threading.Thread.Sleep(2000);

                Driver.FindElement(By.Id("ctl00_leftPanel_liCompliance")).Click();
                System.Threading.Thread.Sleep(2000);

                ComplianceSmokeTestReport.Log(LogStatus.Pass, "Test Case Passed");
                TestReport.EndTest(ComplianceSmokeTestReport);
        }

       [TearDown]
       public void CloseBrowser()
      {
            Driver.Quit();
            TestReport.Flush();
       }
    }
}
