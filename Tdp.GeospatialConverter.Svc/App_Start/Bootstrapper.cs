using System.Web.Http;

namespace Tdp.GeospatialConverter.Svc
{
    public class Bootstrapper
    {
        public static void Run()
        {
            //Configure AutoFac  
            AutofacConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}