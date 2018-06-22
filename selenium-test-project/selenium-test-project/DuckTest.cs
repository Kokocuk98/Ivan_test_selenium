using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class DuckTest
    {
        public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }
        [Test]
        public void TestDuck()

        {
            driver.Url = "http://localhost/litecart/";

            string[] Arr1 = new string[3];
            string[] Arr2 = new string[3];
            int i;
            int j;
            string decoration;
            string weight;
            int r;
            int g;
            int b;
            //проверяем цвет обычной цены и то, что она зачеркнута
            string regularPriceColor = driver.FindElement(By.CssSelector("#box-campaigns s.regular-price")).GetCssValue("color");
            string[] regularPriceChanel = regularPriceColor.Replace("rgba(", "").Replace(")", "").Split(",");
            r = Int32.Parse(regularPriceChanel[0].Trim());
            g = Int32.Parse(regularPriceChanel[1].Trim());
            b = Int32.Parse(regularPriceChanel[2].Trim());
            decoration = driver.FindElementByCssSelector("#box-campaigns s.regular-price").GetCssValue("text-decoration-line");
            if (r == g && g == b && decoration == "line-through")
            {
                //проверяем цвет акционной цены и то, что она выделена жирным
                string campaignPriceColor = driver.FindElement(By.CssSelector("#box-campaigns strong.campaign-price")).GetCssValue("color");
                string[] campaignPriceChanel = campaignPriceColor.Replace("rgba(", "").Replace(")", "").Split(",");
                r = Int32.Parse(campaignPriceChanel[0].Trim());
                g = Int32.Parse(campaignPriceChanel[1].Trim());
                b = Int32.Parse(campaignPriceChanel[2].Trim());
                weight = driver.FindElementByCssSelector("#box-campaigns strong.campaign-price").GetCssValue("font-weight");
                if (g == 0 && g == b && weight == "700")
                {
                    string regularPriceSize = driver.FindElementByCssSelector("#box-campaigns s.regular-price").GetAttribute("offsetHeight");
                    string campaignPriceSize = driver.FindElementByCssSelector("#box-campaigns strong.campaign-price").GetAttribute("offsetHeight");
                    i = Convert.ToInt32(regularPriceSize);
                    j = Convert.ToInt32(campaignPriceSize);
                    if (i > j)
                    {
                        Assert.Fail("Цвет/Размер некорректны");
                    }
                }
            }
            else
            Assert.Fail("Цвет/Размер некорректны");

            //Записываем свойства товара в массив
            Arr1[0] = driver.FindElementByCssSelector("#box-campaigns div.name").GetAttribute("innerText");
            Arr1[1] = driver.FindElementByCssSelector("#box-campaigns s.regular-price").GetAttribute("innerText");
            Arr1[2] = driver.FindElementByCssSelector("#box-campaigns strong.campaign-price").GetAttribute("innerText");

            driver.FindElementByCssSelector("#box-campaigns  a:first-child").Click();

            Arr2[0] = driver.FindElementByCssSelector("#box-product h1.title").GetAttribute("innerText");
            Arr2[1] = driver.FindElementByCssSelector("#box-product s.regular-price").GetAttribute("innerText");
            Arr2[2] = driver.FindElementByCssSelector("#box-product strong.campaign-price").GetAttribute("innerText");
           
            //проверяем цвет обычной цены и то, что она зачеркнута на странице товара
            string regularPriceColor2 = driver.FindElement(By.CssSelector("#box-product s.regular-price")).GetCssValue("color");
            string[] regularPriceChanel2 = regularPriceColor2.Replace("rgba(", "").Replace(")", "").Split(",");
            r = Int32.Parse(regularPriceChanel2[0].Trim());
            g = Int32.Parse(regularPriceChanel2[1].Trim());
            b = Int32.Parse(regularPriceChanel2[2].Trim());
            decoration = driver.FindElementByCssSelector("#box-product s.regular-price").GetCssValue("text-decoration-line");
            if (r == g && g == b && decoration == "line-through")
            {
                //проверяем цвет акционной цены и то, что она выделена жирным на странице товара
                string campaignPriceColor = driver.FindElement(By.CssSelector("#box-product strong.campaign-price")).GetCssValue("color");
                string[] campaignPriceChanel = campaignPriceColor.Replace("rgba(", "").Replace(")", "").Split(",");
                r = Int32.Parse(campaignPriceChanel[0].Trim());
                g = Int32.Parse(campaignPriceChanel[1].Trim());
                b = Int32.Parse(campaignPriceChanel[2].Trim());
                weight = driver.FindElementByCssSelector("#box-product strong.campaign-price").GetCssValue("font-weight");
                if (g == 0 && g == b && weight == "700")
                {
                    string regularPriceSize = driver.FindElementByCssSelector("#box-product s.regular-price").GetAttribute("offsetHeight");
                    string campaignPriceSize = driver.FindElementByCssSelector("#box-product strong.campaign-price").GetAttribute("offsetHeight");
                    i = Convert.ToInt32(regularPriceSize);
                    j = Convert.ToInt32(campaignPriceSize);
                    if (i > j)
                    {
                        Assert.Fail("Цвет/Размер некорректны");
                    }
                }
            }
            else
            Assert.Fail("Цвет/Размер некорректны");

            //Сравниваем свойства товара с разных страниц
            for (int k = 0; k <= 2; k++)
                {
                    if (Arr1[k] == Arr2[k])
                    { }
                    else
                    Assert.Fail("Данные на траницах различаются");
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