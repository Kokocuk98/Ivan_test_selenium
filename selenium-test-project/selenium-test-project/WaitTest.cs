using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    class WaitTest
    {
        public ChromeDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void TestWait()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            int m;
            int n;
            string f;

            //Заходим в магазин
            driver.Url = "http://localhost/litecart/";
            //циклом добавляем 3 товара в корзину
            for (int i = 1; i <= 3; i++)
            {

                //Кликаем на товар и записываем сколько товаров в корзине сейчас
                driver.FindElement(By.XPath("//*[@id='box-most-popular']//li["+i+"]")).Click();
                f = driver.FindElement(By.CssSelector("#cart span.quantity")).GetAttribute("textContent");
                n = Convert.ToInt32(f);

                //Проверяем наличие выбора опций товара
                Boolean present;
                try
                {
                    driver.FindElement(By.CssSelector("#box-product select"));
                    present = true;
                }
                catch (NoSuchElementException )
                {
                    present = false;
                }
                //Заполняем опции товара если они есть
                //Добавляем товар, если опции отсутствуют
                if (present == true)
                {
                    driver.FindElement(By.CssSelector("#box-product select")).SendKeys("large" + Keys.Enter);
                    driver.FindElement(By.CssSelector("#box-product button[type = 'submit']")).Click();
                }
                else
                {
                    driver.FindElement(By.CssSelector("#box-product button[type = 'submit']")).Click();
                }

                //Проверяем, что количество товаров в корзине увеличилось
                f = driver.FindElement(By.CssSelector("#cart span.quantity")).GetAttribute("textContent");
                m = Convert.ToInt32(f);

                //Если количество товаров изменилось возвращаемся на главную
                if (m > n)
                {
                    driver.FindElement(By.CssSelector("#logotype-wrapper img")).Click();
                }
            }
            
            //Заходим в корзину и записываем количество элементов товаров
            driver.FindElement(By.CssSelector("#cart a.link")).Click();
            int itemCount = driver.FindElements(By.CssSelector("#box-checkout-cart li.item")).Count;
            for (int k = 1; k <= itemCount; k++)
            {
                //Записываем имя первого товара, ждем его появления и удаляем
                string name = driver.FindElement(By.CssSelector("#box-checkout-cart strong")).GetAttribute("textContent");
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#box-checkout-cart button[value='Remove']")));
                element.Click();

                //Проверяем, что товар удалился из списка
                Boolean order;
                try
                {
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                    driver.FindElement(By.XPath("//*[@id='order_confirmation-wrapper']//*[contains(text(), '" + name + "')]"));
                    order = true;
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
                }
                catch (NoSuchElementException)
                {
                    order = false;
                }
                if (order == true)
                {
                    Assert.Fail("Товар не удалился");
                }
            }
            driver.FindElement(By.XPath("//*[@id='main-wrapper']//*[text()='There are no items in your cart.']"));
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
