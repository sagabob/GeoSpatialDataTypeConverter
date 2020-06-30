using System;
using System.Collections.Generic;
using NLog;
using Tdp.GeospatialConverter.Application.Handlers;
using Tdp.GeospatialConverter.Application.Helpers;
using Tdp.GeospatialConverter.Svc.Models;

namespace Tdp.GeospatialConverter.Svc.Handlers
{
    public class GeoConvertingHandler : IGeoConvertingHandler
    {
        private readonly IGeospatialConvertingHandler _convertingHandler;
        private readonly GdalHelpers _gdalHelpers;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IZippingHandler _zippingHandler;

        public GeoConvertingHandler(GdalHelpers gdalHelpers, IGeospatialConvertingHandler convertingHandler,
            IZippingHandler zippingHandler)
        {
            _gdalHelpers = gdalHelpers;

            _convertingHandler = convertingHandler;

            _zippingHandler = zippingHandler;
        }

        public string ConvertingPreprocessor(List<string> inputFileNames, IDictionary<string, string> parameters,
            string folderPath)
        {
            try
            {
                
                var inputFormatValue = parameters[InputConvertingParameters.InputFormat];

                var outputFormatValue = parameters[InputConvertingParameters.OutputFormat];

                var inputGeoType = GdalHelpers.ParseFromString(inputFormatValue);

                var outputGeoType = GdalHelpers.ParseFromString(outputFormatValue);

                var outputExtension = _gdalHelpers.GetExtension(outputGeoType);

                var outputFileNames = new List<string>();

                foreach (var fileName in inputFileNames)
                {
                    var outputFileName = fileName + "-" + Guid.NewGuid() + "." + outputExtension;

                    var isConverted = _convertingHandler.ConvertingHandler(fileName, outputFileName,
                        inputGeoType, outputGeoType);

                    if (isConverted)
                        outputFileNames.Add(outputFileName);
                }


                if (inputFileNames.Count == outputFileNames.Count)
                    return _zippingHandler.Zipping(outputFileNames, folderPath);
                else
                {
                    _logger.Error($"Exception in converting one of input fiels");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception {ex.Message}");
                return null;
            }
        }
    }
}