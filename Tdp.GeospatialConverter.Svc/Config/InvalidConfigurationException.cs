using System;

namespace Tdp.GeospatialConverter.Svc.Config
{
    [Serializable]
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException()
        {
        }

        public InvalidConfigurationException(string name)
            : base($"Invalid path for configuration folder {name}")
        {
        }
    }
}