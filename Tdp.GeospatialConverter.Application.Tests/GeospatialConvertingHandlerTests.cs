using System;
using FluentAssertions;
using OSGeo.GDAL;
using OSGeo.OGR;
using Tdp.GeospatialConverter.Application.Handlers;
using Tdp.GeospatialConverter.Application.Helpers;
using Xunit;

namespace Tdp.GeospatialConverter.Application.Tests
{
    public class GeospatialConvertingHandlerTests : IDisposable
    {
        private readonly GeospatialConvertingHandler _geospatialConvertingHandler;

        public GeospatialConvertingHandlerTests()
        {
            GdalConfiguration.ConfigureGdal();
            GdalConfiguration.ConfigureOgr();

            Gdal.AllRegister();
            Ogr.RegisterAll();
            
            _geospatialConvertingHandler = new GeospatialConvertingHandler(new GdalHelpers());
        }

        public void Dispose()
        {
        }

        [Theory]
        [InlineData("GeoJsonContent.json", "GEOJSON", "KML")]
        [InlineData("GeoJsonContent.json", "GEOJSON", "GML")]
        [InlineData("KmlContent.xml", "KML", "GML")]
        [InlineData("KmlContent.xml", "KML", "GeoJSON")]
        [InlineData("GmlContent.xml", "GML", "KML")]
        [InlineData("GmlContent.xml", "GML", "GeoJSON")]

        public void ShouldGetConvertedContentInOutputFileName(string inputFileName, string inputDriverName,
            string outputDriverName)
        {
            var inputFullName = AppDomain.CurrentDomain.BaseDirectory + @"\files\" + inputFileName;

            var outputFileName = inputFullName + Guid.NewGuid() + "."+outputDriverName.ToLower();

            var isConverted =
                _geospatialConvertingHandler.Converting(inputFullName, outputFileName, inputDriverName,
                    outputDriverName);

            isConverted.Should().BeTrue();
        }

        [Theory]
        [InlineData("GeoJsonContent-fake.json", "GEOJSON", "KML")]
        public void ShouldGetExceptionWhenInputFileNotFound(string inputFileName, string inputDriverName,
            string outputDriverName)
        {
            var inputFullName = AppDomain.CurrentDomain.BaseDirectory + @"\files\" + inputFileName;

            var outputFileName = inputFullName + Guid.NewGuid() + outputDriverName;

            var isConverted =  _geospatialConvertingHandler.Converting(inputFullName, outputFileName,
                inputDriverName,
                outputDriverName);

            isConverted.Should().BeFalse();
        }

        [Theory]
        [InlineData("GeoJsonContent.json", "GML", "KML")]
        public void ShouldGetExceptionWhenInputFileWithWrongDriver(string inputFileName, string inputDriverName,
            string outputDriverName)
        {
            var inputFullName = AppDomain.CurrentDomain.BaseDirectory + @"\files\" + inputFileName;

            var outputFileName = inputFullName + Guid.NewGuid() + outputDriverName;

            var isConverted = _geospatialConvertingHandler.Converting(inputFullName, outputFileName,
                inputDriverName,
                outputDriverName);

            isConverted.Should().BeFalse();
        }
    }



}