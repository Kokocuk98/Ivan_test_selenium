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
            //Заходим и логинимся
            driver.Url = "http://localhost/litecart/";
            
            int MenuCount = driver.FindElements(By.XPath("//*[@class='product column shadow hover-light']")).Count;
            ReadOnlyCollection<IWebElement> menu = driver.FindElements(By.XPath("//*[@class='product column shadow hover-light']"));

            for (int i = 0; i <= MenuCount - 1; i++)
            {
                menu[i].FindElement(By.XPath(".//*[contains(@class, 'sticker sale') or contains(@class, 'sticker new')]"));
                int StickerCount = menu[i].FindElements(By.XPath("//*[contains(@class, 'sticker sale') or contains(@class, 'sticker new')]")).Count;
                if (StickerCount > 1 || StickerCount < 1)
                    Assert.Fail("Больше одного стикера у уточки");

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
