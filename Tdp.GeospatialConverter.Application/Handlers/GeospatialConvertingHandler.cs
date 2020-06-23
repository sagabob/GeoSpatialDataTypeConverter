using System;
using NLog;
using OSGeo.OGR;
using Tdp.GeospatialConverter.Application.Config;
using Tdp.GeospatialConverter.Application.Helpers;

namespace Tdp.GeospatialConverter.Application.Handlers
{
    public class GeospatialConvertingHandler: IGeospatialConvertingHandler
    {
        private readonly GdalHelpers _gdalHelpers;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public GeospatialConvertingHandler(GdalHelpers gdalHelpers)
        {
            _gdalHelpers = gdalHelpers;
        }
        public bool ConvertingHandler(string inputFileName, string outputFileName,
            GeospatialContentType inputContentType, GeospatialContentType outputContentType)
        {
            var inputDriverName = _gdalHelpers.GetDriverName(inputContentType);

            var outputDriverName = _gdalHelpers.GetDriverName(outputContentType);

            return Converting(inputFileName, outputFileName, inputDriverName, outputDriverName);
        }

        public bool Converting(string inputFileName, string outputFileName, string inputDriverName,
            string outputDriverName)
        {
            try
            {
                var sourceDriver = Ogr.GetDriverByName(inputDriverName);
                var sourceDataSource = sourceDriver.Open(inputFileName, 0);

                var count = sourceDataSource.GetLayerCount();

                var targetDriver = Ogr.GetDriverByName(outputDriverName);

                var targetDataSource =
                    targetDriver.CreateDataSource(outputFileName,
                        new string[] { });

                for (var i = 0; i < count; i++)
                {
                    var layer = sourceDataSource.GetLayerByIndex(i);
                    targetDataSource.CopyLayer(layer, layer.GetName(), new string[] { });
                }

                // save into file
                targetDataSource.SyncToDisk();

                // dispose 
                targetDataSource.Dispose();
                targetDriver.Dispose();
                
                // dispose 
                sourceDriver.Dispose();
                sourceDataSource.Dispose();

            }
            catch (Exception e)
            {
                _logger.Error($"Exception {e.Message}");
                return false;
            }

            return true;
        }
    }
}