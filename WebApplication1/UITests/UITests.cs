using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace UITests
{
    public class AutomatedUITests : IDisposable
    {   
        private IWebDriver _driver;

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public AutomatedUITests() 
        {
            _driver = new ChromeDriver();
        }

        [Fact]
        public void Create_NewJobOffer_WhenExecuted_ReturnsCreateView()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/JobOffers/Create");
            Assert.Contains("Create", _driver.Title);
            Assert.Contains("Create", _driver.PageSource);

        }

        [Fact]
        public void When_Create_New_JobOffer_NoDescription_ReturnsErrorMessage()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/JobOffers/Create");

            _driver.FindElement(By.Id("JobTitle")).SendKeys("Test title");
            _driver.FindElement(By.Id("Salary")).SendKeys("4000");
            _driver.FindElement(By.Id("Localization")).SendKeys("Test localization");

            _driver.FindElement(By.Id("Create")).Click();
            var errorMessage = _driver.FindElement(By.Id("Description-error")).Text;
            Assert.Equal("Description is required", errorMessage);

        }

        [Fact]
        public void Create_NewCompany_WhenExecuted_ReturnsCreateView()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/Company/Create");
            Assert.Contains("Create", _driver.Title);
            Assert.Contains("Create", _driver.PageSource);

        }

        [Fact]
        public void When_Create_New_Company_ReturnToIndexView()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/Company/Create");

            _driver.FindElement(By.Id("Name")).SendKeys("The new company");

            _driver.FindElement(By.Id("CreateBtn")).Click();
 
            Assert.Contains("Index", _driver.Title);
            Assert.Contains("The new company", _driver.PageSource);
            Dispose();
        }

        [Fact]
        public void When_Create_New_Company_AndBackToList_Changes_ShouldNotBeSaved()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/Company/Create");

            _driver.FindElement(By.Id("Name")).SendKeys("My new company");

            _driver.FindElement(By.Id("BackBtn")).Click();

            Assert.Contains("Index", _driver.Title);
            Assert.DoesNotContain("My new company", _driver.PageSource);
        }

        [Fact]
        public void When_Create_NewValid_JobOffer_ShouldReturnToIndexView()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/JobOffers/Create");

            _driver.FindElement(By.Id("JobTitle")).SendKeys("Test title25");
            _driver.FindElement(By.Id("Salary")).SendKeys("4000");
            _driver.FindElement(By.Id("Localization")).SendKeys("Test localization");
            _driver.FindElement(By.Id("Description")).SendKeys("This is long description of that job offer.");
            _driver.FindElement(By.Id("ValidUntil")).SendKeys("20-02-2020");
            
            _driver.FindElement(By.Id("Create")).Click();

            Assert.Contains("Index", _driver.Title);
        }
    }
}
