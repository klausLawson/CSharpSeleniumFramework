using CSharpSeleniumFramework.utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace CSharpSeleniumFramework.tests
{
    class SortWebTables : Base
    {
        [Test]
        public void SortTables()
        {
            ArrayList a = new ArrayList();
            ArrayList b = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");
            //step 1 - Get all veggie into arraylist
            IList<IWebElement> veggies = driver.FindElements(By.XPath("//td[1]"));
            foreach (IWebElement veggie in veggies)
            {
                a.Add(veggie.Text);
            }

            //step 2 - sort this arrayList
            foreach (String element in a)
            {
                TestContext.Progress.WriteLine(element);
            }
            TestContext.Progress.WriteLine("After Sorting");
            a.Sort();
            foreach (String element in a)
            {
                TestContext.Progress.WriteLine(element);
            }

            //step 3 - Go and Click column
            //th[contains(@aria-label,'fruit name')]
            driver.FindElement(By.CssSelector("th[aria-label *= 'fruit name']")).Click();

            //step 4 Get all veggie names into arrayList B
            IList<IWebElement> sortedVeggies = driver.FindElements(By.XPath("//td[1]"));
            foreach (IWebElement veggie in sortedVeggies)
            {
                b.Add(veggie.Text);
            }
            //arraylist A to b equal
            Assert.AreEqual(a, b);
        }
    }
}
