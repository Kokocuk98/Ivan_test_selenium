using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace csharp_example
{
    class RegionTest
    {
        public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void TestCountry()
        {
            //Заходим и логинимся
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            //Заходим в раздел страны
            driver.FindElement(By.XPath("//*[@href='http://localhost/litecart/admin/?app=countries&doc=countries']")).Click();

            //Находим все страны на странице
            //Создаем 2 массива которые потом будут сравниваться
            int CountryCount = driver.FindElements(By.XPath("//*[@class='row']")).Count;
            string[] Arr = new string[CountryCount];
            string[] Arr1 = new string[CountryCount];
            
            for (int i = 1; i <= CountryCount; i++)
            {
                Arr[i-1] = driver.FindElement(By.XPath("//*[@class='row'][" + i + "]/td[5]")).GetAttribute("innerText");

                //Проверяем количество регионов в стране
                string n = driver.FindElement(By.XPath("//*[@class='row'][" + i + "]/td[6]")).GetAttribute("innerText");
                int h = int.Parse(n);
                if (h>0)
                {
                    //Находим все регионы на странице
                    //Создаем 2 массива которые потом будут сравниваться
                    driver.FindElement(By.XPath("//*[@class='row'][" + i + "]/td[5]/a")).Click();
                    int RegionCount = driver.FindElements(By.XPath("//*[@class='dataTable']/tbody/tr/td[3][text()]/..")).Count;
                    ReadOnlyCollection<IWebElement> Region = driver.FindElements(By.XPath("//*[@class='dataTable']/tbody/tr/td[3][text()]/.."));
                    string[] Arr2 = new string[RegionCount];
                    string[] Arr3 = new string[RegionCount];

                    for (int j = 0; j <= RegionCount - 1; j++)
                    {
                        Arr2[j] = Region[j].FindElement(By.XPath("./td[3]")).GetAttribute("innerText");                        
                    }

                    //Копирование массива, сортировка по алфавиту и последующее сравнение с оригиналом
                    Array.Copy(Arr2, Arr3, RegionCount);
                    Array.Sort(Arr3);
                    for (int k = 0; k <= RegionCount - 1; k++)
                    {
                        if (Arr2[k] == Arr3[k])
                        { }
                        else
                        {
                            Assert.Fail("Регионы отсортированы не по алфавиту");
                        }
                    }
                    driver.FindElement(By.XPath("//*[@href='http://localhost/litecart/admin/?app=countries&doc=countries']")).Click();
                }
                   

            }
            //Копирование массива, сортировка по алфавиту и последующее сравнение с оригиналом
            Array.Copy(Arr, Arr1, CountryCount);
            Array.Sort(Arr1);
            for (int m = 0; m <= CountryCount - 1; m++)
            {
                if (Arr[m] == Arr1[m])
                { }
                else
                {
                    Assert.Fail("Страны отсортированы не по алфавиту");
                }
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
