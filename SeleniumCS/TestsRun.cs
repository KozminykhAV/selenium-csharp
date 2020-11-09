using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumCS
{
    class TestsRun : TestClass
    {
        [Test, Order(1)]
        public void SberOnlineAuthorizationTest()
        {
            driver.Url = "https://online.sberbank.ru/";

            IWebElement login = driver.FindElement(By.XPath("//input[contains(@name, 'login')]"));
            login.Click();
            login.SendKeys("0123456789");

            IWebElement password = driver.FindElement(By.XPath("//input[contains(@name, 'password')]"));
            password.Click();
            password.SendKeys("12A3S4D5F6");
            password.SendKeys(Keys.Enter);

            IWebElement warning = driver.FindElement
                (By.XPath("//form[@id='homeAuth']//div[contains(@data-unit, 'markdown')]"));
            IWebElement innerText = driver.FindElement
                (By.XPath("//form[@id='homeAuth']//div[contains(@data-unit, 'markdown')]/p"));

            Assert.IsTrue(warning.Displayed);
            Assert.IsTrue(innerText.GetAttribute("innerHTML").ToString().Contains("Неверный логин или пароль"));
        }

        [Test, Order(2)]
        public void GoToOnlineServicesPageTest()
        {
            driver.Url = "https://www.sberbank.ru/";

            IWebElement serviceButton = driver.FindElement(By.XPath
                ("//*[@class='kitt-content']//a[contains(@aria-label, 'Сервисы')]"));
            serviceButton.Click();

            IWebElement allServices = serviceButton.FindElement
                (By.XPath("..//a[contains(@data-cga_click_top_menu, 'Все онлайн-сервисы')]"));
            allServices.Click();

            Assert.IsTrue(driver.Title.Contains("Онлайн-сервисы"));
        }

        [Test, Order(3)]
        public void IsCurrentPageIsPerson()
        {
            driver.Url = "https://www.sberbank.ru/";

            IWebElement target = driver.FindElement
                (By.XPath("//div[@class='kitt-header__sections']//li[contains(@class, 'current')]"));
            string actual = target.FindElement(By.XPath("a")).GetAttribute("data-cga_click_segment");

            Assert.AreEqual("Частным клиентам", actual);
        }

        [Test, Order(4)]
        public void IsCurrentPageIsBusiness()
        {
            driver.Url = "https://www.sberbank.ru/ru/svoedelo";

            IWebElement target = driver.FindElement
                (By.XPath("//div[@class='kitt-header__sections']//li[contains(@class, 'current')]"));
            string actual = target.FindElement(By.XPath("span")).GetAttribute("innerHTML");

            Assert.AreEqual("Самозанятым", actual);

        }
    }
}
