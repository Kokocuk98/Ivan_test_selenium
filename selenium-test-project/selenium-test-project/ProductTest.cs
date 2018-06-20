using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.IO;
namespace csharp_example
{
    [TestFixture]
    public class TestProduct
    {
        public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }
        [Test]
        public void ProductCreate()

        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string spath = "../../../../selenium-test-project/png.JPG";
            string localpath;
            localpath = Path.GetFullPath(spath);
                
            //Заходим и логинимся
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //Переходим в меню создания товаров
            driver.FindElement(By.XPath("//*[@id='box-apps-menu']/li[2]")).Click();
            driver.FindElement(By.XPath("//*[@href='http://localhost/litecart/admin/?category_id=0&app=catalog&doc=edit_product']")).Click();
            driver.FindElement(By.CssSelector("#tab-general input[value='1'][type = 'radio']")).Click();

            //Заполняем все поля, сохраняем и проверяем,что в списке есть товар с таким же названием
            driver.FindElement(By.Name("name[en]")).SendKeys("graphics card");
            driver.FindElement(By.Name("code")).SendKeys("789654");
            driver.FindElement(By.CssSelector("#tab-general input[type='checkbox'][data-name='Rubber Ducks'] ")).Click();
            driver.FindElement(By.XPath("//*[@id='tab-general']//tr[7]//tr[2]/td[1]")).Click();
            driver.FindElement(By.XPath("//*[@name='quantity']")).Clear();
            driver.FindElement(By.XPath("//*[@name='quantity']")).SendKeys("45");
            driver.FindElementByName("new_images[]").SendKeys(localpath);
            driver.FindElement(By.XPath("//*[@name='date_valid_from']")).SendKeys("13.05.2018");
            driver.FindElement(By.XPath("//*[@name='date_valid_to']")).SendKeys("15.05.2018");
            driver.FindElementByName("save").Click();
            driver.FindElement(By.XPath("//*[@id='content']//a[contains(text(), 'graphics card')]"));
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}