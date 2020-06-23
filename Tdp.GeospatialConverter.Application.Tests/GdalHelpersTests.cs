using FluentAssertions;
using Tdp.GeospatialConverter.Application.Config;
using Tdp.GeospatialConverter.Application.Helpers;
using Xunit;

namespace Tdp.GeospatialConverter.Application.Tests
{
    public class GdalHelpersTests
    {
        private readonly GdalHelpers _gdalHelpers;

        public GdalHelpersTests()
        {
            _gdalHelpers = new GdalHelpers();
        }

        [Theory]
        [InlineData(GeospatialContentType.Kml, "KML")]
        [InlineData(GeospatialContentType.Gml, "GML")]
        [InlineData(GeospatialContentType.Geojson, "GeoJSON")]
        public void ShouldGetExpectedDriverNameWithInputContentType(GeospatialContentType inputContentType,
            string expectedDriverName)
        {
            var outputDriverName = _gdalHelpers.GetDriverName(inputContentType);

            outputDriverName.Should().Be(expectedDriverName);
        }
    }
}