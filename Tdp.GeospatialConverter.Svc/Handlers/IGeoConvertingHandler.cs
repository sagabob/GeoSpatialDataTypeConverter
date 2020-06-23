using System.Collections.Generic;

namespace Tdp.GeospatialConverter.Svc.Handlers
{
    public interface IGeoConvertingHandler
    {
        string ConvertingPreprocessor(List<string> inputFileNames, IDictionary<string, string> parameters,
            string folderPath);
    }
}