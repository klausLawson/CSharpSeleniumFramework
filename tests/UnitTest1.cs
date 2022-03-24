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
    class E2ETest : Base
    {        
        [Test, TestCaseSource("AddTestDataConfig")]
       // [TestCase("rahulshettyacademy", "learning")]
       // [TestCase("rahulshetty", "learning")]
       //[TestCaseSource("AddTestDataConfig")]
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

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"),getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));
        }
    }
}
