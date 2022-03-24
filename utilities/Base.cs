using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Configuration;
using WebDriverManager.DriverConfigs.Impl;

namespace CSharpSeleniumFramework.utilities
{
    class Base
    {
        public IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            String browserName = ConfigurationManager.AppSettings["browser"];
            InitBrowser(browserName);
            // implicit wait 5sec can be declare globaly
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
        }
        public IWebDriver getDriver()
        {
            return driver; ;

        }
        //factory design pattern
        public void InitBrowser(String browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break; 
            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }
        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }
    }
}
