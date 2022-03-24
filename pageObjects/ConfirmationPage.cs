using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace CSharpSeleniumFramework.pageObjects
{
    class ConfirmationPage
    {
        IWebDriver driver;
        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement countryInput;

        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement suggestedCountry;

        [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
        private IWebElement agreementCheckBox;

        [FindsBy(How = How.CssSelector, Using = "[value = 'Purchase']")]
        private IWebElement purchaseButton;
        
        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement successText;

        public IWebElement getCountry()
        {
            return countryInput;
        }

        public IWebElement getSuggestedCountry()
        {
            return suggestedCountry;
        }

        public IWebElement getPurchaseButton()
        {
            return purchaseButton;
        }
        
        public IWebElement getAgreementCheckBox()
        {
            return agreementCheckBox;
        }

        public IWebElement getSuccessText()
        {
            return successText;
        }

        //Operations
        public void CountryName(string country)
        {
            getCountry().SendKeys(country); ;
        }
        public void WaitForSuggestion()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
        }

        public void ClickSuggestedCountry()
        {
            getSuggestedCountry().Click();
        }

        public void AgreementChecked()
        {
            getAgreementCheckBox().Click();
        }

        public void Purchase()
        {
            getPurchaseButton().Click();
        }

        
    }
}
