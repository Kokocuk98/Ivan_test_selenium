using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;

namespace csharp_example
{
    [TestFixture]
    public class TestNewPage
    {
        public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }
        [Test]
        public void NewPageTest()

        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
            //Заходим и логинимся
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //Переходим в меню создания товаров
            driver.FindElement(By.XPath("//*[@id='box-apps-menu']/li[3]")).Click();
            driver.FindElement(By.XPath("//*[@href='http://localhost/litecart/admin/?app=countries&doc=edit_country']")).Click();

            int MenuCount = driver.FindElements(By.XPath("//*[contains(@class, 'fa fa-external-link')]")).Count;
            ReadOnlyCollection<IWebElement> menu = driver.FindElements(By.XPath("//*[contains(@class, 'fa fa-external-link')]"));
            string mainWindow = driver.CurrentWindowHandle;
            for (int i = 0; i <= MenuCount - 1; i++)
            {
                menu[i].Click();
                driver.SwitchTo().Window(driver.WindowHandles[1]);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }



        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}