using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace csharp_example
{
    class ProductTest
    {
        public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void TestSticker()
        {
            //Заходим в магазин
            driver.Url = "http://localhost/litecart/";
            
            //Находим всех уточек на странице
            int MenuCount = driver.FindElements(By.CssSelector("#main li.product")).Count;
            ReadOnlyCollection <IWebElement> menu = driver.FindElements(By.CssSelector("#main li.product"));

            //В цикле проверяем, что у каждой уточки не больше одного стикера
            for (int i = 0; i <= MenuCount - 1; i++)
            {
              //menu[i].FindElement(By.CssSelector(".sticker"));
                int StickerCount = menu[i].FindElements(By.CssSelector(".sticker")).Count;
                if (StickerCount > 1)
                    Assert.Fail("Больше одного стикера у уточки");
                if (StickerCount < 1)
                    Assert.Fail("Стикер отсутствует");
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
