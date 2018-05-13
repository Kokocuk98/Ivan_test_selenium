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

            string[] Arr1 = new string[5];
            string[] Arr2 = new string[5];
            string color = "rgba(119, 119, 119, 1)";
            string color1 = "rgba(204, 0, 0, 1)";

            //Получаем цвета и размеры цен
            string regularPriceColor = driver.FindElementByCssSelector("#box-campaigns s.regular-price").GetCssValue("color");
            string campaignPriceColor = driver.FindElementByCssSelector("#box-campaigns strong.campaign-price").GetCssValue("color");
            string regularPriceSize = driver.FindElementByCssSelector("#box-campaigns s.regular-price").GetAttribute("offsetHeight");
            string campaignPriceSize = driver.FindElementByCssSelector("#box-campaigns strong.campaign-price").GetAttribute("offsetHeight");
            int i = Convert.ToInt32(regularPriceSize);
            int j = Convert.ToInt32(campaignPriceSize);

            //Записываем свойства товара в массив
            Arr1[0] = driver.FindElementByCssSelector("#box-campaigns div.name").GetAttribute("innerText");
            Arr1[1] = driver.FindElementByCssSelector("#box-campaigns s.regular-price").GetAttribute("innerText");
            Arr1[2] = driver.FindElementByCssSelector("#box-campaigns s.regular-price").GetCssValue("text-decoration-line");
            Arr1[3] = driver.FindElementByCssSelector("#box-campaigns strong.campaign-price").GetAttribute("innerText");
            Arr1[4] = driver.FindElementByCssSelector("#box-campaigns strong.campaign-price").GetCssValue("font-weight");

            //Сравниваем цвета и размеры цен
            if (regularPriceColor == color && campaignPriceColor == color1 && i < j)
            {
                driver.FindElementByCssSelector("#box-campaigns  a:first-child").Click();

                color = "rgba(102, 102, 102, 1)";
                color1 = "rgba(204, 0, 0, 1)";
                regularPriceColor = driver.FindElementByCssSelector("#box-product s.regular-price").GetCssValue("color");
                campaignPriceColor = driver.FindElementByCssSelector("#box-product strong.campaign-price").GetCssValue("color");

                Arr2[0] = driver.FindElementByCssSelector("#box-product h1.title").GetAttribute("innerText");
                Arr2[1] = driver.FindElementByCssSelector("#box-product s.regular-price").GetAttribute("innerText");
                Arr2[2] = driver.FindElementByCssSelector("#box-product s.regular-price").GetCssValue("text-decoration-line");
                Arr2[3] = driver.FindElementByCssSelector("#box-product strong.campaign-price").GetAttribute("innerText");
                Arr2[4] = driver.FindElementByCssSelector("#box-product strong.campaign-price").GetCssValue("font-weight");

                regularPriceSize = driver.FindElementByCssSelector("#box-product s.regular-price").GetAttribute("offsetHeight");
                campaignPriceSize = driver.FindElementByCssSelector("#box-product strong.campaign-price").GetAttribute("offsetHeight");
                i = Convert.ToInt32(regularPriceSize);
                j = Convert.ToInt32(campaignPriceSize);

                //Сравниваем свойства товара с разных страниц
                if (regularPriceColor == color && campaignPriceColor == color1 && i < j)
                {
                    for (int k = 0; k <= 4; k++)
                    {
                        if (Arr1[k] == Arr2[k])
                        { }
                        Assert.Fail("Данные на траницах различаются");
                    }
                }
                Assert.Fail("Цвет/Размер некорректны");
            }
            Assert.Fail("Цвет/Размер некорректны");
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}