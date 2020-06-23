using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Tdp.GeospatialConverter.Svc.Handlers
{
    public class ZippingHandler: IZippingHandler
    {
        public string Zipping(List<string> fileNames, string folderPath)
        {
            var zipFile = folderPath + "\\" + Guid.NewGuid() + ".zip";

            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                foreach (var fPath in fileNames) archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
            }

            return zipFile;
        }
    }
}