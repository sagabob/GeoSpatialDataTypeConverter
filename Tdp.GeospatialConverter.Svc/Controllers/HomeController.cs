using System.Web.Mvc;
using Tdp.GeospatialConverter.Svc.Config;

namespace Tdp.GeospatialConverter.Svc.Controllers
{
    public class HomeController : Controller
    {
      // GET: Home
        public ActionResult Index(GeoServiceConfiguration serviceConfiguration)
        {
            ViewBag.BuildNumber = serviceConfiguration.BuildNumber;

            return View();
        }
    }
}