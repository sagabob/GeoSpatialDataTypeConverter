using OSGeo.GDAL;
using OSGeo.OGR;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tdp.GeospatialConverter.Svc
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();

            Bootstrapper.Run();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register Gdal
            GdalConfiguration.ConfigureGdal();
            GdalConfiguration.ConfigureOgr();

            Gdal.AllRegister();
            Ogr.RegisterAll();
        }
    }
}