using System.Threading;

namespace Tdp.GeospatialConverter.Specs.Model
{
    public class TestHelper
    {
        public static void Pause(int secondsToPause = 5000)
        {
            Thread.Sleep(secondsToPause);
        }
    }
}