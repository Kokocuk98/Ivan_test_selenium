using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class RegTest
    {
        public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }
        [Test]
        public void TestReg()

        {
            Random l = new Random();
            int num = l.Next(0, 1000);

            string i = "ivan_test" + num + "@test.com";

            driver.Url = "http://localhost/litecart/";

            driver.FindElement(By.XPath("//*[@href='http://localhost/litecart/en/create_account']")).Click();
            
            driver.FindElement(By.Name("tax_id")).SendKeys("12345");
            driver.FindElement(By.Name("company")).SendKeys("software-testing");
            driver.FindElement(By.Name("firstname")).SendKeys("Ivan");
            driver.FindElement(By.Name("lastname")).SendKeys("Ryabinin");
            driver.FindElement(By.Name("address1")).SendKeys("Russia");
            driver.FindElement(By.Name("address2")).SendKeys("NiNo");
            driver.FindElement(By.Name("postcode")).SendKeys("54321");
            driver.FindElement(By.Name("city")).SendKeys("Houston");            
            driver.FindElement(By.CssSelector("#create-account span[role = 'presentation']")).Click();
            driver.FindElement(By.CssSelector("body *> input[type='search']")).SendKeys("United States" + Keys.Enter);
            driver.FindElement(By.CssSelector("#create-account select[name = 'zone_code']")).Click();
            driver.FindElement(By.CssSelector("#create-account select[name = 'zone_code']")).SendKeys("Texas" + Keys.Enter);
            driver.FindElement(By.Name("email")).SendKeys(i);
            driver.FindElement(By.Name("phone")).SendKeys("123456789");
            driver.FindElement(By.Name("password")).SendKeys("1q2w3e4r");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("1q2w3e4r"); 
            driver.FindElement(By.Name("create_account")).Click();

            driver.FindElement(By.XPath("//*[@id='box-account']//li[4]/a")).Click();            
            driver.FindElement(By.XPath("//*[@id='box-account-login']//*[@name='email']")).SendKeys(i);
            driver.FindElement(By.XPath("//*[@id='box-account-login']//*[@name='password']")).SendKeys("1q2w3e4r");
            driver.FindElement(By.XPath("//*[@id='box-account-login']//*[@name='login']")).Click();
            driver.FindElement(By.XPath("//*[@id='box-account']//li[4]/a")).Click();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}