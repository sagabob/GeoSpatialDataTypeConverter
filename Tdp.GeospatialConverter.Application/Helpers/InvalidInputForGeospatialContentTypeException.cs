using System;

namespace Tdp.GeospatialConverter.Application.Helpers
{
    [Serializable]
    public class InvalidInputForGeospatialContentTypeException : Exception
    {
        public InvalidInputForGeospatialContentTypeException()
        {
        }

        public InvalidInputForGeospatialContentTypeException(string name)
            : base($"Invalid input geospatial content type {name}")
        {
        }
    }
}