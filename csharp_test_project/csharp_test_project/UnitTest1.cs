using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest
    {
        private ChromeDriver driver;
        private WebDriverWait wait;

        public ChromeDriver Driver { get => driver; set => driver = value; }

        [SetUp]
        public void start()
        {
            Driver = new ChromeDriver();
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void FirstTest()
        {
            Driver.Url = "http://www.google.com/";
            Driver.FindElement(By.Name("q")).SendKeys("webdriver");
            Driver.FindElement(By.Name("btnG")).Click();
        }

        [TearDown]
        public void stop()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}