using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Tdp.GeospatialConverter.Specs.Model;
using TechTalk.SpecFlow;

namespace Tdp.GeospatialConverter.Specs
{
    [Binding]
    public class GeoConvertingServiceSteps
    {
        private GeoConvertingServicePageModel _page;

        [Given(@"The page url & the browser")]
        public void GivenThePageUrl()
        {
            _page = new GeoConvertingServicePageModel
            {
                Url = "https://tdp-techground.info/GeoConvertingService",
                InputContentTypeDropdownListId = "inputformat",
                OutputContentTypeDropdownListId = "outputformat",
                UploadInputFileId = "UploadFile"
            };
        }

        [When(@"I access the page")]
        public void WhenIAccessThePage()
        {
        }

        [Then(@"The result should be 'Geospatial Data Converter' on the screen")]
        public void ThenTheResultShouldShowTheTitleOnTheScreen()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(_page.Url);
                TestHelper.Pause();

                _page.Title = driver.Title;
                _page.Title.Should().Be("Geospatial Data Converter");
            }
        }

        [When(@"I select input dropdown-list")]
        public void WhenIAccessTheDropdownList()
        {
        }

        [Then(@"Select KML from the dropdown-list")]
        public void ThenTheInputDropdownListShouldShowValues()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(_page.Url);

                TestHelper.Pause();

                var inputSelect = new SelectElement(driver.FindElement(By.Id(_page.InputContentTypeDropdownListId)));

                var options = new List<string>();

                for (var i = 0; i < inputSelect.Options.Count; i++)
                    options.Add(inputSelect.Options[i].GetAttribute("innerText"));

                TestHelper.Pause();

                options.Contains("KML").Should().BeTrue();
            }
        }


        [When(@"I locate the upload input")]
        public void WhenILocateTheUploadInput()
        {
        }

        [Then(@"I upload a file")]
        public void ThenIUploadAFile()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(_page.Url);

                TestHelper.Pause();

                var uploadElement = driver.FindElement(By.Id("UploadFile"));

                var uploadedFileName = AppDomain.CurrentDomain.BaseDirectory + @"\Files\" + "GeoJsonContent.json";

                uploadElement.SendKeys(uploadedFileName);

                TestHelper.Pause(2000);

                uploadElement.GetAttribute("value").Should().Contain("GeoJsonContent.json");
            }
        }


        [Then(@"I upload a file and click submit")]
        public void ThenIUploadAFileAndSubmit()
        {
            var path = @"C:\temp";
            
            var fileName = "output.zip";


            var exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            if (File.Exists(path + "\\" + fileName))
                File.Delete(path + "\\" + fileName);

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", path);

            using (IWebDriver driver = new ChromeDriver(chromeOptions))
            {
                driver.Navigate().GoToUrl(_page.Url);

                TestHelper.Pause();

                var uploadElement = driver.FindElement(By.Id("UploadFile"));

                var uploadedFileName = AppDomain.CurrentDomain.BaseDirectory + @"\Files\" + "GeoJsonContent.json";

                uploadElement.SendKeys(uploadedFileName);

                TestHelper.Pause(2000);

                driver.FindElement(By.Id("submitBtn")).Submit();

                TestHelper.Pause(2000);

                File.Exists(path + "\\" + fileName).Should().BeTrue();
            }
        }
    }
}