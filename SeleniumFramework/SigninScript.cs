using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections;
using System.Linq;

namespace SeleniumFramework
{
    [TestFixture]
    [Parallelizable]

   
    public class Signin : DataClass
    {   
        

        IWebDriver Driver;
        
        [SetUp]
        public void OpenBrowser()

        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
        }
        
        [Test]
        public void BroadpeakPortal()

        {
            Driver.Url = "https://broadpeak.naranga.com/ncompass/corporate/login.aspx";

                  Driver.FindElement(By.Id("txtUserName")).SendKeys("E-mail");

                  Driver.FindElement(By.Id("txtPassword")).SendKeys("Password");

                  Driver.FindElement(By.Id("btnSignIn")).Click();

                   //Handle Communications 
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

                   IWebElement ncomassLink = Driver.FindElement(By.Id("liNcompass"));
                   ncomassLink.Click();

            //Corporate.naranga.signin   

            Driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
            string W = Driver.WindowHandles.Last();
            Driver.SwitchTo().Window(W);
            Driver.Url = "https://corporate.naranga.com/ncompass/corporate/login.aspx";
            
            Driver.FindElement(By.Id("txtUserName")).SendKeys("E-mail");

            Driver.FindElement(By.Id("txtPassword")).SendKeys("Password");

            Driver.FindElement(By.Id("btnSignIn")).Click();

            //     Handle if Communications are present
            int CommunicationPresent1 = Driver.FindElements(By.Id("btnAck")).Count();

            if (CommunicationPresent1 == 1)

            {
                Driver.FindElement(By.Id("btnAck")).Click();
                int MultipleMessages = Driver.FindElements(By.Id("btnAck")).Count();

                while (MultipleMessages > 0)
                {
                    Driver.FindElement(By.Id("btnAck")).Click();
                    MultipleMessages = Driver.FindElements(By.Id("btnAck")).Count();
                }
            }

            IWebElement ncomassLink1 = Driver.FindElement(By.Id("liNcompass"));
            ncomassLink1.Click();

        }


        }
    
    }
