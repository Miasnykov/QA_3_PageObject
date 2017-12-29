using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace IhorMiasnykov_WDtest
{
    [TestFixture]
    public class TestClass
    {
        IWebDriver Browser;
        [OneTimeSetUp]
        public void Open()
        {
            string address = "https://www.mailinator.com/";
            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl(address);
        }

        [Test]
        public void VerifyMail()
        {
            WebDriverWait wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("inboxfield")));
            IWebElement mailField = Browser.FindElement(By.Id("inboxfield"));
            string mail = "TestSeleniumWD@mailinator.com";
            mailField.SendKeys(mail);
            IWebElement buttonOk = Browser.FindElement(By.XPath(".//div[1]/span/button"));
            buttonOk.Click();

        }
        [Test]
        public void VerifyMessage()
        {
            WebDriverWait wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//*[@class='all_message-min']/div[@title = \"FROM\"]")));
            string from = Browser.FindElement(By.XPath(".//*[@class='all_message-min']/div[@title = \"FROM\"]")).Text;
            Assert.AreEqual("Ihor Miasnykov", from);
            string title = Browser.FindElement(By.CssSelector(".all_message-min_text.all_message-min_text-3")).Text;
            Assert.AreEqual("TestMessage12345", title);
        }
        [Test]
        public void VerifyMessageText()
        {
            WebDriverWait wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".all_message-min")));
            IWebElement message = Browser.FindElement(By.CssSelector(".all_message-min"));
            message.Click();
            Browser.SwitchTo().Frame(Browser.FindElement(By.Id("msg_body")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("html/body/div[1]")));
            string text = Browser.FindElement(By.XPath("html/body/div[1]")).Text;
            Assert.AreEqual("TestWDMessage54321", text);
        }
        [OneTimeTearDown]
        public void BrowserClose()
        {
            Browser.Close();
        }
    }
}
