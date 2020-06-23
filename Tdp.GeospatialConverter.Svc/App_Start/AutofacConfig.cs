using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Tdp.GeospatialConverter.Application.Handlers;
using Tdp.GeospatialConverter.Application.Helpers;
using Tdp.GeospatialConverter.Svc.Config;
using Tdp.GeospatialConverter.Svc.Handlers;

namespace Tdp.GeospatialConverter.Svc
{
    public class AutofacConfig
    {
        public static IContainer Container;
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
           
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var serviceConfiguration = LocalConfigurationBuilder.Builder();

            builder.RegisterInstance(serviceConfiguration).SingleInstance();
            builder.RegisterType<GdalHelpers>().SingleInstance();

            builder.RegisterType<GeospatialConvertingHandler>().As<IGeospatialConvertingHandler>().InstancePerLifetimeScope();
            builder.RegisterType<GeoConvertingHandler>().As<IGeoConvertingHandler>().InstancePerLifetimeScope();
            builder.RegisterType<ZippingHandler>().As<IZippingHandler>().InstancePerLifetimeScope();

            // Set the dependency resolver to be Autofac.
            Container = builder.Build();

            return Container;
        }

       
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

       
    }
}