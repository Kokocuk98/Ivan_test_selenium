using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace csharp_example
{
    class GeoZones
    {
        public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void GeoTest()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            //Заходим и логинимся
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //Заходим в раздел гео зоны
            driver.FindElement(By.XPath("//*[@href='http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones']")).Click();

            //Находим все страны на странице
            //Создаем 2 массива которые потом будут сравниваться
            int CountryCount = driver.FindElements(By.XPath("//*[@class='row']")).Count;
          
            for (int i = 1; i <= CountryCount; i++)
            {
                driver.FindElement(By.XPath("//*[@class='row'][" + i + "]/td[3]/a")).Click();
                int ZoneCount = driver.FindElements(By.XPath("//*[@id='table-zones']//*[contains(@name,'[zone_code]')]")).Count;
                ReadOnlyCollection<IWebElement> Zone = driver.FindElements(By.XPath("//*[@id='table-zones']//*[contains(@name,'[zone_code]')]"));
                string[] Arr2 = new string[ZoneCount];
                string[] Arr3 = new string[ZoneCount];

                for (int j = 0; j <= ZoneCount - 1; j++)
                {
                    Arr2[j] = Zone[j].FindElement(By.XPath("./option[@selected='selected']")).GetAttribute("innerText");
                }
                                
                    //Копирование массива, сортировка по алфавиту и последующее сравнение с оригиналом
                    Array.Copy(Arr2, Arr3, ZoneCount);
                    Array.Sort(Arr3);
                    for (int k = 0; k <= ZoneCount - 1; k++)
                    {
                        if (Arr2[k] == Arr3[k])
                        { }
                        else
                        {
                            Assert.Fail("Регионы отсортированы не по алфавиту");
                        }
                    }
                driver.FindElement(By.XPath("//*[@href='http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones']")).Click();
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
