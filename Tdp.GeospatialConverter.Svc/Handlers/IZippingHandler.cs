using System.Collections.Generic;

namespace Tdp.GeospatialConverter.Svc.Handlers
{
    public interface IZippingHandler
    {
        string Zipping(List<string> fileNames, string folderPath);
    }
}