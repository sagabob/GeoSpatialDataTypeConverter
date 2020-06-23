using System.Configuration;
using System.IO;

namespace Tdp.GeospatialConverter.Svc.Config
{
    public class LocalConfigurationBuilder
    {
        public static GeoServiceConfiguration Builder()
        {
            var savedDataPath = ConfigurationManager.AppSettings["SavedDataPath"];
            var localDataPath = ConfigurationManager.AppSettings["LocalDataPath"];

            if (string.IsNullOrEmpty(savedDataPath) || !Directory.Exists(savedDataPath))
                throw new InvalidConfigurationException("SavedDataPath");

            if (string.IsNullOrEmpty(localDataPath) || !Directory.Exists(localDataPath))
                throw new InvalidConfigurationException("LocalDataPath");


            return new GeoServiceConfiguration
            {
                SavedDataPath = savedDataPath,
                LocalDataPath = localDataPath
            };
        }
    }
}