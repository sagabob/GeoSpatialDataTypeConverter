using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace Tdp.GeospatialConverter.Specs
{
    [Binding]
    public class GeoConvertingServiceSteps
    {
        private string _url;
        private IWebDriver _driver;

        [Given(@"The page url")]
        public void GivenThePageUrl()
        {
            _url = "https://tdp-techground.info/GeoConvertingService";

            _driver = new ChromeDriver();
        }

        [When(@"I access the page")]
        public void WhenIAccessThePage()
        {
            _driver.Navigate().GoToUrl(_url);
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            var title = _driver.Title;
            title.Should().Be("Geospatial Data Converter");
        }
    }
}