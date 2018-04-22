using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    public class LoginTest
    {
   
    public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
        driver = new ChromeDriver();
        }

        [Test]
        public void TestLogin()
        {
        driver.Url = "http://localhost/litecart/admin";
        driver.FindElement(By.Name("username")).SendKeys("admin");
        driver.FindElement(By.Name("password")).SendKeys("admin");
        driver.FindElement(By.Name("login")).Click();
        driver.FindElement(By.ClassName("fa-sign-out")).Click();
        }
    
        [TearDown]
        public void Stop()
        {
        driver.Quit();
        driver = null;
        }
    }
}

