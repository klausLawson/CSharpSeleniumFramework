using CSharpSeleniumFramework.utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace CSharpSeleniumFramework.tests
{
    class ChildWindowHandling : Base
    {
        IWebDriver driver;
        
        [Test]
        public void HnadleWindow()
        {
            string email= "mentor@rahulshettyacademy.com";
            String parentWindowId = driver.CurrentWindowHandle;
            driver.FindElement(By.ClassName("blinkingText")).Click();
            Assert.AreEqual(2, driver.WindowHandles.Count);
            String childWindow = driver.WindowHandles[1];
            driver.SwitchTo().Window(childWindow);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
            String text = driver.FindElement(By.CssSelector(".red")).Text;
            String[] splittedText = text.Split("at");
                String[] trimmedString = splittedText[1].Trim().Split(" ");
            Assert.AreEqual(email, trimmedString[0]);
            driver.SwitchTo().Window(parentWindowId);
            driver.FindElement(By.Id("username")).SendKeys(trimmedString[0]);
        }
    }
}
