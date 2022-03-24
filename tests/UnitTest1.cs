using CSharpSeleniumFramework.pageObjects;
using CSharpSeleniumFramework.utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
;

namespace CSharpSeleniumFramework.tests
{
    [Parallelizable(ParallelScope.Self)]
    class E2ETest : Base
    {        
        [Test, TestCaseSource("AddTestDataConfig"),Category("Regression")]
       // [TestCase("rahulshettyacademy", "learning")]
       // [TestCase("rahulshetty", "learning")]
       //[TestCaseSource("AddTestDataConfig")]

        // run all data sets of Test method in parallel - Done
        // run all methods in one class parallel - Done
        // run all test files in project parallel -Done

        //dotnet test pathto.csproj (All test)
        // dotnet test pathto.csproj --filter TestCategory=smoke
        // dotnet test pathto.csproj --filter TestCategory=smoke -- testRun

       [Parallelizable(ParallelScope.All)]
        public void EndToEndFlow(String username, String password, String[] expectedProducts)
        {            
            //String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new string[2];
            LoginPage loginPage = new LoginPage(getDriver());

            ProductsPage productPage = loginPage.validLogin(username, password);
            productPage.waitForPageDisplay();
            IList<IWebElement> products = productPage.getCards();
            foreach (IWebElement product in products)
            {
                if (expectedProducts.Contains(product.FindElement(productPage.getCardTitle()).Text))
                {
                    product.FindElement(productPage.addToCardButton()).Click();
                }
            }
            CheckoutPage checkoutPage = productPage.checkout();
            
            IList<IWebElement> checkoutCards = checkoutPage.getCards();
            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }
            Assert.AreEqual(expectedProducts, actualProducts);


            ConfirmationPage confirmationPage = checkoutPage.checkOut();

            confirmationPage.CountryName("ind");
            confirmationPage.WaitForSuggestion();
            confirmationPage.ClickSuggestedCountry();
            confirmationPage.AgreementChecked();
            confirmationPage.Purchase();

            String confirmText = confirmationPage.getSuccessText().Text;
            StringAssert.Contains("Success", confirmText);
        }


        [Test,Category("Smoke")]
        public void LocatorIdentication()
        {
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.Value.FindElement(By.Name("password")).SendKeys("12345");

            //CSS : .text-info span:nth-child(1) input
            // Xpath - //label[@class = 'text-info']/span/input
            driver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            //xpath  ==> //tagname[@attribure = 'value']
            //css selector ==> tagname[attribure = 'value']
            driver.Value.FindElement(By.XPath("//input[@value = 'Sign In']")).Click();

            //Thread.Sleep(8000);
            // Explicit wait 8sec apply to signIn btn(webObject)
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .TextToBePresentInElementValue(driver.Value.FindElement(By.Id("signInBtn")), "Sign In"));

            String errorMessage = driver.Value.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);

            IWebElement link = driver.Value.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttribute = link.GetAttribute("href");
            String expectedUrl = "https://rahulshettyacademy.com/#/documents-request";
            Assert.AreEqual(expectedUrl, hrefAttribute);

            // validate url of the link text

            driver.Value.FindElement(By.CssSelector("#terms")).Click();
        }
        
        
        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"),getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));
        }
    }
}
