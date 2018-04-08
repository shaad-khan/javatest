using System;
using System.IO;
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
        private string baseURL = "https://www.netflix.com/in/";
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
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl(this.baseURL);
            tagName = driver.FindElement(By.ClassName("cta-pitch")).Text;
            Assert.IsTrue(tagName.Contains("Post published1."), tagName + " doesn't contains 'Post published1.'");
            //do other Selenium things here!
            //write(tagName);
         //   Console.WriteLine(tagName);
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
                this.baseURL = "https://www.netflix.com/in/"; //default URL just to get started with
            }
        }
    }
}
        


    