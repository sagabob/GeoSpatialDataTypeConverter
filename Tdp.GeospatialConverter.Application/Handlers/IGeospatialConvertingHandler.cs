using Tdp.GeospatialConverter.Application.Config;

namespace Tdp.GeospatialConverter.Application.Handlers
{
    public interface IGeospatialConvertingHandler
    {
        bool ConvertingHandler(string inputFileName, string outputFileName,
            GeospatialContentType inputContentType, GeospatialContentType outputContentType);
    }
}