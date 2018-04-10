using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace selenium_test
{
    [TestClass]
    public class UnitTest1
    {
        private string baseURL = "https://admin.qa.atrius-iot.com/";
        private RemoteWebDriver driver;
        private string browser = string.Empty;
        public TestContext TestContext { get; set; }
        string tagName;
        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(1)]
        [Owner("Shaad")]
        public void TestMethod1()
        {
            /* driver.Manage().Window.Maximize();
             driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
             driver.Navigate().GoToUrl(this.baseURL);
             tagName = driver.FindElement(By.ClassName("cta-pitch")).Text;
             Assert.IsTrue(tagName.Contains("Post published1."), tagName + " doesn't contains 'Post published1.'");
             //do other Selenium things here!
             //write(tagName);
          //   Console.WriteLine(tagName);*/

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl(this.baseURL);
            driver.FindElement(By.XPath("//*[@id='i0116']")).SendKeys("testqa3@atgqa.onmicrosoft.com");
            driver.FindElement(By.Id("i0116")).SendKeys(Keys.Enter);
            Thread.Sleep(5000);
            driver.FindElement(By.Id("i0118")).SendKeys("April@2018");
            
            tagName = driver.FindElement(By.Id("i0118")).GetAttribute("value");

            if (tagName == "April@2018")
            {

                driver.FindElement(By.XPath("//*[@id='idSIButton9']")).Submit();
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//*[@id='idSIButton9']")).Submit();
            }
            Thread.Sleep(6000);
            //var sel= new SelectElement(driver.FindElement(By.Id("{put your tag information here}"))).SelectByText("{State here}");
            driver.FindElement(By.XPath("/html/body/compose[2]/div/div/form/div[1]/material-select/div/select")).FindElement(By.XPath("/html/body/compose[2]/div/div/form/div[1]/material-select/div/select/option[3]")).Click();

            tagName = driver.FindElement(By.XPath("/html/body/compose[2]/div/div/form/div[1]/material-select/div/select")).FindElement(By.XPath("/html/body/compose[2]/div/div/form/div[1]/material-select/div/select/option[3]")).Text;
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='save']")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='save']")).Click();
            Thread.Sleep(7000);
            driver.FindElement(By.XPath("/html/body/section/nav-bar/nav/ul/li[3]/div/a")).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("/html/body/section/div/router-view/section/div[1]/ul/li[2]/a")).Click();
            var query = driver.FindElements(By.XPath("/html/body/section/div/router-view/section/div[2]/router-view/div[1]/ul"));
            foreach (var element in query)
            {

                
                if (element.Text.Contains("Organization"))
                {
                    tagName = element.Text;
                    Console.WriteLine(element.Text);
                    
                }
                else if (element.Text.Contains("Partner"))
                {
                    Assert.Fail("Having Access to Partner link");

                }

            }
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(Environment.CurrentDirectory + @"/screenshot/ss.png", System.Drawing.Imaging.ImageFormat.Png);
                this.TestContext.AddResultFile(Environment.CurrentDirectory + @"/screenshot/ss.png");
            }
            catch(Exception ex)
            {

            }

        }

        private void write(string tagName)
        {
            Console.WriteLine(tagName);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            File.WriteAllText(Environment.CurrentDirectory+@"\result.txt", tagName);
            driver.Quit();
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            //Set the browswer from a build
            browser = this.TestContext.Properties["browser"] != null ? this.TestContext.Properties["browser"].ToString() : "chrome";
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                case "PhantomJS":
                    driver = new PhantomJSDriver();
                    break;
                default:
                    driver = new PhantomJSDriver();
                    break;
            }
            if (this.TestContext.Properties["Url"] != null) //Set URL from a build
            {
                this.baseURL = this.TestContext.Properties["Url"].ToString();
            }
            else
            {
                this.baseURL = "https://admin.qa.atrius-iot.com/"; //default URL just to get started with
            }
        }
    }
}
        


    