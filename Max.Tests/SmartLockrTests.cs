using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using System.Resources;
using Xunit;


namespace Max.Tests
{
    public class SmartLockrTests : IDisposable
    {
        public static IWebDriver _browserDriver;
        public IConfiguration _config;
        public string _filePath;

        public SmartLockrTests()
        {
            _browserDriver = new ChromeDriver("./");
            _config = new ConfigurationBuilder().AddJsonFile("config.json").Build();
            _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/sample_resume.txt";
        }

        public void Dispose()
        {
            _browserDriver.Quit();
        }

        [Fact]
        public void Apply_For_Job()
        {
            //go to url and click apply button for QA Engineer role
            _browserDriver.Navigate().GoToUrl(_config["mainPage"]);
            _browserDriver.FindElement(By.Id("hs_cos_wrapper_language-switcher")).Click();
            _browserDriver.FindElement(By.XPath("//*[@class='lang_switcher_link' and @data-language='en' ]")).Click();

            //Navigate to careers page
            _browserDriver.FindElement(By.LinkText("About us")).Click();
            _browserDriver.FindElement(By.XPath("//div[@class='kl-navbar__description']/a[contains(@href,'https://www.smartlockr.eu/en/careers?hsLang')]")).Click();


            // Verify that  Machine Learning Engineer exists and 
            CheckIfVacancyTrueOrFalse("Machine Learning Engineer", true);
            CheckIfVacancyTrueOrFalse("Security test specialist", false);

            void CheckIfVacancyTrueOrFalse(String VacancyName, Boolean ShouldExists)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_browserDriver;
                var xPath = "//div[@id='recruitee-careers']//span[contains(text(),'" + VacancyName + "')]";
                if (ShouldExists)
                {
                    var elem = _browserDriver.FindElement(By.XPath(xPath));
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", elem);
                }
                if (!ShouldExists)
                {
                    try
                    {
                        var elem = _browserDriver.FindElement(By.XPath(xPath));
                        js.ExecuteScript("arguments[0].scrollIntoView(true);", elem);
                        Console.WriteLine("*** Expected result failed: " + VacancyName + " does exist");
                        Assert.True(false);
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                        Assert.True(true);
                        Console.WriteLine("*** Expected result obtained: " + VacancyName + " does not exist");
                    }
                }
            }
        }
    }
}
