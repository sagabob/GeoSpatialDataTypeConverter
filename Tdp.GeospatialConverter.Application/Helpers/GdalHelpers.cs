using System;
using System.Collections.Generic;
using Tdp.GeospatialConverter.Application.Config;

namespace Tdp.GeospatialConverter.Application.Helpers
{
    public class GdalHelpers
    {
        private readonly IDictionary<GeospatialContentType, string> _gdalDriverDic;

        private readonly IDictionary<GeospatialContentType, string> _gdalExtensionDic;

        public GdalHelpers()
        {
            _gdalDriverDic = new Dictionary<GeospatialContentType, string>();

            _gdalExtensionDic = new Dictionary<GeospatialContentType, string>();

            Init();
        }

        private void Init()
        {
            _gdalDriverDic.Add(GeospatialContentType.Geojson, "GeoJSON");
            _gdalDriverDic.Add(GeospatialContentType.Kml, "KML");
            _gdalDriverDic.Add(GeospatialContentType.Gml, "GML");

            _gdalExtensionDic.Add(GeospatialContentType.Geojson, "json");
            _gdalExtensionDic.Add(GeospatialContentType.Kml, "xml");
            _gdalExtensionDic.Add(GeospatialContentType.Gml, "xml");
        }

        public string GetDriverName(GeospatialContentType inputContentType)
        {
            _gdalDriverDic.TryGetValue(inputContentType, out var outputDriverName);

            return outputDriverName;
        }

        public string GetExtension(GeospatialContentType inputContentType)
        {
            _gdalExtensionDic.TryGetValue(inputContentType, out var outputExtensionName);

            return outputExtensionName;
        }

        public static GeospatialContentType ParseFromString(string inputTypeStr)
        {
            var inputTypeInt = Convert.ToInt32(inputTypeStr);

            var enumDefined = Enum.IsDefined(typeof(GeospatialContentType), inputTypeInt);

            if (!enumDefined)
                throw new InvalidInputForGeospatialContentTypeException("Invalid input geospatial content type");

            return (GeospatialContentType)inputTypeInt;
        }
    }
}