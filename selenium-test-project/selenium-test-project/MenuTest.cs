using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace csharp_example
{
    class MenuTest
    {
    public ChromeDriver driver;

    [SetUp]
    public void Start()
        {
            driver = new ChromeDriver();
        }

    [Test]
    public void TestMenu()
        {
            //Заходим и логинимся
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            int MenuCount = driver.FindElements(By.XPath("//*[@id='box-apps-menu']/li")).Count;
            ReadOnlyCollection<IWebElement> menu = driver.FindElements(By.XPath("//*[@id='box-apps-menu']/li"));

            for (int i = 0; i <= MenuCount-1; i++)
            {
                menu[i].Click();
                driver.FindElement(By.XPath("//*[@id='content']/h1"));
                int count = driver.FindElements(By.XPath("//*[@class='docs']/li")).Count;
                ReadOnlyCollection<IWebElement> template = driver.FindElements(By.XPath("//*[@class='docs']/li"));

                for (int n = 0; n <= count - 1; n++)

                {
                    template[n].Click();
                    driver.FindElement(By.XPath("//*[@id='content']/h1"));
                    template = driver.FindElements(By.XPath("//*[@class='docs']/li"));
                }
                menu = driver.FindElements(By.XPath("//*[@id='box-apps-menu']/li"));
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
