using OpenQA.Selenium;
using SeleniumExtras.PageObjects;


namespace CSharpSeleniumFramework.pageObjects
{
    class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //PageObject Factory
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;


        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement checkbox;


        [FindsBy(How = How.CssSelector, Using = "input[value = 'Sign In']")]
        private IWebElement signinButton;

        public ProductsPage validLogin(string user, string pswd)
        {
            username.SendKeys(user);
            password.SendKeys(pswd);
            checkbox.Click();
            signinButton.Click();
            return new ProductsPage(driver);
        }

        public IWebDriver getDriver() 
        { 
            return driver;
        }
        public IWebElement getUserName()
        {
            return username;
        }
    }
}
